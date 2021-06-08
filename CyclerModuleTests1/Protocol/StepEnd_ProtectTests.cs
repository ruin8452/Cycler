using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class StepEnd_ProtectTests
    {
        static StepEnd_Protect stepEnd_Protect;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            stepEnd_Protect = new StepEnd_Protect();
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

            stepEnd_Protect.End_Protects = new End_Protect[STEP_INFO_CNT];
            int size = stepEnd_Protect.SizeOf();

            int expected = 4 + 224 * STEP_INFO_CNT;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            const int STEP_INFO_CNT = 1;
            stepEnd_Protect.End_Protects = new End_Protect[STEP_INFO_CNT];

            stepEnd_Protect.StepCount = 300;

            for (int i = 0; i < STEP_INFO_CNT; i++)
            {
                stepEnd_Protect.End_Protects[i].EndVolt = 2;
                stepEnd_Protect.End_Protects[i].EndVoltMoveStep = 3;
                stepEnd_Protect.End_Protects[i].EndCurr = 4;
                stepEnd_Protect.End_Protects[i].EndCurrMoveStep = 5;
                stepEnd_Protect.End_Protects[i].EndTimeDay = 6;
                stepEnd_Protect.End_Protects[i].EndTimeTime = 7;
                stepEnd_Protect.End_Protects[i].EndTimeMoveStep = 8;
                stepEnd_Protect.End_Protects[i].EndCVTimeDay = 9;
                stepEnd_Protect.End_Protects[i].EndCVTimeTime = 10;
                stepEnd_Protect.End_Protects[i].EndCVTImeMoveStep = 11;
                stepEnd_Protect.End_Protects[i].EndCapa = 12;
                stepEnd_Protect.End_Protects[i].EndCapaMoveStep = 13;
                stepEnd_Protect.End_Protects[i].EndPower = 14;
                stepEnd_Protect.End_Protects[i].EndWatthour = 15;
                stepEnd_Protect.End_Protects[i].EndDeltaVp = 16;
                stepEnd_Protect.End_Protects[i].EndTemp = 17;
                stepEnd_Protect.End_Protects[i].EndTempType = 2;
                stepEnd_Protect.End_Protects[i].EndTempMoveStep = 19;
                stepEnd_Protect.End_Protects[i].EndSocEiffc = 20;
                stepEnd_Protect.End_Protects[i].EndSoc = 21;
                stepEnd_Protect.End_Protects[i].EndSocMoveStep = 22;
                stepEnd_Protect.End_Protects[i].EndSocMode = 2;
                stepEnd_Protect.End_Protects[i].EndStepSave = 24;
                stepEnd_Protect.End_Protects[i].EndCapaEiffc = 25;
                stepEnd_Protect.End_Protects[i].EndCapaRepeat = 26;
                stepEnd_Protect.End_Protects[i].EndCapaStep = 27;
                stepEnd_Protect.End_Protects[i].EndCapaValue = 28;
                stepEnd_Protect.End_Protects[i].EndCapaFinish = 29;
                stepEnd_Protect.End_Protects[i].EndEnergyEiffc = 30;
                stepEnd_Protect.End_Protects[i].EndEnergyRepeat = 31;
                stepEnd_Protect.End_Protects[i].EndEnergyStep = 32;
                stepEnd_Protect.End_Protects[i].EndEnergyValue = 33;
                stepEnd_Protect.End_Protects[i].EndEnergyFinish = 2;
                stepEnd_Protect.End_Protects[i].EndResistanceEiffc = 35;
                stepEnd_Protect.End_Protects[i].EndResistanceRepeat = 36;
                stepEnd_Protect.End_Protects[i].EndResistanceStep = 37;
                stepEnd_Protect.End_Protects[i].EndResistanceValue = 38;
                stepEnd_Protect.End_Protects[i].EndResistanceFinish = 2;
                stepEnd_Protect.End_Protects[i].EndPauseRepeat = 40;
                stepEnd_Protect.End_Protects[i].EndPauseStep = 41;
                stepEnd_Protect.End_Protects[i].EndSumCapaKind = 3;
                stepEnd_Protect.End_Protects[i].EndSumCapaValue = 43;
                stepEnd_Protect.End_Protects[i].EndSumWhKind = 3;
                stepEnd_Protect.End_Protects[i].EndSumWhValue = 45;

                stepEnd_Protect.End_Protects[i].ProtectVoltUpper = 46;
                stepEnd_Protect.End_Protects[i].ProtectVoltLower = 47;
                stepEnd_Protect.End_Protects[i].ProtectCurrUpper = 48;
                stepEnd_Protect.End_Protects[i].ProtectCurrLower = 49;
                stepEnd_Protect.End_Protects[i].ProtectCapaUpper = 50;
                stepEnd_Protect.End_Protects[i].ProtectCapaLower = 51;
                stepEnd_Protect.End_Protects[i].ProtectResistanceUpper = 52;
                stepEnd_Protect.End_Protects[i].ProtectResistanceLower = 53;
                stepEnd_Protect.End_Protects[i].ProtectTempUpper = 54;
                stepEnd_Protect.End_Protects[i].ProtectTempLower = 55;
                stepEnd_Protect.End_Protects[i].ProtectRestartTempUpper = 56;
                stepEnd_Protect.End_Protects[i].ProtectRestartTempLower = 57;
            }

            byte[] tempByte = { 0, 0, 1, 44, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8, 0, 0, 0, 9, 0, 0, 0, 10,
                                0, 0, 0, 11, 0, 0, 0, 12, 0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15, 0, 0, 0, 16, 0, 0, 0, 17, 0, 0, 0, 2, 0, 0, 0, 19, 0, 0, 0, 20,
                                0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 2, 0, 0, 0, 24, 0, 0, 0, 25, 0, 0, 0, 26, 0, 0, 0, 27, 0, 0, 0, 28, 0, 0, 0, 29, 0, 0, 0, 30,
                                0, 0, 0, 31, 0, 0, 0, 32, 0, 0, 0, 33, 0, 0, 0, 2, 0, 0, 0, 35, 0, 0, 0, 36, 0, 0, 0, 37, 0, 0, 0, 38, 0, 0, 0, 2, 0, 0, 0, 40,
                                0, 0, 0, 41, 0, 0, 0, 3, 0, 0, 0, 43, 0, 0, 0, 3, 0, 0, 0, 45, 0, 0, 0, 46, 0, 0, 0, 47, 0, 0, 0, 48, 0, 0, 0, 49, 0, 0, 0, 50,
                                0, 0, 0, 51, 0, 0, 0, 52, 0, 0, 0, 53, 0, 0, 0, 54, 0, 0, 0, 55, 0, 0, 0, 56, 0, 0, 0, 57};

            var byteArr = stepEnd_Protect.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine();
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case3-2 바이트 배열 생성 테스트 - 스텝 정보 미삽입 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest1()
        {
            stepEnd_Protect.End_Protects = null;
            var byteArr = stepEnd_Protect.ToByteArray();
        }

        [TestMethod("Case3-3 바이트 배열 생성 테스트 - 스텝 정보 개수 0개 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest2()
        {
            stepEnd_Protect.End_Protects = new End_Protect[0];
            var byteArr = stepEnd_Protect.ToByteArray();
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            stepEnd_Protect.End_Protects = new End_Protect[1];

            try
            {
                // 범위 : 3 ~ 300
                stepEnd_Protect.StepCount = 2;
                stepEnd_Protect.StepCount = 301;
                // 범위 : 1 ~ 2
                stepEnd_Protect.End_Protects[0].EndTempType = 0;
                stepEnd_Protect.End_Protects[0].EndTempType = 3;
                // 범위 : 1 ~ 2
                stepEnd_Protect.End_Protects[0].EndSocMode = 0;
                stepEnd_Protect.End_Protects[0].EndSocMode = 3;
                // 범위 : 0 <=
                stepEnd_Protect.End_Protects[0].EndCapaEiffc = -1;
                // 범위 : 0 <=
                stepEnd_Protect.End_Protects[0].EndEnergyEiffc = -1;
                // 범위 : 0 ~ 2
                stepEnd_Protect.End_Protects[0].EndEnergyFinish = -1;
                stepEnd_Protect.End_Protects[0].EndEnergyFinish = 3;
                // 범위 : 0 <=
                stepEnd_Protect.End_Protects[0].EndResistanceEiffc = -1;
                // 범위 : 0 ~ 2
                stepEnd_Protect.End_Protects[0].EndResistanceFinish = -1;
                stepEnd_Protect.End_Protects[0].EndResistanceFinish = 3;
                // 범위 : 0 ~ 3
                stepEnd_Protect.End_Protects[0].EndSumCapaKind = -1;
                stepEnd_Protect.End_Protects[0].EndSumCapaKind = 4;
                // 범위 : 0 ~ 3
                stepEnd_Protect.End_Protects[0].EndSumWhKind = -1;
                stepEnd_Protect.End_Protects[0].EndSumWhKind = 4;
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