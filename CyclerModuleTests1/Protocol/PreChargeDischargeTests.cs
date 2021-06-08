using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class PreChargeDischargeTests
    {
        static PreChargeDischarge preChargeDischarge;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            preChargeDischarge = new PreChargeDischarge();
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod("Case1-1 생성자 테스트 - 객체생성 여뷰 - null이 아님")]
        public void StepRecipyTest()
        {
            Assert.IsNotNull(preChargeDischarge);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 16 반환")]
        public void SizeOfTest()
        {
            int size = preChargeDischarge.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(32, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            preChargeDischarge.SetMode = 1;
            preChargeDischarge.SetVolt = 2;
            preChargeDischarge.SetCurr = 3;
            preChargeDischarge.SetEndTIme = 4;
            preChargeDischarge.SetEndVolt = 5;
            preChargeDischarge.SetEndCurr = 6;
            preChargeDischarge.VoltLimit = 7;
            preChargeDischarge.CurrLimit = 8;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4,
                                0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8 };

            var byteArr = preChargeDischarge.ToByteArray();

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
                preChargeDischarge.SetMode = 0;
                preChargeDischarge.SetMode = 3;
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