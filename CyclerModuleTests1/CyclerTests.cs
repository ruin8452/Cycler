using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyclerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommModules;
using CommModules.CommManager;
using CyclerModule.Code;

namespace CyclerModule.Tests
{
    [TestClass()]
    public class CyclerTests
    {
        static Cycler cycler = new Cycler(new TcpModule("127.0.0.1", 9000));

        [TestMethod()]
        public void SendTest()
        {
            cycler.Send(ProtocolCommand.CAL_DMM, new byte[] { 1,2,3,4,5,6 });
            Assert.Fail();
        }
    }
}