using CyclerModule.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CalDmmTests
    {
        static CalDmm calDmm;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            calDmm = new CalDmm();
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
            Assert.IsNotNull(calDmm);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 12 반환")]
        public void SizeOfTest()
        {
            int size = calDmm.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(12, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            calDmm.SetVolt = 1;
            calDmm.SetCurr = 2;
            calDmm.DmmValue = 3;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3 };

            var byteArr = calDmm.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod()]
        public void ToClassTest()
        {
            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3 };

            bool result = calDmm.ToClass(tempByte);

            Console.WriteLine($"Result : {result}");
            Assert.AreEqual(true, result);
        }
    }
}