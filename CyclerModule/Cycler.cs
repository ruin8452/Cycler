using CommModules;
using CommModules.CommManager;
using CyclerModule.Code;
using CyclerModule.EventArgsClass;
using CyclerModule.Protocol;
using Prism.Commands;
using Prism.Mvvm;
using SettingModules.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CyclerModule
{
    public class Cycler : BindableBase
    {
        const int STX = 0x7FFE7FFE;
        const int ETX = 0x7FFF7FFF;
        const int PROTOCOL_VER = 1;
        const int RETURN_CODE = 0;

        QueueComm commManager;

        System.Timers.Timer timer = new System.Timers.Timer();

        public CyclerStatus cyclerStatus = new CyclerStatus();
        public RecipyProtect recipyProtect = new RecipyProtect();
        public StepRecipy stepRecipy = new StepRecipy();
        public StepEnd_Protect stepEnd_Protect = new StepEnd_Protect();
        public ControlCommand controlCommand = new ControlCommand();
        public ChamberManualCtrl chamberManual = new ChamberManualCtrl();
        public PreChargeDischarge preCharDischar = new PreChargeDischarge();
        public CouplingTest couplingTest = new CouplingTest();
        public CalPointCheck calPointCheck = new CalPointCheck();
        public CalPointDelete calPointDel = new CalPointDelete();
        public CalOutputChange outputChange = new CalOutputChange();
        public CalDmm calDmm = new CalDmm();
        public CalMainRelay calMainRelay = new CalMainRelay();
        public SafetyCondition safetyCondition = new SafetyCondition();
        public PatternFileInfo patternFile = new PatternFileInfo();
        public ChamberGroupInfo chamberGroup = new ChamberGroupInfo();

        public StatusAnswer statusAnswer = new StatusAnswer();

        public event EventHandler<ConnectEventArg> ConnectEvent;
        public event EventHandler<CommEventArg> SendEvent;
        public event EventHandler<CommEventArg> ReceiveEvent;

        private bool isConnect = false;
        public bool IsConnect
        {
            get { return isConnect; }
            set { SetProperty(ref isConnect, value); }
        }

        private string stateBg;
        public string StateBg
        {
            get { return stateBg; }
            set { SetProperty(ref stateBg, value); }
        }
        private string stateFt;
        public string StateFt
        {
            get { return stateFt; }
            set { SetProperty(ref stateFt, value); }
        }


        private string no;
        public string No
        {
            get { return no; }
            set { SetProperty(ref no, value); }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        private string cyc_Repeat;
        public string Cyc_Repeat
        {
            get { return cyc_Repeat; }
            set { SetProperty(ref cyc_Repeat, value); }
        }

        private double volt;
        public double Volt
        {
            get { return volt; }
            set { SetProperty(ref volt, value); }
        }

        private double curr;
        public double Curr
        {
            get { return curr; }
            set { SetProperty(ref curr, value); }
        }

        private double capa;
        public double Capa
        {
            get { return capa; }
            set { SetProperty(ref capa, value); }
        }

        private double power;
        public double Power
        {
            get { return power; }
            set { SetProperty(ref power, value); }
        }

        private double watthour;
        public double Watthour
        {
            get { return watthour; }
            set { SetProperty(ref watthour, value); }
        }

        private double resi;
        public double Resi
        {
            get { return resi; }
            set { SetProperty(ref resi, value); }
        }

        private double cellTemp;
        public double CellTemp
        {
            get { return cellTemp; }
            set { SetProperty(ref cellTemp, value); }
        }

        private string cellState;
        public string CellState
        {
            get { return cellState; }
            set { SetProperty(ref cellState, value); }
        }

        private DateTime totalTime;
        public DateTime TotalTime
        {
            get { return totalTime; }
            set { SetProperty(ref totalTime, value); }
        }

        private string workName;
        public string WorkName
        {
            get { return workName; }
            set { SetProperty(ref workName, value); }
        }

        private string scheduleName;
        public string ScheduleName
        {
            get { return scheduleName; }
            set { SetProperty(ref scheduleName, value); }
        }

        private DateTime stepTime;
        public DateTime StepTime
        {
            get { return stepTime; }
            set { SetProperty(ref stepTime, value); }
        }

        private string stepInfo;
        public string StepInfo
        {
            get { return stepInfo; }
            set { SetProperty(ref stepInfo, value); }
        }

        private int totalStep;
        public int TotalStep
        {
            get { return totalStep; }
            set { SetProperty(ref totalStep, value); }
        }

        private int chamberId;
        public int ChamberId
        {
            get { return chamberId; }
            set { SetProperty(ref chamberId, value); }
        }

        private double chamberTemp;
        public double ChamberTemp
        {
            get { return chamberTemp; }
            set { SetProperty(ref chamberTemp, value); }
        }

        private DateTime nowTime;
        public DateTime NowTime
        {
            get { return nowTime; }
            set { SetProperty(ref nowTime, value); }
        }

        private string reservation;
        public string Reservation
        {
            get { return reservation; }
            set { SetProperty(ref reservation, value); }
        }

        private int commCode;

        public Cycler()
        {
            Init();
        }
        public Cycler(ICommModule commModule)
        {
            commManager = new QueueComm(commModule);
            commManager.ReceiveEvent += CommManager_ReceiveEvent;

            Init();

            timer.Interval = 1000;
            timer.Elapsed += (send, e) => { StatusRequest(); };
        }

        private void CommManager_ReceiveEvent(object sender, CommModules.EventArgsClass.ReceiveEventArg e)
        {
            byte[] receive = e.ReceiveArray;

            if (receive.Length == 0)
                return;

            OnReceiveEvent(new CommEventArg(receive));

            switch ((ProtocolCommand)PacketCheck(ref receive))
            {
                case ProtocolCommand.NONE_COMMAND:
                    break;
                case ProtocolCommand.CYCLER_STATUS:
                    cyclerStatus.ToClass(receive);

                    StateChange(cyclerStatus.State);

                    //Volt = cyclerStatus.Volt / 10000;
                    Volt += 0.325;
                    Curr = cyclerStatus.Curr / 10000;
                    Capa = cyclerStatus.Capacity / 10000;
                    Power = cyclerStatus.Power / 10000;
                    Watthour = cyclerStatus.Watthour / 10000;
                    Resi = cyclerStatus.Resistance / 10000;
                    CellTemp = cyclerStatus.Temperature / 10;
                    CellState = cyclerStatus.CellStatus.ToString();
                    break;
                case ProtocolCommand.RECIPY_PROTECT:
                    break;
                case ProtocolCommand.STEP_RECIPY:
                    break;
                case ProtocolCommand.STEP_END_PROTECT:
                    break;
                case ProtocolCommand.CONTROL:
                    CtrlCommand recvCmd = (CtrlCommand)BitConverter.ToInt32(receive, 0);
                    if (controlCommand.Command == recvCmd)
                    {

                    }
                    break;
                case ProtocolCommand.SET_CHAMBER:
                    break;
                case ProtocolCommand.PRE_CHARGE_DISCHARGE:
                    break;
                case ProtocolCommand.COUPLING_TEST:
                    break;
                case ProtocolCommand.CAL_POINT_CHECK:
                    calPointCheck.ToClass(receive);
                    break;
                case ProtocolCommand.CAL_POINT_DELETE:
                    int recvVal = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.CAL_OUTPUT_CHANGE:
                    int outMonVolt = BitConverter.ToInt32(receive, 0);
                    int outMonCurr = BitConverter.ToInt32(receive, 4);
                    break;
                case ProtocolCommand.CAL_DMM:
                    int dmmMonVolt = BitConverter.ToInt32(receive, 0);
                    int dmmMonCurr = BitConverter.ToInt32(receive, 4);
                    break;
                case ProtocolCommand.CAL_OUTPUT_STOP:
                    break;
                case ProtocolCommand.CAL_MAIN_RELAY:
                    int revMode = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.CAL_MAIN_DMM:
                    int val = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.PATTERN_FILE_INFO:
                    int stepNo = BitConverter.ToInt32(receive, 0);
                    int startIndex = BitConverter.ToInt32(receive, 4);
                    int count = BitConverter.ToInt32(receive, 8);
                    break;
                case ProtocolCommand.CHAMBER_GROUP_INFO:
                    break;
                case ProtocolCommand.COUPLING_TEST_RESULT:
                    Send(ProtocolCommand.COUPLING_TEST_RESULT);
                    break;
                case ProtocolCommand.GRAPH_DATA_SEND:
                    Send(ProtocolCommand.GRAPH_DATA_SEND);
                    break;
                case ProtocolCommand.STATUS_ANSWER:
                    int cmd = BitConverter.ToInt32(receive, 0);
                    statusAnswer.Command = cmd;
                    Send(ProtocolCommand.STATUS_ANSWER, statusAnswer.ToByteArray());
                    break;
            }
        }

        public void Init()
        {
            State = "연결끊김";
            StateBg = StatusSetting.disconnBg;
            StateFt = StatusSetting.disconnFt;

            Cyc_Repeat = "0/0";
            CellState = "-";
            TotalTime = new DateTime();
            WorkName = "-";
            ScheduleName = "-";
            StepTime = new DateTime();
            StepInfo = "0/0";
            NowTime = DateTime.Now;
            Reservation = "-";
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///// 접속 관련 함수
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void Connect()
        {
            if (!IsConnect)
                IsConnect = commManager.Connect();

            OnConnectEvent(new ConnectEventArg(IsConnect));
        }

        public async void ConnectAsync()
        {
            if (!IsConnect)
            {
                Task<bool> a = commManager.ConnectAsync();
                IsConnect = await a;
            }

            OnConnectEvent(new ConnectEventArg(IsConnect));
        }

        public void Disconnect()
        {
            if (IsConnect)
                IsConnect = !commManager.Disconnect();

            OnConnectEvent(new ConnectEventArg(IsConnect));
        }

        public void MoniStart()
        {
            timer.Start();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///// 통신 관련 함수
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 사이클러 통신 송신
        /// </summary>
        /// 
        /// <param name="cmd">프로토콜 커맨드</param>
        public void Send(ProtocolCommand cmd)
        {
            if (!isConnect)
                return;

            List<byte> byteStream = new List<byte>();

            // 데이터를 바이트 배열로 변환시 역순으로 뒤집혀서
            // 데이터 패킷 자체를 거꾸로 삽입한 후 한번에 뒤집기
            byteStream.AddRange(BitConverter.GetBytes(STX));
            byteStream.AddRange(BitConverter.GetBytes(PROTOCOL_VER));
            byteStream.AddRange(BitConverter.GetBytes(RETURN_CODE));
            byteStream.AddRange(BitConverter.GetBytes(4));
            byteStream.AddRange(BitConverter.GetBytes((int)cmd + 0x2000));
            byteStream.AddRange(BitConverter.GetBytes(ETX));

            byte[] temp = byteStream.ToArray();

            commManager.CommSendStack(temp, out commCode);
            OnSendEvent(new CommEventArg(temp));
        }

        /// <summary>
        /// 사이클러 통신 송신
        /// </summary>
        /// 
        /// <param name="cmd">프로토콜 커맨드</param>
        /// <param name="sendData">프로토콜 데이터</param>
        public void Send(ProtocolCommand cmd, byte[] sendData)
        {
            if (!isConnect)
                return;

            List<byte> byteStream = new List<byte>();

            // 데이터를 바이트 배열로 변환시 역순으로 뒤집혀서
            // 데이터 패킷 자체를 거꾸로 삽입한 후 한번에 뒤집기
            byteStream.AddRange(BitConverter.GetBytes(STX));
            byteStream.AddRange(BitConverter.GetBytes(PROTOCOL_VER));
            byteStream.AddRange(BitConverter.GetBytes(RETURN_CODE));
            byteStream.AddRange(BitConverter.GetBytes(sendData.Length + 4));
            byteStream.AddRange(BitConverter.GetBytes((int)cmd + 0x2000));
            byteStream.AddRange(sendData);
            byteStream.AddRange(BitConverter.GetBytes(ETX));

            byte[] temp = byteStream.ToArray();

            commManager.CommSendStack(temp, out commCode);
            OnSendEvent(new CommEventArg(temp));
        }

        /// <summary>
        /// 사이클러 통신 수신
        /// </summary>
        public void Receive()
        {
            byte[] receive = commManager.CommReceiveStack(commCode);

            if (receive.Length == 0)
                return;

            OnReceiveEvent(new CommEventArg(receive));

            switch((ProtocolCommand)PacketCheck(ref receive))
            {
                case ProtocolCommand.NONE_COMMAND:
                    break;
                case ProtocolCommand.CYCLER_STATUS:
                    cyclerStatus.ToClass(receive);

                    StateChange(cyclerStatus.State);

                    //Volt = cyclerStatus.Volt / 10000;
                    Volt += 0.325;
                    Curr = cyclerStatus.Curr / 10000;
                    Capa = cyclerStatus.Capacity / 10000;
                    Power = cyclerStatus.Power / 10000;
                    Watthour = cyclerStatus.Watthour / 10000;
                    Resi = cyclerStatus.Resistance / 10000;
                    CellTemp = cyclerStatus.Temperature / 10;
                    CellState = cyclerStatus.CellStatus.ToString();
                    break;
                case ProtocolCommand.RECIPY_PROTECT:
                    break;
                case ProtocolCommand.STEP_RECIPY:
                    break;
                case ProtocolCommand.STEP_END_PROTECT:
                    break;
                case ProtocolCommand.CONTROL:
                    CtrlCommand recvCmd = (CtrlCommand)BitConverter.ToInt32(receive, 0);
                    if(controlCommand.Command == recvCmd)
                    {

                    }
                    break;
                case ProtocolCommand.SET_CHAMBER:
                    break;
                case ProtocolCommand.PRE_CHARGE_DISCHARGE:
                    break;
                case ProtocolCommand.COUPLING_TEST:
                    break;
                case ProtocolCommand.CAL_POINT_CHECK:
                    calPointCheck.ToClass(receive);
                    break;
                case ProtocolCommand.CAL_POINT_DELETE:
                    int recvVal = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.CAL_OUTPUT_CHANGE:
                    int outMonVolt = BitConverter.ToInt32(receive, 0);
                    int outMonCurr = BitConverter.ToInt32(receive, 4);
                    break;
                case ProtocolCommand.CAL_DMM:
                    int dmmMonVolt = BitConverter.ToInt32(receive, 0);
                    int dmmMonCurr = BitConverter.ToInt32(receive, 4);
                    break;
                case ProtocolCommand.CAL_OUTPUT_STOP:
                    break;
                case ProtocolCommand.CAL_MAIN_RELAY:
                    int revMode = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.CAL_MAIN_DMM:
                    int val = BitConverter.ToInt32(receive, 0);
                    break;
                case ProtocolCommand.PATTERN_FILE_INFO:
                    int stepNo = BitConverter.ToInt32(receive, 0);
                    int startIndex = BitConverter.ToInt32(receive, 4);
                    int count = BitConverter.ToInt32(receive, 8);
                    break;
                case ProtocolCommand.CHAMBER_GROUP_INFO:
                    break;
                case ProtocolCommand.COUPLING_TEST_RESULT:
                    //Send(ProtocolCommand.COUPLING_TEST_RESULT);
                    break;
            }
        }

        /// <summary>
        /// 수신 데이터 패킷 검사
        /// </summary>
        /// 
        /// <param name="commPacket">수신 받은 데이터</param>
        /// 
        /// <returns>
        /// 수신받은 커맨드 번호
        /// </returns>
        public int PacketCheck(ref byte[] commPacket)
        {
            byte[] sliceArray;
            var list = new List<byte>(commPacket);

            // STX 검사
            sliceArray = list.GetRange(0, 4).ToArray();
            list.RemoveRange(0, 4);
            if (STX != BitConverter.ToInt32(sliceArray, 0))
                return 0;

            // 프로토콜 버전 검사
            sliceArray = list.GetRange(0, 4).ToArray();
            list.RemoveRange(0, 4);
            if (PROTOCOL_VER != BitConverter.ToInt32(sliceArray, 0))
                return 0;

            // 리턴 코드 검사
            sliceArray = list.GetRange(0, 4).ToArray();
            list.RemoveRange(0, 4);
            if (RETURN_CODE != BitConverter.ToInt32(sliceArray, 0))
                return 0;

            // 데이터 길이 검사
            list.RemoveRange(0, 4);

            // ETX 검사
            sliceArray = list.GetRange(list.Count - 4, 4).ToArray();
            list.RemoveRange(list.Count - 4, 4);
            if (ETX != BitConverter.ToInt32(sliceArray, 0))
                return 0;

            // 데이터 추출
            sliceArray = list.GetRange(0, 4).ToArray();
            list.RemoveRange(0, 4);
            commPacket = list.ToArray();

            int a = BitConverter.ToInt32(sliceArray, 0) - 0x1000;
            return a;
            //return BitConverter.ToInt32(sliceArray, 0) - 0x1000;
        }

        private void StateChange(CyclerState cyclerState)
        {
            switch(cyclerState)
            {
                case CyclerState.DISCONNECT:
                    State = "연결끊김";
                    StateBg = StatusSetting.disconnBg;
                    StateFt = StatusSetting.disconnFt;
                    break;
                case CyclerState.WAIT:
                    State = "대기";
                    StateBg = StatusSetting.waitBg;
                    StateFt = StatusSetting.waitFt;
                    break;
                case CyclerState.DATA_CHECK:
                    State = "데이터 검사";
                    StateBg = StatusSetting.dataCheckBg;
                    StateFt = StatusSetting.dataCheckFt;
                    break;
                case CyclerState.STOP:
                    State = "정지";
                    StateBg = StatusSetting.stopBg;
                    StateFt = StatusSetting.stopFt;
                    break;
                case CyclerState.EMG:
                    State = "비상정지";
                    StateBg = StatusSetting.emgBg;
                    StateFt = StatusSetting.emgFt;
                    break;
                case CyclerState.INIT:
                    State = "초기화";
                    StateBg = StatusSetting.initBg;
                    StateFt = StatusSetting.initFt;
                    break;
                case CyclerState.PAUSE:
                    State = "일시정지";
                    StateBg = StatusSetting.pauseBg;
                    StateFt = StatusSetting.pauseFt;
                    break;
                case CyclerState.CHAMBER_TEMP_WAIT:
                    State = "챔버 온도대기";
                    StateBg = StatusSetting.chamTempBg;
                    StateFt = StatusSetting.chamTempFt;
                    break;
                case CyclerState.FINISH:
                    State = "공정 종료";
                    StateBg = StatusSetting.finishBg;
                    StateFt = StatusSetting.finishFt;
                    break;
                case CyclerState.RECIPY_SEND:
                    State = "공정정보 전송";
                    StateBg = StatusSetting.processInfoSendBg;
                    StateFt = StatusSetting.processInfoSendFt;
                    break;
                case CyclerState.CC_CHARGE:
                    State = "CC 충전";
                    StateBg = StatusSetting.ccChargeBg;
                    StateFt = StatusSetting.ccChargeFt;
                    break;
                case CyclerState.CC_CV_CHARGE:
                    State = "CC/CV 충전";
                    StateBg = StatusSetting.cccvChargeBg;
                    StateFt = StatusSetting.cccvChargeFt;
                    break;
                case CyclerState.CP_CHARGE:
                    State = "CP 충전";
                    StateBg = StatusSetting.cpChargeBg;
                    StateFt = StatusSetting.cpChargeFt;
                    break;
                case CyclerState.CR_CHARGE:
                    State = "CR 충전";
                    StateBg = StatusSetting.crChargeBg;
                    StateFt = StatusSetting.crChargeFt;
                    break;
                case CyclerState.CC_DISCHARGE:
                    State = "CC 방전";
                    StateBg = StatusSetting.ccDischargeBg;
                    StateFt = StatusSetting.ccDischargeFt;
                    break;
                case CyclerState.CC_CV_DISCHARGE:
                    State = "CC/CV 방전";
                    StateBg = StatusSetting.cccvDischargeBg;
                    StateFt = StatusSetting.cccvDischargeFt;
                    break;
                case CyclerState.CP_DISCHARGE:
                    State = "CP 방전";
                    StateBg = StatusSetting.cpDischargeBg;
                    StateFt = StatusSetting.cpDischargeFt;
                    break;
                case CyclerState.CR_DISCHARGE:
                    State = "CR 방전";
                    StateBg = StatusSetting.crDischargeBg;
                    StateFt = StatusSetting.crDischargeFt;
                    break;
                case CyclerState.REST:
                    State = "휴지";
                    StateBg = StatusSetting.restBg;
                    StateFt = StatusSetting.restFt;
                    break;
                case CyclerState.OCV:
                    State = "OCV";
                    StateBg = StatusSetting.ocvBg;
                    StateFt = StatusSetting.ocvFt;
                    break;
                case CyclerState.PRECHARGE:
                    State = "간이 충방전";
                    StateBg = StatusSetting.preCDBg;
                    StateFt = StatusSetting.preCDFt;
                    break;
                case CyclerState.BALANCING:
                    State = "밸런싱";
                    StateBg = StatusSetting.balancingBg;
                    StateFt = StatusSetting.balancingFt;
                    break;
                case CyclerState.CONNET_TEST:
                    State = "체결 테스트";
                    StateBg = StatusSetting.connTestBg;
                    StateFt = StatusSetting.connTestFt;
                    break;
                case CyclerState.CALIBRATION:
                    State = "캘리브레이션";
                    StateBg = StatusSetting.calBg;
                    StateFt = StatusSetting.calFt;
                    break;
                case CyclerState.PATTERN:
                    State = "패턴";
                    StateBg = StatusSetting.patternBg;
                    StateFt = StatusSetting.patternFt;
                    break;
                case CyclerState.STEP_SYNC_WAIT:
                    //State = "대기";
                    //StateBg = StatusSetting.sync;
                    //StateFt = StatusSetting.patternFt;
                    break;
                case CyclerState.DATA_RESTORE_WAIT:
                    //State = "대기";
                    //StateBg = StatusSetting.data;
                    //StateFt = StatusSetting.patternFt;
                    break;
            }
        }

        public static void Main()
        {

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///// 사이클러 제어 함수
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        public void StatusRequest()
        {
            Send(ProtocolCommand.CYCLER_STATUS);
        }

        public void SetProtect()
        {
            Send(ProtocolCommand.RECIPY_PROTECT, recipyProtect.ToByteArray());
        }

        public void SetStepRecipy()
        {
            Send(ProtocolCommand.STEP_RECIPY, stepRecipy.ToByteArray());
        }

        public void SetStepEnd_Protect()
        {
            Send(ProtocolCommand.STEP_END_PROTECT, stepEnd_Protect.ToByteArray());
        }

        public void SetCtrlCommand()
        {
            Send(ProtocolCommand.CONTROL, controlCommand.ToByteArray());
        }

        public void SetChamberCtrl()
        {
            Send(ProtocolCommand.SET_CHAMBER, chamberManual.ToByteArray());
        }

        public void SetPreCD()
        {
            Send(ProtocolCommand.PRE_CHARGE_DISCHARGE, preCharDischar.ToByteArray());
        }

        public void SetCouplingTest()
        {
            Send(ProtocolCommand.COUPLING_TEST, couplingTest.ToByteArray());
        }

        public void SetCalPointCheck()
        {
            Send(ProtocolCommand.CAL_POINT_CHECK);
        }

// 켈 포인트 삭제 기능 미구현
#if fasle
        public void SetCalPointDel()
        {
            Send(ProtocolCommand.CAL_POINT_DELETE, calPointDel.ToByteArray());
            Thread.Sleep(1000);
            Receive();
        }
#endif

        public void SetCalOutputChange()
        {
            Send(ProtocolCommand.CAL_OUTPUT_CHANGE, outputChange.ToByteArray());
            //Thread.Sleep(4000);
        }

        public void SetCalDmm()
        {
            Send(ProtocolCommand.CAL_DMM, calDmm.ToByteArray());
            //Thread.Sleep(3500);
            Receive();
        }

        public void SetCalOutputStop()
        {
            Send(ProtocolCommand.CAL_OUTPUT_STOP);
            //Thread.Sleep(3000);
        }

        public void SetCalMainRelay()
        {
            Send(ProtocolCommand.CAL_MAIN_RELAY, calMainRelay.ToByteArray());
        }

        public void SetSafetyCondition()
        {
            Send(ProtocolCommand.SAFETY_CONDITION, safetyCondition.ToByteArray());
        }

        public void SetPatternInfo()
        {
            Send(ProtocolCommand.PATTERN_FILE_INFO, patternFile.ToByteArray());
        }

        public void SetChamberGroup()
        {
            Send(ProtocolCommand.CHAMBER_GROUP_INFO, chamberGroup.ToByteArray());
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///// 이벤트 트리거 함수
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 연결 이벤트 발생 함수
        /// </summary>
        /// <param name="e">이벤트 인자</param>
        private void OnConnectEvent(ConnectEventArg e)
        {
            ConnectEvent?.Invoke(this, e);
        }

        /// <summary>
        /// 송신 이벤트 발생 함수
        /// </summary>
        /// <param name="e">이벤트 인자</param>
        private void OnSendEvent(CommEventArg e)
        {
            SendEvent?.Invoke(this, e);
        }

        /// <summary>
        /// 수신 이벤트 발생 함수
        /// </summary>
        /// <param name="e">이벤트 인자</param>
        private void OnReceiveEvent(CommEventArg e)
        {
            ReceiveEvent?.Invoke(this, e);
        }
    }
}
