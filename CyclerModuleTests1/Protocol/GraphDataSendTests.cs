using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class GraphDataSendTests
    {
        static GraphDataSend graphDataSend;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            graphDataSend = new GraphDataSend();
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod("Case1-1 생성자 테스트 - 스페어 배열 - 2 반환")]
        public void StepRecipyTest()
        {
            Assert.IsNotNull(graphDataSend);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 136 반환")]
        public void SizeOfTest()
        {
            const int STEP_INFO_CNT = 6;

            graphDataSend.GraphDatas = new GraphData[STEP_INFO_CNT];
            int size = graphDataSend.SizeOf();

            int expected = 44 + 96 * STEP_INFO_CNT;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            const int GRAPH_DATA_CNT = 1;
            graphDataSend.GraphDatas = new GraphData[GRAPH_DATA_CNT];

            graphDataSend.TotalStepCount = 1;
            graphDataSend.CycleRepeatCount = 2;
            graphDataSend.CycleJumpCount = 3;
            graphDataSend.CycleNo = 4;
            graphDataSend.CycleRepeatNo = 5;
            graphDataSend.CycleJumpNo = 6;
            graphDataSend.StepNo = 7;
            graphDataSend.StartDateTime = 8;
            graphDataSend.TotalCount = 9;
            graphDataSend.TotalCycleCount = 10;

            for (int i = 0; i < GRAPH_DATA_CNT; i++)
            {
                graphDataSend.GraphDatas[i].TotakDays = 11;
                graphDataSend.GraphDatas[i].TotalTime = 12;
                graphDataSend.GraphDatas[i].StepDays = 13;
                graphDataSend.GraphDatas[i].StepTime = 14;
                graphDataSend.GraphDatas[i].Volt = 15;
                graphDataSend.GraphDatas[i].Curr = 16;
                graphDataSend.GraphDatas[i].Power = 17;
                graphDataSend.GraphDatas[i].Watthour = 18;
                graphDataSend.GraphDatas[i].Capa = 19;
                graphDataSend.GraphDatas[i].Temp = 20;
                graphDataSend.GraphDatas[i].Resistance = 21;
                graphDataSend.GraphDatas[i].ChamberTemp = 22;
                graphDataSend.GraphDatas[i].ChargeCapa = 23;
                graphDataSend.GraphDatas[i].DischargeCapa = 24;
                graphDataSend.GraphDatas[i].CVCapa = 25;
                graphDataSend.GraphDatas[i].CVTime = 26;
                graphDataSend.GraphDatas[i].AccCapa = 27;
                graphDataSend.GraphDatas[i].AccWatthour = 28;
                graphDataSend.GraphDatas[i].ChargeWatthour = 29;
                graphDataSend.GraphDatas[i].DischargeWatthour = 30;
                graphDataSend.GraphDatas[i].AvgVolt = 31;
                graphDataSend.GraphDatas[i].AvgCurr = 32;
                graphDataSend.GraphDatas[i].RealClock = 33;
            }

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6,
                                0, 0, 0, 7, 64, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 10, 0, 0, 0, 11, 0, 0, 0, 12,
                                0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15, 0, 0, 0, 16, 0, 0, 0, 17, 0, 0, 0, 18,
                                0, 0, 0, 19, 0, 0, 0, 20, 0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 23, 0, 0, 0, 24,
                                0, 0, 0, 25, 0, 0, 0, 26, 0, 0, 0, 27, 0, 0, 0, 28, 0, 0, 0, 29, 0, 0, 0, 30,
                                0, 0, 0, 31, 0, 0, 0, 32, 64, 64, 128, 0, 0, 0, 0, 0 };

            var byteArr = graphDataSend.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case3-2 바이트 배열 생성 테스트 - 스텝 정보 미삽입 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest1()
        {
            graphDataSend.GraphDatas = null;
            var byteArr = graphDataSend.ToByteArray();
        }

        [TestMethod("Case3-3 바이트 배열 생성 테스트 - 스텝 정보 개수 0개 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest2()
        {
            graphDataSend.GraphDatas = new GraphData[0];
            var byteArr = graphDataSend.ToByteArray();
        }
    }
}