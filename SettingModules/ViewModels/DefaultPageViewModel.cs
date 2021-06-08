using Prism.Commands;
using Prism.Mvvm;
using SettingModules.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SettingModules.ViewModels
{
    public class DefaultPageViewModel : BindableBase
    {
        public List<string> ColorList { get; set; }


        #region 연결끊김 항목
        public string DisconnFt
        {
            get { return StatusSetting.disconnFt; }
            set { SetProperty(ref StatusSetting.disconnFt, value); }
        }
        public string DisconnBg
        {
            get { return StatusSetting.disconnBg; }
            set { SetProperty(ref StatusSetting.disconnBg, value); }
        }
        #endregion

        #region 대기 항목
        public string WaitFt
        {
            get { return StatusSetting.waitFt; }
            set { SetProperty(ref StatusSetting.waitFt, value); }
        }
        public string WaitBg
        {
            get { return StatusSetting.waitBg; }
            set { SetProperty(ref StatusSetting.waitBg, value); }
        }
        #endregion

        #region 정지 항목
        public string StopFt
        {
            get { return StatusSetting.stopFt; }
            set { SetProperty(ref StatusSetting.stopFt, value); }
        }
        public string StopBg
        {
            get { return StatusSetting.stopBg; }
            set { SetProperty(ref StatusSetting.stopBg, value); }
        }
        #endregion

        #region 비상정지 항목
        public string EmgFt
        {
            get { return StatusSetting.emgFt; }
            set { SetProperty(ref StatusSetting.emgFt, value); }
        }
        public string EmgBg
        {
            get { return StatusSetting.emgBg; }
            set { SetProperty(ref StatusSetting.emgBg, value); }
        }
        #endregion

        #region 초기화 항목
        public string InitFt
        {
            get { return StatusSetting.initFt; }
            set { SetProperty(ref StatusSetting.initFt, value); }
        }
        public string InitBg
        {
            get { return StatusSetting.initBg; }
            set { SetProperty(ref StatusSetting.initBg, value); }
        }
        #endregion

        #region 일시정지 항목
        public string PauseFt
        {
            get { return StatusSetting.pauseFt; }
            set { SetProperty(ref StatusSetting.pauseFt, value); }
        }
        public string PauseBg
        {
            get { return StatusSetting.pauseBg; }
            set { SetProperty(ref StatusSetting.pauseBg, value); }
        }
        #endregion

        #region 챔버 온도대기 항목
        public string ChamTempFt
        {
            get { return StatusSetting.chamTempFt; }
            set { SetProperty(ref StatusSetting.chamTempFt, value); }
        }
        public string ChamTempBg
        {
            get { return StatusSetting.chamTempBg; }
            set { SetProperty(ref StatusSetting.chamTempBg, value); }
        }
        #endregion

        #region 공정종료 항목
        public string FinishFt
        {
            get { return StatusSetting.finishFt; }
            set { SetProperty(ref StatusSetting.finishFt, value); }
        }
        public string FinishBg
        {
            get { return StatusSetting.finishBg; }
            set { SetProperty(ref StatusSetting.finishBg, value); }
        }
        #endregion

        #region 공정정보 전송 항목
        public string ProcessInfoSendFt
        {
            get { return StatusSetting.processInfoSendFt; }
            set { SetProperty(ref StatusSetting.processInfoSendFt, value); }
        }
        public string ProcessInfoSendBg
        {
            get { return StatusSetting.processInfoSendBg; }
            set { SetProperty(ref StatusSetting.processInfoSendBg, value); }
        }
        #endregion

        #region CC 충전 항목
        public string CcChargeFt
        {
            get { return StatusSetting.ccChargeFt; }
            set { SetProperty(ref StatusSetting.ccChargeFt, value); }
        }
        public string CcChargeBg
        {
            get { return StatusSetting.ccChargeBg; }
            set { SetProperty(ref StatusSetting.ccChargeBg, value); }
        }
        #endregion

        #region CC/CV 충전 항목
        public string CccvChargeFt
        {
            get { return StatusSetting.cccvChargeFt; }
            set { SetProperty(ref StatusSetting.cccvChargeFt, value); }
        }
        public string CccvChargeBg
        {
            get { return StatusSetting.cccvChargeBg; }
            set { SetProperty(ref StatusSetting.cccvChargeBg, value); }
        }
        #endregion

        #region CP 충전 항목
        public string CpChargeFt
        {
            get { return StatusSetting.cpChargeFt; }
            set { SetProperty(ref StatusSetting.cpChargeFt, value); }
        }
        public string CpChargeBg
        {
            get { return StatusSetting.cpChargeBg; }
            set { SetProperty(ref StatusSetting.cpChargeBg, value); }
        }
        #endregion

        #region CR 충전 항목
        public string CrChargeFt
        {
            get { return StatusSetting.crChargeFt; }
            set { SetProperty(ref StatusSetting.crChargeFt, value); }
        }
        public string CrChargeBg
        {
            get { return StatusSetting.crChargeBg; }
            set { SetProperty(ref StatusSetting.crChargeBg, value); }
        }
        #endregion

        #region CC 방전 항목
        public string CcDischargeFt
        {
            get { return StatusSetting.ccDischargeFt; }
            set { SetProperty(ref StatusSetting.ccDischargeFt, value); }
        }
        public string CcDischargeBg
        {
            get { return StatusSetting.ccDischargeBg; }
            set { SetProperty(ref StatusSetting.ccDischargeBg, value); }
        }
        #endregion

        #region CC/CV 방전 항목
        public string CccvDischargeFt
        {
            get { return StatusSetting.cccvDischargeFt; }
            set { SetProperty(ref StatusSetting.cccvDischargeFt, value); }
        }
        public string CccvDischargeBg
        {
            get { return StatusSetting.cccvDischargeBg; }
            set { SetProperty(ref StatusSetting.cccvDischargeBg, value); }
        }
        #endregion

        #region CP 방전 항목
        public string CpDischargeFt
        {
            get { return StatusSetting.cpDischargeFt; }
            set { SetProperty(ref StatusSetting.cpDischargeFt, value); }
        }
        public string CpDischargeBg
        {
            get { return StatusSetting.cpDischargeBg; }
            set { SetProperty(ref StatusSetting.cpDischargeBg, value); }
        }
        #endregion

        #region CR 방전 항목
        public string CrDischargeFt
        {
            get { return StatusSetting.crDischargeFt; }
            set { SetProperty(ref StatusSetting.crDischargeFt, value); }
        }
        public string CrDischargeBg
        {
            get { return StatusSetting.crDischargeBg; }
            set { SetProperty(ref StatusSetting.crDischargeBg, value); }
        }
        #endregion

        #region 휴지 항목
        public string RestFt
        {
            get { return StatusSetting.restFt; }
            set { SetProperty(ref StatusSetting.restFt, value); }
        }
        public string RestBg
        {
            get { return StatusSetting.restBg; }
            set { SetProperty(ref StatusSetting.restBg, value); }
        }
        #endregion

        #region OCV 항목
        public string OcvFt
        {
            get { return StatusSetting.ocvFt; }
            set { SetProperty(ref StatusSetting.ocvFt, value); }
        }
        public string OcvBg
        {
            get { return StatusSetting.ocvBg; }
            set { SetProperty(ref StatusSetting.ocvBg, value); }
        }
        #endregion

        #region 간이충방전 항목
        public string PreCDFt
        {
            get { return StatusSetting.preCDFt; }
            set { SetProperty(ref StatusSetting.preCDFt, value); }
        }
        public string PreCDBg
        {
            get { return StatusSetting.preCDBg; }
            set { SetProperty(ref StatusSetting.preCDBg, value); }
        }
        #endregion

        #region 밸런싱 항목
        public string BalancingFt
        {
            get { return StatusSetting.balancingFt; }
            set { SetProperty(ref StatusSetting.balancingFt, value); }
        }
        public string BalancingBg
        {
            get { return StatusSetting.balancingBg; }
            set { SetProperty(ref StatusSetting.balancingBg, value); }
        }
        #endregion

        #region 체결 테스트 항목
        public string ConnTestFt
        {
            get { return StatusSetting.connTestFt; }
            set { SetProperty(ref StatusSetting.connTestFt, value); }
        }
        public string ConnTestBg
        {
            get { return StatusSetting.connTestBg; }
            set { SetProperty(ref StatusSetting.connTestBg, value); }
        }
        #endregion

        #region 캘 항목
        public string CalFt
        {
            get { return StatusSetting.calFt; }
            set { SetProperty(ref StatusSetting.calFt, value); }
        }
        public string CalBg
        {
            get { return StatusSetting.calBg; }
            set { SetProperty(ref StatusSetting.calBg, value); }
        }
        #endregion

        #region 패턴 항목
        public string PatternFt
        {
            get { return StatusSetting.patternFt; }
            set { SetProperty(ref StatusSetting.patternFt, value); }
        }
        public string PatternBg
        {
            get { return StatusSetting.patternBg; }
            set { SetProperty(ref StatusSetting.patternBg, value); }
        }
        #endregion

        #region 데이터 체크 항목
        public string DataCheckFt
        {
            get { return StatusSetting.dataCheckFt; }
            set { SetProperty(ref StatusSetting.dataCheckFt, value); }
        }
        public string DataCheckBg
        {
            get { return StatusSetting.dataCheckBg; }
            set { SetProperty(ref StatusSetting.dataCheckBg, value); }
        }
        #endregion


        #region OCV 스텝 항목
        public string OcvStepFt
        {
            get { return StatusSetting.ocvStepFt; }
            set { SetProperty(ref StatusSetting.ocvStepFt, value); }
        }
        public string OcvStepBg
        {
            get { return StatusSetting.ocvStepBg; }
            set { SetProperty(ref StatusSetting.ocvStepBg, value); }
        }
        #endregion

        #region 충전 스텝 항목
        public string ChargeStepFt
        {
            get { return StatusSetting.chargeStepFt; }
            set { SetProperty(ref StatusSetting.chargeStepFt, value); }
        }
        public string ChargeStepBg
        {
            get { return StatusSetting.chargeStepBg; }
            set { SetProperty(ref StatusSetting.chargeStepBg, value); }
        }
        #endregion

        #region 방전 스텝 항목
        public string DischargeStepFt
        {
            get { return StatusSetting.dischargeStepFt; }
            set { SetProperty(ref StatusSetting.dischargeStepFt, value); }
        }
        public string DischargeStepBg
        {
            get { return StatusSetting.dischargeStepBg; }
            set { SetProperty(ref StatusSetting.dischargeStepBg, value); }
        }
        #endregion

        #region 휴지 스텝 항목
        public string RestStepFt
        {
            get { return StatusSetting.restStepFt; }
            set { SetProperty(ref StatusSetting.restStepFt, value); }
        }
        public string RestStepBg
        {
            get { return StatusSetting.restStepBg; }
            set { SetProperty(ref StatusSetting.restStepBg, value); }
        }
        #endregion

        #region 패턴 스텝 항목
        public string PatternStepFt
        {
            get { return StatusSetting.patternStepFt; }
            set { SetProperty(ref StatusSetting.patternStepFt, value); }
        }
        public string PatternStepBg
        {
            get { return StatusSetting.patternStepBg; }
            set { SetProperty(ref StatusSetting.patternStepBg, value); }
        }
        #endregion

        #region 임피던스 스텝 항목
        public string ImpedanceStepFt
        {
            get { return StatusSetting.impedanceStepFt; }
            set { SetProperty(ref StatusSetting.impedanceStepFt, value); }
        }
        public string ImpedanceStepBg
        {
            get { return StatusSetting.impedanceStepBg; }
            set { SetProperty(ref StatusSetting.impedanceStepBg, value); }
        }
        #endregion

        #region 사이클 스텝 항목
        public string CycleStepFt
        {
            get { return StatusSetting.cycleStepFt; }
            set { SetProperty(ref StatusSetting.cycleStepFt, value); }
        }
        public string CycleStepBg
        {
            get { return StatusSetting.cycleStepBg; }
            set { SetProperty(ref StatusSetting.cycleStepBg, value); }
        }
        #endregion

        #region 루프 스텝 항목
        public string LoopStepFt
        {
            get { return StatusSetting.loopStepFt; }
            set { SetProperty(ref StatusSetting.loopStepFt, value); }
        }
        public string LoopStepBg
        {
            get { return StatusSetting.loopStepBg; }
            set { SetProperty(ref StatusSetting.loopStepBg, value); }
        }
        #endregion

        #region 종료 스텝 항목
        public string EndStepFt
        {
            get { return StatusSetting.endStepFt; }
            set { SetProperty(ref StatusSetting.endStepFt, value); }
        }
        public string EndStepBg
        {
            get { return StatusSetting.endStepBg; }
            set { SetProperty(ref StatusSetting.endStepBg, value); }
        }
        #endregion


        #region 전압 항목
        public int VoltUnitIndex
        {
            get { return StatusSetting.voltUnitIndex; }
            set { SetProperty(ref StatusSetting.voltUnitIndex, value); }
        }
        public int VoltRoundIndex
        {
            get { return StatusSetting.voltRoundIndex; }
            set { SetProperty(ref StatusSetting.voltRoundIndex, value); }
        }
        #endregion

        #region 전류 항목
        public int CurrUnitIndex
        {
            get { return StatusSetting.currUnitIndex; }
            set { SetProperty(ref StatusSetting.currUnitIndex, value); }
        }
        public int CurrRoundIndex
        {
            get { return StatusSetting.currRoundIndex; }
            set { SetProperty(ref StatusSetting.currRoundIndex, value); }
        }
        #endregion

        #region 용량 항목
        public int CapaUnitIndex
        {
            get { return StatusSetting.capaUnitIndex; }
            set { SetProperty(ref StatusSetting.capaUnitIndex, value); }
        }
        public int CapaRoundIndex
        {
            get { return StatusSetting.capaRoundIndex; }
            set { SetProperty(ref StatusSetting.capaRoundIndex, value); }
        }
        #endregion

        #region 전력 항목
        public int PowerUnitIndex
        {
            get { return StatusSetting.powerUnitIndex; }
            set { SetProperty(ref StatusSetting.powerUnitIndex, value); }
        }
        public int PowerRoundIndex
        {
            get { return StatusSetting.powerRoundIndex; }
            set { SetProperty(ref StatusSetting.powerRoundIndex, value); }
        }
        #endregion

        #region 전력량 항목
        public int WatthourUnitIndex
        {
            get { return StatusSetting.watthourUnitIndex; }
            set { SetProperty(ref StatusSetting.watthourUnitIndex, value); }
        }
        public int WatthourRoundIndex
        {
            get { return StatusSetting.watthourRoundIndex; }
            set { SetProperty(ref StatusSetting.watthourRoundIndex, value); }
        }
        #endregion

        #region 임피던스 항목
        public int ResiUnitIndex
        {
            get { return StatusSetting.resiUnitIndex; }
            set { SetProperty(ref StatusSetting.resiUnitIndex, value); }
        }
        public int ResiRoundIndex
        {
            get { return StatusSetting.resiRoundIndex; }
            set { SetProperty(ref StatusSetting.resiRoundIndex, value); }
        }
        #endregion


        #region 데이터 갱신주기
        public int DataRenuwal
        {
            get { return StatusSetting.dataRenuwal; }
            set { SetProperty(ref StatusSetting.dataRenuwal, value); }
        }
        #endregion

        #region 모니터링 폰트 크기
        public int MoniFontSize
        {
            get { return StatusSetting.moniFontSize; }
            set { SetProperty(ref StatusSetting.moniFontSize, value); }
        }
        #endregion

        private ObservableCollection<ChMoniList> _moniSetter = new ObservableCollection<ChMoniList>();
        public ObservableCollection<ChMoniList> MoniSetter
        {
            get { return _moniSetter; }
            set { SetProperty(ref _moniSetter, value); }
        }

        public DelegateCommand SaveCommand { get; set; }

        public delegate Point GetPosition(IInputElement element);

        public DefaultPageViewModel()
        {
            // 색상 리스트 추출
            ColorList = new List<string>();
            foreach (var name in typeof(Colors).GetProperties())
                ColorList.Add(name.Name);

            // 채널 모니터링 목록 작성
            MoniSetter.AddRange(StatusSetting.chMoniList);

            SaveCommand = new DelegateCommand(OnSettingSave);
        }

        private void OnSettingSave()
        {
            // ini 파일의 정보 쓰기
            StatusSetting.WriteSetting();
        }
    }
}
