using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorModules.Models
{
    public class Chamber : BindableBase
    {
        /// <summary>
        /// 챔버 번호
        /// </summary>
        private int no;
        public int No
        {
            get { return no; }
            set { SetProperty(ref no, value); }
        }

        /// <summary>
        /// 챔버 연결 상태
        /// </summary>
        private string state;
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        /// <summary>
        /// 설정 온도
        /// </summary>
        private double setTemp;
        public double SetTemp
        {
            get { return setTemp; }
            set { SetProperty(ref setTemp, value); }
        }

        /// <summary>
        /// 현재 온도
        /// </summary>
        private double currTemp;
        public double CurrTemp
        {
            get { return currTemp; }
            set { SetProperty(ref currTemp, value); }
        }
    }
}
