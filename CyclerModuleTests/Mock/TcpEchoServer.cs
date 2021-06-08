using System;
using System.Net;
using System.Net.Sockets;

namespace CyclerModuleTests.Mock
{
    public class TcpEchoServer
    {
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream ns;

        public TcpEchoServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, 9000);
        }

        public void start()
        {
            byte[] buffer = new byte[1024];
            int totalByteCount = 0;
            int readByteCount = 0;

            tcpListener.Start();

            tcpClient = tcpListener.AcceptTcpClient();
            ns = tcpClient.GetStream();

            while (true)
            {
                if (!ns.CanRead)
                    break;

                readByteCount = ns.Read(buffer, 0, buffer.Length);
                if (readByteCount == 0)
                    break;

                totalByteCount += readByteCount;

                byte[] data = new byte[readByteCount];
                Array.Copy(buffer, data, readByteCount);
                ns.Write(data, 0, readByteCount);
            }
        }

        public void End()
        {
            if (ns == null)
                return;

            ns.Close();
            tcpClient.Close();
            tcpListener.Stop();
        }
    }
}
