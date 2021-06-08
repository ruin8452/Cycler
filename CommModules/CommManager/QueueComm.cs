using CommModules.EventArgsClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CommModules.CommManager
{
    /**
     *  @brief 통신 클래스
     *  @details 큐를 사용하여 반이중 통신을 관리
     *
     *  @author SSW
     *  @date 2020.08.18
     *  @version 1.0.0
     */
    public class QueueComm : ICommManager
    {
        ICommModule commModule;

        int idCode = 1;
        readonly object lockObj = 0;

        struct CommPacket
        {
            public int ID;
            public byte[] ByteData;

            public CommPacket(int id, byte[] data)
            {
                ID = id;
                ByteData = data;
            }
        }

        Queue<CommPacket> SendCommQueue = new Queue<CommPacket>();
        List<CommPacket> ReceiveCommQueue = new List<CommPacket>();
        CommPacket tempPacket = new CommPacket();

        Timer SendTimer = new Timer(50);
        Timer ReceiveTimer = new Timer(10);

        public event EventHandler<ReceiveEventArg> ReceiveEvent;

        public QueueComm(ICommModule commModule)
        {
            this.commModule = commModule;

            SendTimer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) =>
            {
                Send();
            });

            commModule.ReceiveEvent += Receive;
        }

        /**
         *  @brief 통신 연결
         *  @details 시리얼 통신 연결
         *  @version 1.0.0
         *  
         *  @param string portName 포트 이름
         *  @param int baudRate 통신 속도
         *  
         *  @return string 연결 결과
         */
        public bool Connect()
        {
            try
            {
                commModule.Connect();

                if (commModule.ConnectCheck())
                {
                    SendTimer.Start();
                    ReceiveTimer.Start();

                    return true;
                }
                else
                    commModule.Disconnect();
            }
            catch (Exception)
            {
                commModule.Disconnect();
            }

            return false;
        }

        /**
         *  @brief 통신 연결
         *  @details 시리얼 통신 연결
         *  @version 1.0.0
         *  
         *  @param string portName 포트 이름
         *  @param int baudRate 통신 속도
         *  
         *  @return string 연결 결과
         */
        public async Task<bool> ConnectAsync()
        {
            try
            {
                await commModule.ConnectAsync();

                if (commModule.ConnectCheck())
                {
                    SendTimer.Start();
                    ReceiveTimer.Start();

                    return true;
                }
                else
                    commModule.Disconnect();
            }
            catch (Exception)
            {
                commModule.Disconnect();
            }

            return false;
        }

        /**
         *  @brief 통신 연결 해제
         *  @details 시리얼 통신 연결 해제
         *  @version 1.0.0
         *  
         *  @param
         *  
         *  @return bool 연결 해제 결과@n
         *               True : 명령저장 성공@n
         *               False : 명령저장 실패
         */
        public bool Disconnect()
        {
            SendTimer.Stop();
            ReceiveTimer.Stop();
            return commModule.Disconnect();
        }

        /**
         *  @brief 명령 전송 스택(명령 타입 : byte[])
         *  @details 전송할 명령을 큐에 쌓는다.
         *  @version 1.0.0
         *  
         *  @param byte[] cmd 전송할 명령
         *  @param out int code 명령에 대한 고유코드
         *  
         *  @return bool 명령 스택 성공여부@n
         *               True : 명령저장 성공@n
         *               False : 명령저장 실패
         */
        public bool CommSendStack(byte[] cmd, out int code)
        {
            CommPacket data = new CommPacket();

            lock (lockObj)
            {
                data.ID = code = idCode++;
            }
            Debug.WriteLine($"Send code : {code}");
            data.ByteData = cmd;

            SendCommQueue.Enqueue(data);

            return true;
        }

        /**
         *  @brief 수신받은 데이터 가져가기(데이터 타입 : byte[])
         *  @details 수신받은 데이터가 쌓여있는 큐에서 코드에 해당하는 데이터를 찾아 리턴한다.
         *  @version 1.0.0
         *  
         *  @param out byte[] data 수신받은 응답 데이터
         *  @param int code 찾고 싶은 데이터에 대한 고유코드
         *  
         *  @return bool 데이터 서치 성공여부@n
         *               True : 서치 성공@n
         *               False : 서치 실패
         */
        public byte[] CommReceiveStack(int code)
        {
            List<byte> receiveData = new List<byte>();
            for (int i = 0; i < 10; i++)
            {
                List<CommPacket> dataPacket = ReceiveCommQueue.FindAll(x => x.ID == code);
                ReceiveCommQueue.RemoveAll(x => x.ID == code);

                Debug.WriteLine($"Command Receive Count : {dataPacket.Count}");

                // 모든 데이터를 하나로 합치기
                foreach (var packet in dataPacket)
                    receiveData.AddRange(packet.ByteData);

                if (dataPacket.Count != 0 && dataPacket[0].ID != -1)
                {
                    Debug.WriteLine($"Receive Succ - code : {code}");
                    return receiveData.ToArray();
                }
            }

            return Array.Empty<byte>();
        }

        /**
         *  @brief 명령 전송 타이머 함수(데이터 타입 : byte[])
         *  @details 주기적으로 큐에 쌓여있는 명령을 전송한다.
         *  @version 1.0.0
         *  
         *  @param object sender 함수를 호출한 객체(사용 안함)
         *  @param EventArgs e 이벤트 변수(사용 안함)
         *  
         *  @return 
         */
        private void Send()
        {
            Debug.WriteLine($"Send QUEUE Count : {SendCommQueue.Count}");
            if (SendCommQueue.Count <= 0)
                return;

            tempPacket = SendCommQueue.Dequeue();

            if (commModule.ConnectCheck())
            {
                try
                {
                    commModule.Send(tempPacket.ByteData);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    commModule.Connect();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

        }

        /**
         *  @brief 데이터 수신 타이머 함수(데이터 타입 : byte[])
         *  @details 주기적으로 큐에 쌓여있는 명령을 전송한다.
         *  @version 1.0.0
         *  
         *  @param object sender 함수를 호출한 객체(사용 안함)
         *  @param EventArgs e 이벤트 변수(사용 안함)
         *  
         *  @return 
         */
        private void Receive(object sender, ReceiveEventArg e)
        {
            Debug.WriteLine("QUEUE Intterrupt Receive");

            Debug.WriteLine($"Receive Data : {tempPacket.ID}, {Encoding.ASCII.GetString(e.ReceiveArray)}");
            ReceiveCommQueue.Add(new CommPacket(tempPacket.ID, e.ReceiveArray));

            OnReceiveEvent(new ReceiveEventArg(e.ReceiveArray));
        }

        private void OnReceiveEvent(ReceiveEventArg e)
        {
            ReceiveEvent?.Invoke(this, e);
        }
    }
}