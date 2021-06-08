using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models
{
    public class ChSettingList : BindableBase
    {
        int no;
        public int No
        {
            get { return no; }
            set { SetProperty(ref no, value); }
        }

        string chName;
        public string ChName
        {
            get { return chName; }
            set { SetProperty(ref chName, value); }
        }

        int chamberGroup;
        public int ChamberGroup
        {
            get { return chamberGroup; }
            set { SetProperty(ref chamberGroup, value); }
        }

        string ip;
        public string IP
        {
            get { return ip; }
            set { SetProperty(ref ip, value); }
        }

        int port;
        public int Port
        {
            get { return port; }
            set { SetProperty(ref port, value); }
        }

        bool enable;
        public bool Enable
        {
            get { return enable; }
            set { SetProperty(ref enable, value); }
        }
    }
}
