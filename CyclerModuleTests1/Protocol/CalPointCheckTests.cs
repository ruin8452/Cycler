using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class CalPointCheckTests
    {
        static CalPointCheck calPointCheck;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            calPointCheck = new CalPointCheck();
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
            PrivateObject po = new PrivateObject(typeof(CalPointCheck));

            int[] test1 = (int[])po.GetField("VoltPoint");
            int[] test2 = (int[])po.GetField("CurrPoint");

            Console.WriteLine($"VoltPoint 개수 : 예상값 - 10개 / 결과값 - {test1.Length}");
            Console.WriteLine($"CurrPoint 개수 : 예상값 - 20개 / 결과값 - {test2.Length}");

            Assert.AreEqual(10, test1.Length);
            Assert.AreEqual(20, test2.Length);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 120 반환")]
        public void SizeOfTest()
        {
            int size = calPointCheck.SizeOf();

            Console.WriteLine($"Result : {size}");
            Assert.AreEqual(120, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            calPointCheck.VoltPoint[0] = 1;
            calPointCheck.VoltPoint[1] = 2;
            calPointCheck.VoltPoint[2] = 3;
            calPointCheck.VoltPoint[3] = 4;
            calPointCheck.VoltPoint[4] = 5;
            calPointCheck.VoltPoint[5] = 6;
            calPointCheck.VoltPoint[6] = 7;
            calPointCheck.VoltPoint[7] = 8;
            calPointCheck.VoltPoint[8] = 9;
            calPointCheck.VoltPoint[9] = 10;

            calPointCheck.CurrPoint[0] = 11;
            calPointCheck.CurrPoint[1] = 12;
            calPointCheck.CurrPoint[2] = 13;
            calPointCheck.CurrPoint[3] = 14;
            calPointCheck.CurrPoint[4] = 15;
            calPointCheck.CurrPoint[5] = 16;
            calPointCheck.CurrPoint[6] = 17;
            calPointCheck.CurrPoint[7] = 18;
            calPointCheck.CurrPoint[8] = 19;
            calPointCheck.CurrPoint[9] = 20;
            calPointCheck.CurrPoint[10] = 21;
            calPointCheck.CurrPoint[11] = 22;
            calPointCheck.CurrPoint[12] = 23;
            calPointCheck.CurrPoint[13] = 24;
            calPointCheck.CurrPoint[14] = 25;
            calPointCheck.CurrPoint[15] = 26;
            calPointCheck.CurrPoint[16] = 27;
            calPointCheck.CurrPoint[17] = 28;
            calPointCheck.CurrPoint[18] = 29;
            calPointCheck.CurrPoint[19] = 30;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5,
                                0, 0, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8, 0, 0, 0, 9, 0, 0, 0, 10,
                                0, 0, 0, 11, 0, 0, 0, 12, 0, 0, 0, 13, 0, 0, 0, 14, 0, 0, 0, 15,
                                0, 0, 0, 16, 0, 0, 0, 17, 0, 0, 0, 18, 0, 0, 0, 19, 0, 0, 0, 20,
                                0, 0, 0, 21, 0, 0, 0, 22, 0, 0, 0, 23, 0, 0, 0, 24, 0, 0, 0, 25,
                                0, 0, 0, 26, 0, 0, 0, 27, 0, 0, 0, 28, 0, 0, 0, 29, 0, 0, 0, 30, };

            var byteArr = calPointCheck.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }
    }
}