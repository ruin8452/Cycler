using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CouplingTestTests
    {
        static CouplingTest couplingTest;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            couplingTest = new CouplingTest();
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
            Assert.IsNotNull(couplingTest);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 36 반환")]
        public void SizeOfTest()
        {
            int size = couplingTest.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(36, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            couplingTest.SetMode = 1;
            couplingTest.SetChargeVolt = 2;
            couplingTest.SetDishargeVolt = 3;
            couplingTest.SetCurr = 4;
            couplingTest.SetEndTIme = 5;
            couplingTest.JudgeChargeIRMax = 6;
            couplingTest.JudgeChargeIRMix = 7;
            couplingTest.JudgeDischargeIRMax = 8;
            couplingTest.JudgeDischargeIRMix = 9;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4,
                                0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8,
                                0, 0, 0, 9};

            var byteArr = couplingTest.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            try
            {
                // 범위 : 1 ~ 3
                couplingTest.SetMode = -1;
                couplingTest.SetMode = 4;
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