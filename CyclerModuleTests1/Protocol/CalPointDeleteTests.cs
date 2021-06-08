using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CalPointDeleteTests
    {
        static CalPointDelete calPointDelete;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            calPointDelete = new CalPointDelete();
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
            Assert.IsNotNull(calPointDelete);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 8 반환")]
        public void SizeOfTest()
        {
            int size = calPointDelete.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(8, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            calPointDelete.Mode = 1;
            calPointDelete.PointValue = 2;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2 };

            var byteArr = calPointDelete.ToByteArray();

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
                calPointDelete.Mode = 0;
                calPointDelete.Mode = 3;
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