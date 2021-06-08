using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclerModule.EventArgsClass
{
    public class ConnectEventArg : EventArgs
    {
        public bool IsConnect { get; set; }

        public ConnectEventArg(bool isConnect)
        {
            IsConnect = isConnect;
        }
    }
}
