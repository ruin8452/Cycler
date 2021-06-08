using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class StepRecipyTests
    {
        static StepRecipy stepRecipy;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            stepRecipy = new StepRecipy();
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
            PrivateObject po = new PrivateObject(typeof(StepRecipy));

            int[] test = (int[])po.GetField("spare");

            Assert.AreEqual(2, test.Length);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 136 반환")]
        public void SizeOfTest()
        {
            const int STEP_INFO_CNT = 6;

            stepRecipy.StepInfo = new Step[STEP_INFO_CNT];
            int size = stepRecipy.SizeOf();

            int expected = 20 + 80 * STEP_INFO_CNT;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            const int STEP_INFO_CNT = 1;
            stepRecipy.StepInfo = new Step[STEP_INFO_CNT];

            stepRecipy.StepCount = 3;
            stepRecipy.StartStepNo = 2;
            stepRecipy.StartRepeatCount = 3;

            for (int i = 0; i < STEP_INFO_CNT; i++)
            {
                stepRecipy.StepInfo[i].CycleNo = 4;
                stepRecipy.StepInfo[i].StepNo = 5;
                stepRecipy.StepInfo[i].RunType = 6;
                stepRecipy.StepInfo[i].CtrlMode = 5;
                stepRecipy.StepInfo[i].SetVolt = 8;
                stepRecipy.StepInfo[i].SetCurr = 9;
                stepRecipy.StepInfo[i].SetPower = 10;
                stepRecipy.StepInfo[i].SetResistance = 11;
                stepRecipy.StepInfo[i].SetChamberTemp = 12;
                stepRecipy.StepInfo[i].SetChamberHum = 13;
                stepRecipy.StepInfo[i].SetChamberWaitRate = 14;
                stepRecipy.StepInfo[i].SetChamberRange = 15;
                stepRecipy.StepInfo[i].SetRecTime = 16;
                stepRecipy.StepInfo[i].SetRecVolt = 17;
                stepRecipy.StepInfo[i].SetRecCurr = 18;
                stepRecipy.StepInfo[i].SetRecTemp = 19;
                stepRecipy.StepInfo[i].CycleRepeateCount = 20;
                stepRecipy.StepInfo[i].CycleGotoStep = 21;
                stepRecipy.StepInfo[i].CycleGotoCount = 22;
                stepRecipy.StepInfo[i].CycleAccCapaInit = 1;
            }

            byte[] tempByte = { 0, 0, 0, 3, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6,
                                0, 0, 0, 5, 0, 0, 0, 8, 0, 0, 0, 9, 0, 0, 0, 10, 0, 0, 0, 11, 0, 0, 0, 12,
                                0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15, 0, 0, 0, 16, 0, 0, 0, 17, 0, 0, 0, 18,
                                0, 0, 0, 19, 0, 0, 0, 20, 0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 1,
                                0, 0, 0, 0, 0, 0, 0, 0 };

            var byteArr = stepRecipy.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case3-2 바이트 배열 생성 테스트 - 스텝 정보 미삽입 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest1()
        {
            stepRecipy.StepInfo = null;
            var byteArr = stepRecipy.ToByteArray();
        }

        [TestMethod("Case3-3 바이트 배열 생성 테스트 - 스텝 정보 개수 0개 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest2()
        {
            stepRecipy.StepInfo = new Step[0];
            var byteArr = stepRecipy.ToByteArray();
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            stepRecipy.StepInfo = new Step[1];

            try
            {
                // 범위 : 3 ~ 300
                stepRecipy.StepCount = 2;
                stepRecipy.StepCount = 301;
                // 범위 : 0 ~ 9
                stepRecipy.StepInfo[0].RunType = -1;
                stepRecipy.StepInfo[0].RunType = 10;
                // 범위 : 0 ~ 5
                stepRecipy.StepInfo[0].CtrlMode = -1;
                stepRecipy.StepInfo[0].CtrlMode = 6;
                // 범위 : 0 ~ 1
                stepRecipy.StepInfo[0].CycleAccCapaInit = -1;
                stepRecipy.StepInfo[0].CycleAccCapaInit = 2;
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