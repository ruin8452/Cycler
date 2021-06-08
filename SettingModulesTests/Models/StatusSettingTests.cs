using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models.Tests
{
    [TestClass()]
    public class StatusSettingTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //// 테스트 함수
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod()]
        public void WriteSettingTest()
        {
            StatusSetting.disconnFt = "White";
            StatusSetting.WriteSetting();
            StatusSetting.disconnFt = "";
            StatusSetting.ReadSetting();

            Console.WriteLine($"예상값 : White");
            Console.WriteLine($"결과값 : {StatusSetting.disconnFt}");
            Assert.AreEqual("White", StatusSetting.disconnFt);
        }
    }
}