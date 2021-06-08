using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class RecipyProtectTests
    {
        static RecipyProtect recipyProtect;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            recipyProtect = new RecipyProtect();
        }


        [ClassCleanup]
        public static void ClassCleanTest()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod("Case1-1 생성자 테스트 - 스페어 배열 - 6 반환")]
        public void RecipyProtectTest()
        {
            PrivateObject po = new PrivateObject(typeof(RecipyProtect));

            int[] test = (int[])po.GetField("spare");

            Assert.AreEqual(6, test.Length);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 48 반환")]
        public void SizeOfTest()
        {
            int size = recipyProtect.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(48, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            recipyProtect.VoltUpperLimite = 1;
            recipyProtect.VoltLowerLimite = 2;
            recipyProtect.CurrUpperLimite = 3;
            recipyProtect.CurrLowerLimite = 4;
            recipyProtect.CapaUpperLimite = 5;
            recipyProtect.TempUpperLimite = 6;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3,
                                0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6,
                                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };

            var byteArr = recipyProtect.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }
    }
}