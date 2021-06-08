using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class SafetyConditionTests
    {
        static SafetyCondition safetyCondition;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            safetyCondition = new SafetyCondition();
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
            Assert.IsNotNull(safetyCondition);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 24 반환")]
        public void SizeOfTest()
        {
            int size = safetyCondition.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(24, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            safetyCondition.UseOverCharge = 1;
            safetyCondition.OverChargeVolt = 2;
            safetyCondition.UseOverDischarge = 1;
            safetyCondition.OverDischargeVolt = 4;
            safetyCondition.UseOverTemp = 1;
            safetyCondition.OverTemp = 6;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1,
                                0, 0, 0, 4, 0, 0, 0, 1, 0, 0, 0, 6, };

            var byteArr = safetyCondition.ToByteArray();

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
                safetyCondition.UseOverCharge = -1;
                safetyCondition.UseOverCharge = 2;
                // 범위 : 0 ~ 1
                safetyCondition.UseOverDischarge = -1;
                safetyCondition.UseOverDischarge = 2;
                // 범위 : 0 ~ 1
                safetyCondition.UseOverTemp = -1;
                safetyCondition.UseOverTemp = 2;
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