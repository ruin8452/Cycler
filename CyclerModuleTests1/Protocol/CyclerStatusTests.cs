using CyclerModule.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CyclerStatusTests
    {
        static CyclerState cyclerStatus;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            cyclerStatus = new CyclerStatus();
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod("Case1-1 생성자 테스트 - 스페어 배열 - 2 반환")]
        public void CyclerStatusTest()
        {
            PrivateObject po = new PrivateObject(typeof(CyclerStatus));

            int[] test = (int[])po.GetField("spare");

            Assert.AreEqual(2, test.Length);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 136 반환")]
        public void SizeOfTest()
        {
            int size = cyclerStatus.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(136, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ByteArrayTest()
        {
            cyclerStatus.State = CyclerState.WAIT;
            cyclerStatus.EmgStatus = EmgState.EMG_BACK_UP_PC;
            cyclerStatus.FaultCode = 3;
            cyclerStatus.CellStatus = CellState.NORMAL;
            cyclerStatus.TotalStepCount = 5;
            cyclerStatus.CycleRepeatCount = 6;
            cyclerStatus.CycleJumpCount = 7;
            cyclerStatus.CycleNo = 8;
            cyclerStatus.CycleRepeatNo = 9;
            cyclerStatus.CycleJumpNo = 10;
            cyclerStatus.StepNo = 11;
            cyclerStatus.TotalDays = 12;
            cyclerStatus.TotalTime = 13;
            cyclerStatus.StepDays = 14;
            cyclerStatus.StepTime = 15;
            cyclerStatus.Volt = 16;
            cyclerStatus.Curr = 17;
            cyclerStatus.Power = 18;
            cyclerStatus.Watthour = 19;
            cyclerStatus.Capacity = 20;
            cyclerStatus.Temperature = 21;
            cyclerStatus.Resistance = 22;
            cyclerStatus.ReserveStatus = 23;
            cyclerStatus.ChamberGroupStatus = 1;
            cyclerStatus.ChamberStatus = 0;
            cyclerStatus.ChamberTemperature = 26;
            cyclerStatus.ChamberSettingTemp = 27;
            cyclerStatus.InProcessStatus = 1;
            cyclerStatus.StepCount = 29;
            cyclerStatus.RealClock = 30.0;
            cyclerStatus.FwVersion = 31;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 48, 48, 48, 48,
                                0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8,
                                0, 0, 0, 9, 0, 0, 0, 10, 0, 0, 0, 11, 0, 0, 0, 12,
                                0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15, 0, 0, 0, 16,
                                0, 0, 0, 17, 0, 0, 0, 18, 0, 0, 0, 19, 0, 0, 0, 20,
                                0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 23, 0, 0, 0, 1,
                                0, 0, 0, 0, 0, 0, 0, 26, 0, 0, 0, 27, 0, 0, 0, 1,
                                0, 0, 0, 29, 64, 62, 0, 0, 0, 0, 0, 0, 0, 0, 0, 31,
                                0, 0, 0, 0, 0, 0, 0, 0 };

            var byteArr = cyclerStatus.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            try
            {
                // 범위 : 0 ~ 1
                cyclerStatus.ChamberGroupStatus = -1;
                cyclerStatus.ChamberGroupStatus = 2;
                // 범위 : 0 ~ 1
                cyclerStatus.ChamberStatus = -1;
                cyclerStatus.ChamberStatus = 2;
                // 범위 : 0 ~ 1
                cyclerStatus.InProcessStatus = -1;
                cyclerStatus.InProcessStatus = 2;
                goto FAIL;
            }
            catch { }

            Assert.IsTrue(true);
            return;

            FAIL:
            Assert.Fail();
        }
    }
}