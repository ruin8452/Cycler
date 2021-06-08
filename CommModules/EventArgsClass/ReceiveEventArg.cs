using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommModules.EventArgsClass
{
    public class ReceiveEventArg
    {
        public byte[] ReceiveArray { get; set; }

        public ReceiveEventArg(byte[] array)
        {
            ReceiveArray = array;
        }
    }
}
