using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CouplingTestResultTests
    {
        static CouplingTestResult couplingTestResult;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            couplingTestResult = new CouplingTestResult();
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
            Assert.IsNotNull(couplingTestResult);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 48 반환")]
        public void SizeOfTest()
        {
            int size = couplingTestResult.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(48, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            couplingTestResult.SetMode = 1;
            couplingTestResult.SetChargeVolt = 2;
            couplingTestResult.SetDishargeVolt = 3;
            couplingTestResult.SetCurr = 4;
            couplingTestResult.SetEndTIme = 5;
            couplingTestResult.JudgeChargeIRMax = 6;
            couplingTestResult.JudgeChargeIRMix = 7;
            couplingTestResult.JudgeDischargeIRMax = 8;
            couplingTestResult.JudgeDischargeIRMix = 9;
            couplingTestResult.ResultChargeIR = 10;
            couplingTestResult.ResultDischargeIR = 11;
            couplingTestResult.ResultStatus = 1;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4,
                                0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8,
                                0, 0, 0, 9, 0, 0, 0, 10, 0, 0, 0, 11, 0, 0, 0, 1 };

            var byteArr = couplingTestResult.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            try
            {
                // 범위 : 1 ~ 2
                couplingTestResult.SetMode = -1;
                couplingTestResult.SetMode = 2;
                // 범위 : 1 ~ 2
                couplingTestResult.ResultStatus = -1;
                couplingTestResult.ResultStatus = 2;
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