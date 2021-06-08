using Prism.Commands;
using Prism.Mvvm;
using SettingModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.ViewModels
{
    public class SafetyConditionPageViewModel : BindableBase
    {
        public bool CellOverCharEnable
        {
            get { return SafetySetting.cellOverCharEnable; }
            set { SetProperty(ref SafetySetting.cellOverCharEnable, value); }
        }
        public int CellOverCharLimit
        {
            get { return SafetySetting.cellOverCharLimit; }
            set { SetProperty(ref SafetySetting.cellOverCharLimit, value); }
        }

        public bool CellOverDischarEnable
        {
            get { return SafetySetting.cellOverDischarEnable; }
            set { SetProperty(ref SafetySetting.cellOverDischarEnable, value); }
        }
        public int CellOverDischarLimit
        {
            get { return SafetySetting.cellOverDischarLimit; }
            set { SetProperty(ref SafetySetting.cellOverDischarLimit, value); }
        }

        public bool CellOverTempEnable
        {
            get { return SafetySetting.cellOverTempEnable; }
            set { SetProperty(ref SafetySetting.cellOverTempEnable, value); }
        }
        public int CellOverTempLimit
        {
            get { return SafetySetting.cellOverTempLimit; }
            set { SetProperty(ref SafetySetting.cellOverTempLimit, value); }
        }

        public DelegateCommand SaveCommand { get; set; }

        public SafetyConditionPageViewModel()
        {
            SaveCommand = new DelegateCommand(OnSettingSave);
        }

        private void OnSettingSave()
        {
            // ini 파일의 정보 쓰기
            SafetySetting.WriteSetting();
        }
    }
}
