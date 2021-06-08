using CommModules.EventArgsClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CommModules
{
    public class TcpModule : ICommModule
    {
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns;

        Thread receiveThread;

        string ip;
        int port;

        public event EventHandler<ReceiveEventArg> ReceiveEvent;

        public TcpModule(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            receiveThread = new Thread(Receive);
            receiveThread.IsBackground = true;
        }

        public bool Connect()
        {
            try
            {
                tcpClient.Connect(ip, port);
                ns = tcpClient.GetStream();

                bool isConn = ConnectCheck();
                if (isConn) receiveThread.Start();

                return isConn;
            }
            catch (Exception)
            {
                //return ConnectCheck();
                return false;
            }
        }

        /// <summary>
        /// 통신 연결
        /// </summary>
        /// 
        /// <returns>
        /// 연결 여부<br/>
        /// true - 연결 성공<br/>
        /// false - 연결 실패
        /// </returns>
        public async Task<bool> ConnectAsync()
        {
            try
            {
                await tcpClient.ConnectAsync(ip, port);
                ns = tcpClient.GetStream();

                bool isConn = ConnectCheck();
                if (isConn) receiveThread.Start();

                return isConn;
            }
            catch (Exception)
            {
                //return ConnectCheck();
                return false;
            }
        }

        /// <summary>
        /// 소켓 접속 해제
        /// </summary>
        /// 
        /// <returns>
        /// 해제 완료 여부<br/>
        /// true - 해제 완료<br/>
        /// false - 해제 실패
        /// </returns>
        public bool Disconnect()
        {
            if (ns != null) ns.Close();
            tcpClient.Close();
            receiveThread.Abort();

            return !ConnectCheck();
        }

        /// <summary>
        /// 통신 연결 상태를 확인
        /// </summary>
        /// 
        /// <returns>
        /// 연결 상태 여부<br/>
        /// true - 연결 중<br/>
        /// false - 연결 끊김
        /// </returns>
        public bool ConnectCheck()
        {
            if (tcpClient.Client == null) return false;

            IPGlobalProperties iPProperties = IPGlobalProperties.GetIPGlobalProperties();

            try
            {
                // 현재 PC에 접속되어 있는 소켓의 모든 정보를 획득 후
                // 연결한 TCP와 일치하는 소켓 정보를 필터링
                TcpConnectionInformation[] tcpConnections = iPProperties.GetActiveTcpConnections()
                    .Where(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(tcpClient.Client.RemoteEndPoint)).ToArray();

                // 일치하는 소켓 정보가 있다면
                if (tcpConnections != null && tcpConnections.Length > 0)
                {
                    // 현재 접속 상태가 접속 중이면 연결
                    if (tcpConnections.First().State == TcpState.Established)
                        return true;
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        /// <summary>
        /// TCP 데이터 송신
        /// </summary>
        /// 
        /// <param name="sendData">송신할 바이트 배열</param>
        /// 
        /// <returns>
        /// 정상 송신 여부<br/>
        /// true - 정상 송신<br/>
        /// false - 송신 에러
        /// </returns>
        public bool Send(byte[] sendData)
        {
            try
            {
                ns.Write(sendData, 0, sendData.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// TCP 데이터 수신
        /// </summary>
        /// 
        /// <param name="receiveData">수신 받은 데이터</param>
        /// <param name="size">수신 받을 데이터의 크기</param>
        /// 
        /// <returns>
        /// 수신 여부<br/>
        /// true - 수신 완료<br/>
        /// false - 수신 실패
        /// </returns>
        public void Receive()
        {
            List<byte> tempReceive;

            while (true)
            {
                tempReceive = new List<byte>();

                byte[] tempByte = new byte[tcpClient.Available];

                if (!ns.CanRead || tempByte.Length == 0)
                    continue;

                do
                {
                    ns.Read(tempByte, 0, tempByte.Length);
                    tempReceive.AddRange(tempByte);
                }
                while (ns.DataAvailable);

                OnReceiveEvent(new ReceiveEventArg(tempReceive.ToArray()));
            }
        }

        private void OnReceiveEvent(ReceiveEventArg e)
        {
            ReceiveEvent?.Invoke(this, e);
        }
    }
}
