using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclerModule.EventArgsClass
{
    public class CommEventArg : EventArgs
    {
        public byte[] CommArray { get; set; }

        public CommEventArg(byte[] array)
        {
            CommArray = array;
        }
    }
}
