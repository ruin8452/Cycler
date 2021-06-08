using CyclerModuleTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommModules.Tests
{
    [TestClass()]
    public class TcpModuleTests
    {
        static TcpModule tcpModule;

        static TcpEchoServer echoServer;

        static Thread worker;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            echoServer = new TcpEchoServer();

            worker = new Thread(() => echoServer.start());
            worker.Start();

            tcpModule = new TcpModule("127.0.0.1", 9000);
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {
            echoServer.End();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// TCP 소켓 테스트
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod("Case1-1 소켓 테스트 - 서버 비활성화 - false 반환")]
        public void SoketConnectTest()
        {
            bool isConn = tcpModule.Connect();

            Assert.AreEqual(false, isConn);
        }

        [TestMethod("Case1-2 소켓 테스트 - 잘못된 IP - false 반환")]
        public void SoketConnectTest1()
        {
            bool isConn = tcpModule.Connect();

            Assert.AreEqual(false, isConn);
        }

        [TestMethod("Case1-3 소켓 테스트 - Port 범위 초과 - false 반환")]
        public void SoketConnectTest2()
        {
            bool isConn = tcpModule.Connect();

            Assert.AreEqual(false, isConn);
        }

        [TestMethod("Case1-4 소켓 테스트 - 정상 - true 반환")]
        public void SoketConnectTest3()
        {
            bool isConn = tcpModule.Connect();

            Assert.AreEqual(true, isConn);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// TCP 연결 상태 테스트
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod("Case2-1 TCP 연결 상태 - 정상 - true 반환")]
        public void ConnectCheckTest()
        {
            tcpModule.Connect();
            bool isConn = tcpModule.ConnectCheck();

            Assert.AreEqual(true, isConn);
        }

        [TestMethod("Case2-2 TCP 연결 상태 - 연결 안됨")]
        public void ConnectCheckTest1()
        {
            bool isConn = tcpModule.ConnectCheck();

            Assert.AreEqual(false, isConn);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// TCP 데이터 전송 테스트
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod("Case3-1 TCP 데이터 전송(Byte) - 정상")]
        public void SendTest()
        {
            byte[] send = new byte[] { 1, 2, 3, 4, 5 };
            byte[] receive = new byte[5];

            tcpModule.Connect();

            bool result = tcpModule.Send(send);
            Console.WriteLine($"Send Result : {result} / {string.Join(",", send)}");
            Assert.AreEqual(true, result);

            tcpModule.Receive();
            Console.WriteLine($"Receive Result : {result} / {string.Join(",", receive)}");
            Assert.AreEqual(true, result);

            CollectionAssert.AreEqual(send, receive);
        }
    }
}