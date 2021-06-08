using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class ChamberCommLogTests
    {
        static ChamberCommLog chamberCommLog;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            chamberCommLog = new ChamberCommLog();
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
            Assert.IsNotNull(chamberCommLog);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 12 반환")]
        public void SizeOfTest()
        {
            int size = chamberCommLog.SizeOf();

            int expected = 12;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            chamberCommLog.LogSpec = 1;
            chamberCommLog.LogMode = 2;
            chamberCommLog.SetValue = 3;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3 };

            var byteArr = chamberCommLog.ToByteArray();

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
                chamberCommLog.LogSpec = 0;
                chamberCommLog.LogSpec = 3;
                // 범위 : 1 ~ 5
                chamberCommLog.LogMode = 0;
                chamberCommLog.LogMode = 6;
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