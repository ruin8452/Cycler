using CommModules;
using CyclerModule;
using CyclerModule.Code;
using MonitorModules.Models;
using Prism.Commands;
using Prism.Mvvm;
using SettingModules.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonitorModules.ViewModels
{
    public class MonitorPageViewModel : BindableBase
    {
        System.Timers.Timer ConnectTimer = new System.Timers.Timer();

        public ObservableCollection<ChMoniList> ColumnInfo { get; private set; }
        public ObservableCollection<Chamber> ChamberObj { get; private set; }
        public ObservableCollection<Cycler> CyclerObj { get; private set; }

        // Command
        public DelegateCommand LoadedCommand { get; set; }

        public DelegateCommand RecipyRun { get; set; }

        public MonitorPageViewModel()
        {
            // 열 정보 세팅
            ColumnInfo = new ObservableCollection<ChMoniList>((ChMoniList[])StatusSetting.chMoniList.Clone());
            for (int i = 0; i < ColumnInfo.Count; i++)
            {
                if (!ColumnInfo[i].Enable) ColumnInfo[i].Width = 0;

                if (ColumnInfo[i].Name == "전압")           ColumnInfo[i].Name += $"[{StatusSetting.GetVoltUnit()}]";
                else if (ColumnInfo[i].Name == "전류")      ColumnInfo[i].Name += $"[{StatusSetting.GetCurrUnit()}]";
                else if (ColumnInfo[i].Name == "용량")      ColumnInfo[i].Name += $"[{StatusSetting.GetCapaUnit()}]";
                else if (ColumnInfo[i].Name == "전력")      ColumnInfo[i].Name += $"[{StatusSetting.GetPowerUnit()}]";
                else if (ColumnInfo[i].Name == "전력량")    ColumnInfo[i].Name += $"[{StatusSetting.GetWatthourUnit()}]";
                else if (ColumnInfo[i].Name == "저항")      ColumnInfo[i].Name += $"[{StatusSetting.GetResiUnit()}]";
                else if (ColumnInfo[i].Name == "셀 온도")   ColumnInfo[i].Name += $"[℃]";
                else if (ColumnInfo[i].Name == "챔버 온도") ColumnInfo[i].Name += $"[℃]";
            }

            // 사이클러 정보 세팅
            CyclerObj = new ObservableCollection<Cycler>();
            for (int i = 0; i < EquipmentSetting.ChCount; i++)
            {
                ICommModule commModule = new TcpModule(EquipmentSetting.chSettingList[i].IP, EquipmentSetting.chSettingList[i].Port);
                Cycler cycler = new Cycler(commModule)
                {
                    No = "CH" + (i + 1)
                };
                CyclerObj.Add(cycler);
            }

            // 챔버 정보 세팅
            ChamberObj = new ObservableCollection<Chamber>();
            for (int i = 0; i < EquipmentSetting.chamberCount; i++)
            {
                Chamber chamber = new Chamber();
                chamber.No = i + 1;
                chamber.State = "연결 끊김";
                ChamberObj.Add(chamber);
            }

            // 접속 타이머 설정
            ConnectTimer.Interval = 1000;
            ConnectTimer.Elapsed += AutoConnecter;
            //ConnectTimer.Start();

            LoadedCommand = new DelegateCommand(OnLoaded);
            RecipyRun = new DelegateCommand(Run);
        }

        private void AutoConnecter(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                if (!CyclerObj[i].IsConnect)
                    CyclerObj[i].Connect();
            }
        }

        private void OnLoaded()
        {
            Connector();
        }

        private void Run()
        {

        }

        private async void Connector()
        {
            bool pingResult;
            Ping pingSender = new Ping();

            for (int i = 0; i < CyclerObj.Count; i++)
            {
                Task<PingReply> replyTask = pingSender.SendPingAsync(EquipmentSetting.chSettingList[i].IP, 100);
                PingReply reply = await replyTask;

                pingResult = reply.Status == IPStatus.Success;

                if (pingResult)
                {
                    CyclerObj[i].Connect();
                    Thread.Sleep(50);
                    CyclerObj[i].MoniStart();
                }
            }
        }
    }
}