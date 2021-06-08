using Microsoft.WindowsAPICodePack.Dialogs;
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
    public class DataPathPageViewModel : BindableBase
    {
        public string DataPath
        {
            get { return DataPathSetting.dataPath; }
            set { SetProperty(ref DataPathSetting.dataPath, value); }
        }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand PathSerchCommand { get; set; }

        public DataPathPageViewModel()
        {
            SaveCommand = new DelegateCommand(OnSettingSave);
            PathSerchCommand = new DelegateCommand(OnPathSerch);
        }

        private void OnSettingSave()
        {
            // ini 파일의 정보 쓰기
            DataPathSetting.WriteSetting();
        }

        private void OnPathSerch()
        {
            CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog
            {
                Title = "폴더 선택",
                InitialDirectory = DataPath,
                IsFolderPicker = true
            };
            openFileDialog.ShowDialog();
            DataPath = openFileDialog.FileName;
        }
    }
}
