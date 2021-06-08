using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CyclerModule.Protocol.Tests
{
    [TestClass()]
    public class PatternFileInfoTests
    {
        static PatternFileInfo patternFileInfo;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 클래스 초기화
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            patternFileInfo = new PatternFileInfo();
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
            Assert.IsNotNull(patternFileInfo);
        }

        [TestMethod("Case2-1 클래스 사이즈 테스트 - 사이즈 계산 - 24 반환")]
        public void SizeOfTest()
        {
            const int PATTERN_CNT = 6;

            patternFileInfo.OverTemp = new Pattern[PATTERN_CNT];
            int size = patternFileInfo.SizeOf();

            int expected = 20 + 8 * PATTERN_CNT;

            Console.WriteLine($"예상값 : {expected}");
            Console.WriteLine($"결과값 : {size}");
            Assert.AreEqual(expected, size);
        }

        [TestMethod("Case3-1 바이트 배열 생성 테스트 - 통신용 배열 작성 - 배열 반환")]
        public void ToByteArrayTest()
        {
            const int PATTERN_CNT = 1;
            patternFileInfo.OverTemp = new Pattern[PATTERN_CNT];

            patternFileInfo.StepNo = 1;
            patternFileInfo.PatternStartIndex = 2;
            patternFileInfo.PatternCount = 3;
            patternFileInfo.PatternMode = 2;
            patternFileInfo.PatternEnd = 1;

            patternFileInfo.OverTemp[0].PatternTime = 6;
            patternFileInfo.OverTemp[0].PatternValue = 7;

            byte[] tempByte = { 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3,
                                0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 6, 0, 0, 0, 7 };

            var byteArr = patternFileInfo.ToByteArray();

            Console.WriteLine($"예상값 : {string.Join(" ", tempByte)}");
            Console.WriteLine($"결과값 : {string.Join(" ", byteArr)}");
            CollectionAssert.AreEqual(tempByte, byteArr);
        }

        [TestMethod("Case3-2 바이트 배열 생성 테스트 - 스텝 정보 미삽입 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest1()
        {
            patternFileInfo.OverTemp = null;
            var byteArr = patternFileInfo.ToByteArray();
        }

        [TestMethod("Case3-3 바이트 배열 생성 테스트 - 스텝 정보 개수 0개 - 에러처리")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToByteArrayTest2()
        {
            patternFileInfo.OverTemp = new Pattern[0];
            var byteArr = patternFileInfo.ToByteArray();
        }

        [TestMethod("Case4-1 유효 데이터 범위 테스트 - 범위 밖의 데이터 삽입")]
        public void DataValidityTest()
        {
            try
            {
                // 범위 : 1 ~ 50000
                patternFileInfo.PatternStartIndex = 0;
                patternFileInfo.PatternStartIndex = 50001;
                // 범위 : 0 ~ 50000
                patternFileInfo.PatternCount = -1;
                patternFileInfo.PatternCount = 50001;
                // 범위 : 1 ~ 2
                patternFileInfo.PatternMode = 0;
                patternFileInfo.PatternMode = 3;
                // 범위 : 0 ~ 1
                patternFileInfo.PatternEnd = -1;
                patternFileInfo.PatternEnd = 2;
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