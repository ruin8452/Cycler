using Prism.Commands;
using Prism.Interactivity;
using Prism.Mvvm;
using SettingModules.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SettingModules.ViewModels
{
    public class EquipmentPageViewModel : BindableBase
    {
        public string EquipmentName
        {
            get { return EquipmentSetting.equipmentName; }
            set { SetProperty(ref EquipmentSetting.equipmentName, value); }
        }


        public string BackupPcIp
        {
            get { return EquipmentSetting.backupPcIp; }
            set { SetProperty(ref EquipmentSetting.backupPcIp, value); }
        }
        public int BackupPcPort
        {
            get { return EquipmentSetting.backupPcPort; }
            set { SetProperty(ref EquipmentSetting.backupPcPort, value); }
        }

        public string CalIp
        {
            get { return EquipmentSetting.calIp; }
            set { SetProperty(ref EquipmentSetting.calIp, value); }
        }
        public int CalPort
        {
            get { return EquipmentSetting.calPort; }
            set { SetProperty(ref EquipmentSetting.calPort, value); }
        }

        public string TotalMoniPcIp
        {
            get { return EquipmentSetting.totalMoniPcIp; }
            set { SetProperty(ref EquipmentSetting.totalMoniPcIp, value); }
        }
        public int TotalMoniPcPort
        {
            get { return EquipmentSetting.totalMoniPcPort; }
            set { SetProperty(ref EquipmentSetting.totalMoniPcPort, value); }
        }


        public int ChamberCount
        {
            get { return EquipmentSetting.chamberCount; }
            set { SetProperty(ref EquipmentSetting.chamberCount, value); }
        }
        public double ChamberTimeScale
        {
            get { return EquipmentSetting.chamberTimeScale; }
            set { SetProperty(ref EquipmentSetting.chamberTimeScale, value); }
        }
        public double ChamberTempErrRate
        {
            get { return EquipmentSetting.chamberTempErrRate; }
            set { SetProperty(ref EquipmentSetting.chamberTempErrRate, value); }
        }
        public double ChamberDefTemp
        {
            get { return EquipmentSetting.chamberDefTemp; }
            set { SetProperty(ref EquipmentSetting.chamberDefTemp, value); }
        }
        public bool ChamberAutoEnd
        {
            get { return EquipmentSetting.chamberAutoEnd; }
            set { SetProperty(ref EquipmentSetting.chamberAutoEnd, value); }
        }

        private ObservableCollection<ChSettingList> _chSetter = new ObservableCollection<ChSettingList>();
        public ObservableCollection<ChSettingList> ChSetter
        {
            get { return _chSetter; }
            set { SetProperty(ref _chSetter, value); }
        }


        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ChAddCommand { get; set; }
        public DelegateCommand<object> ChDelCommand { get; set; }

        public EquipmentPageViewModel()
        {
            // 채널 모니터링 목록 작성
            ChSetter.AddRange(EquipmentSetting.chSettingList);

            SaveCommand = new DelegateCommand(OnSettingSave);
            ChAddCommand = new DelegateCommand(OnChListAdd);
            ChDelCommand = new DelegateCommand<object>(OnChListDel);
        }

        private void OnSettingSave()
        {
            // 채널정보 데이터 유효성 검사
            for (int i = 0; i < ChSetter.Count; i++)
            {
                if (ChSetter[i].ChamberGroup > EquipmentSetting.chamberCount)
                    ChSetter[i].ChamberGroup = EquipmentSetting.chamberCount;

                else if (ChSetter[i].ChamberGroup < 1)
                    ChSetter[i].ChamberGroup = 1;
            }

            // ini 파일의 정보 쓰기
            EquipmentSetting.WriteSetting();
        }

        private void OnChListAdd()
        {
            ChSettingList temp = new ChSettingList
            {
                No = ChSetter.Count + 1,
                ChName = "CH" + (ChSetter.Count + 1),
                ChamberGroup = 1,
                IP = "0.0.0.0",
                Port = 1000,
                Enable = true
            };

            ChSetter.Add(temp);
        }

        private void OnChListDel(object obj)
        {
            var coll = ((IList)obj).Cast<ChSettingList>();

            int selCount = coll.Count();
            for (int i = 0; i < selCount; i++)
                ChSetter.Remove(coll.ElementAt(0));
        }
    }
}