using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Title.ViewModels.Tests
{
    [TestClass()]
    public class TitleBarViewModelTests
    {
        TitleBarViewModel vm = new TitleBarViewModel();
        static DriveInfo drive;
        static string driveName = "C";
        static long tempMax;
        static long tempUse;
        static double tempPersent;

        [ClassInitialize]
        public static void ClassInitTest(TestContext context)
        {
            drive = new DriveInfo(driveName);
            tempMax = drive.TotalSize;
            tempUse = tempMax - drive.AvailableFreeSpace;
            tempPersent = Math.Round(tempUse / (double)drive.AvailableFreeSpace * 100, 2);
        }

        [TestMethod("Case1-1 용량검사 정상")]
        public void DriveVolumeCalcTest1()
        {
            bool isPass = vm.DriveVolumeCalc(driveName, out long max, out long use, out double persent);

            if (tempMax != max) isPass = false;  // 최대 용량 검사
            if (tempUse != use) isPass = false;  // 사용 중인 용량 검사
            if (tempPersent != persent) isPass = false;  // 퍼센트 변환

            Console.WriteLine($"{isPass}");
            Assert.AreEqual(true, isPass);
        }

        [TestMethod("Case1-2 용량검사 비정상(없는 드라이브명)")]
        public void DriveVolumeCalcTest2()
        {
            bool isPass = vm.DriveVolumeCalc("Z", out long max, out long use, out double persent);

            Console.WriteLine($"{isPass}");
            Assert.AreEqual(false, isPass);
        }

        [TestMethod("Case1-3 용량검사 비정상(이상한 드라이브명)")]
        public void DriveVolumeCalcTest3()
        {
            bool isPass = vm.DriveVolumeCalc("1", out long max, out long use, out double persent);

            Console.WriteLine($"{isPass}");
            Assert.AreEqual(false, isPass);
        }
    }
}