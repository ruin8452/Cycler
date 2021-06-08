using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class ResultDataSendTests
    {
        static ResultDataSend resultDataSend;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            resultDataSend = new ResultDataSend();
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod("Case1-1 생성자 테스트 - 객체생성 여뷰 - null이 아님")]
        public void CouplingTestTest()
        {
            Assert.IsNotNull(resultDataSend);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 24 반환")]
        public void SizeOfTest()
        {
            int size = resultDataSend.SizeOf();

            int expected = 240;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            int num = 0;

            resultDataSend.TotalStepCount = ++num;
            resultDataSend.CycleRepeatCount = ++num;
            resultDataSend.CycleJumpCount = ++num;
            resultDataSend.CycleNo = ++num;
            resultDataSend.CycleRepeatNo = ++num;
            resultDataSend.CycleJumpNo = ++num;
            resultDataSend.StepNo = ++num;
            resultDataSend.StartDateTime = ++num;
            resultDataSend.TotakDays = ++num;
            resultDataSend.TotalTime = ++num;
            resultDataSend.StepDays = ++num;
            resultDataSend.StepTime = ++num;
            resultDataSend.Volt = ++num;
            resultDataSend.Curr = ++num;
            resultDataSend.Power = ++num;
            resultDataSend.Watthour = ++num;
            resultDataSend.Capa = ++num;
            resultDataSend.Temp = ++num;
            resultDataSend.Resistance = ++num;
            resultDataSend.StatusCode = ++num;
            resultDataSend.CellCode = ++num;
            resultDataSend.ChamberTemp = ++num;
            resultDataSend.StartVolt = ++num;
            resultDataSend.StepCount = ++num;
            resultDataSend.ChargeCapa = ++num;
            resultDataSend.DischargeCapa = ++num;
            resultDataSend.CVCapa = ++num;
            resultDataSend.CVTime = ++num;

            for (int i = 0; i < resultDataSend.DCIRVolt.Length; i++)
                resultDataSend.DCIRVolt[i] = ++num;
            for (int i = 0; i < resultDataSend.DCIRCurr.Length; i++)
                resultDataSend.DCIRCurr[i] = ++num;

            resultDataSend.AccCapa = ++num;
            resultDataSend.AccWatthour = ++num;
            resultDataSend.ChargeWatthour = ++num;
            resultDataSend.DischargeWatthour = ++num;
            resultDataSend.AvgVolt = ++num;
            resultDataSend.AvgCurr = ++num;
            resultDataSend.TotalCycleCount = ++num;
            resultDataSend.EndClock = ++num;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 64, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 10,
                                0, 0, 0, 11, 0, 0, 0, 12, 0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15, 0, 0, 0, 16, 0, 0, 0, 17, 0, 0, 0, 18, 0, 0, 0, 19, 0, 0, 0, 20,
                                0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 23, 0, 0, 0, 24, 0, 0, 0, 25, 0, 0, 0, 26, 0, 0, 0, 27, 0, 0, 0, 28, 0, 0, 0, 29, 0, 0, 0, 30,
                                0, 0, 0, 31, 0, 0, 0, 32, 0, 0, 0, 33, 0, 0, 0, 34, 0, 0, 0, 35, 0, 0, 0, 36, 0, 0, 0, 37, 0, 0, 0, 38, 0, 0, 0, 39, 0, 0, 0, 40,
                                0, 0, 0, 41, 0, 0, 0, 42, 0, 0, 0, 43, 0, 0, 0, 44, 0, 0, 0, 45, 0, 0, 0, 46, 0, 0, 0, 47, 0, 0, 0, 48, 0, 0, 0, 49, 0, 0, 0, 50,
                                0, 0, 0, 51, 0, 0, 0, 52, 0, 0, 0, 53, 0, 0, 0, 54, 0, 0, 0, 55, 0, 0, 0, 56, 0, 0, 0, 57, 64, 77, 0, 0, 0, 0, 0, 0 };

            var byteArr = resultDataSend.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }
    }
}