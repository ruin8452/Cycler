using CommModules;
using CyclerModule;
using CyclerModule.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclerTester
{
    public partial class Form1 : Form
    {
        Cycler cycler;

        delegate void abc();

        public Form1()
        {
            InitializeComponent();
        }

        private void Cycler_SendEvent(object sender, CyclerModule.EventArgsClass.CommEventArg e)
        {
            string hexString = BitConverter.ToString(e.CommArray).Replace('-', ' ');
            if(DataView.InvokeRequired)
            {
                DataView.Invoke(new abc(() => {
                    DataView.Items.Add(string.Format($"[S] {hexString}"));
                    DataView.SelectedIndex = DataView.Items.Count - 1;
                }));
            }
            else
            {
                DataView.Items.Add(string.Format($"[S] {hexString}"));
                DataView.SelectedIndex = DataView.Items.Count - 1;
            }
        }

        private void Cycler_ReceiveEvent(object sender, CyclerModule.EventArgsClass.CommEventArg e)
        {
            string hexString = BitConverter.ToString(e.CommArray).Replace('-', ' ');
            if(DataView.InvokeRequired)
            {
                DataView.Invoke(new abc(() => {
                    DataView.Items.Add(string.Format($"[R] {hexString}"));
                    DataView.SelectedIndex = DataView.Items.Count - 1;
                }));
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {

            ICommModule commModule = new TcpModule(IPBox.Text, int.Parse(PortBox.Text));
            cycler = new Cycler(commModule);

            cycler.SendEvent += Cycler_SendEvent;
            cycler.ReceiveEvent += Cycler_ReceiveEvent;

            cycler.Connect();
            ConnectCheck.Checked = cycler.IsConnect;
        }

        private void ConnectCheckBtn_Click(object sender, EventArgs e)
        {
            ConnectCheck.Checked = cycler.IsConnect;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            cycler.Send(ProtocolCommand.CYCLER_STATUS);
            Thread.Sleep(100);
            cycler.Receive();
        }

        private void MoniEnable_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = MoniEnable.Checked;
        }

        private void StatusBtn_Click(object sender, EventArgs e)
        {
            cycler.StatusRequest();
        }

        private void ProtectBtn_Click(object sender, EventArgs e)
        {
            cycler.recipyProtect.VoltUpperLimite = 100;
            cycler.recipyProtect.VoltLowerLimite = 200;
            cycler.recipyProtect.CurrUpperLimite = 300;
            cycler.recipyProtect.CurrLowerLimite = 400;
            cycler.recipyProtect.CapaUpperLimite = 500;
            cycler.recipyProtect.TempUpperLimite = 600;
            
            cycler.SetProtect();
        }

        private void StepRecipyBtn_Click(object sender, EventArgs e)
        {
            cycler.stepRecipy.StepInfo = new CyclerModule.Protocol.Step[3];

            cycler.stepRecipy.StepCount = 3;
            cycler.stepRecipy.StartStepNo = 0;
            cycler.stepRecipy.StartRepeatCount = 0;
            for(int i = 0; i < cycler.stepRecipy.StepInfo.Length; i++)
            {
                cycler.stepRecipy.StepInfo[i].CycleNo = i;
                cycler.stepRecipy.StepInfo[i].StepNo = i;
                cycler.stepRecipy.StepInfo[i].RunType = 1;
                cycler.stepRecipy.StepInfo[i].CtrlMode = 1;
                cycler.stepRecipy.StepInfo[i].SetVolt = 1000;
                cycler.stepRecipy.StepInfo[i].SetCurr = 2000;
                cycler.stepRecipy.StepInfo[i].SetPower = 3000;
                cycler.stepRecipy.StepInfo[i].SetResistance = 4000;
                cycler.stepRecipy.StepInfo[i].SetChamberTemp = 500;
                cycler.stepRecipy.StepInfo[i].SetChamberHum = 500;
                cycler.stepRecipy.StepInfo[i].SetChamberWaitRate = 0;
                cycler.stepRecipy.StepInfo[i].SetChamberRange = 3;
                cycler.stepRecipy.StepInfo[i].SetRecTime = 0;
                cycler.stepRecipy.StepInfo[i].SetRecVolt = 0;
                cycler.stepRecipy.StepInfo[i].SetRecCurr = 0;
                cycler.stepRecipy.StepInfo[i].SetRecTemp = 0;
                cycler.stepRecipy.StepInfo[i].CycleRepeateCount = 1;
                cycler.stepRecipy.StepInfo[i].CycleGotoStep = 0;
                cycler.stepRecipy.StepInfo[i].CycleGotoCount = 0;
                cycler.stepRecipy.StepInfo[i].CycleAccCapaInit = 0;
            }

            cycler.SetStepRecipy();
        }

        private void StepEndBtn_Click(object sender, EventArgs e)
        {
            cycler.stepEnd_Protect.End_Protects = new CyclerModule.Protocol.End_Protect[3];

            cycler.stepEnd_Protect.StepCount = 3;
            for (int i = 0; i < cycler.stepEnd_Protect.End_Protects.Length; i++)
            {
                cycler.stepEnd_Protect.End_Protects[i].EndVolt = 1000;
                cycler.stepEnd_Protect.End_Protects[i].EndVoltMoveStep = 10;
                cycler.stepEnd_Protect.End_Protects[i].EndCurr = 1000;
                cycler.stepEnd_Protect.End_Protects[i].EndCurrMoveStep = 10;
                cycler.stepEnd_Protect.End_Protects[i].EndTimeDay = 3;
                cycler.stepEnd_Protect.End_Protects[i].EndTimeTime = 50;
                cycler.stepEnd_Protect.End_Protects[i].EndTimeMoveStep = 20;
                cycler.stepEnd_Protect.End_Protects[i].EndCVTimeDay = 1;
                cycler.stepEnd_Protect.End_Protects[i].EndCVTimeTime = 3;
                cycler.stepEnd_Protect.End_Protects[i].EndCVTImeMoveStep = 5;
                cycler.stepEnd_Protect.End_Protects[i].EndCapa = 5000;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaMoveStep = 5;
                cycler.stepEnd_Protect.End_Protects[i].EndPower = 5;
                cycler.stepEnd_Protect.End_Protects[i].EndWatthour = 5000;
                cycler.stepEnd_Protect.End_Protects[i].EndDeltaVp = 5123;
                cycler.stepEnd_Protect.End_Protects[i].EndTemp = 645;
                cycler.stepEnd_Protect.End_Protects[i].EndTempType = 1;
                cycler.stepEnd_Protect.End_Protects[i].EndTempMoveStep = 3;
                cycler.stepEnd_Protect.End_Protects[i].EndSocEiffc = 6000;
                cycler.stepEnd_Protect.End_Protects[i].EndSoc = 9000;
                cycler.stepEnd_Protect.End_Protects[i].EndSocMoveStep = 10;
                cycler.stepEnd_Protect.End_Protects[i].EndSocMode = 1;
                cycler.stepEnd_Protect.End_Protects[i].EndStepSave = 8527;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaEiffc = 9663;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaRepeat = 4100;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaStep = 7846;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaValue = 113;
                cycler.stepEnd_Protect.End_Protects[i].EndCapaFinish = 83613;
                cycler.stepEnd_Protect.End_Protects[i].EndEnergyEiffc = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndEnergyRepeat = 31;
                cycler.stepEnd_Protect.End_Protects[i].EndEnergyStep = 100;
                cycler.stepEnd_Protect.End_Protects[i].EndEnergyValue = 789;
                cycler.stepEnd_Protect.End_Protects[i].EndEnergyFinish = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndResistanceEiffc = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndResistanceRepeat = 4621;
                cycler.stepEnd_Protect.End_Protects[i].EndResistanceStep = 8312;
                cycler.stepEnd_Protect.End_Protects[i].EndResistanceValue = 852;
                cycler.stepEnd_Protect.End_Protects[i].EndResistanceFinish = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndPauseRepeat = 7894;
                cycler.stepEnd_Protect.End_Protects[i].EndPauseStep = 700;
                cycler.stepEnd_Protect.End_Protects[i].EndSumCapaKind = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndSumCapaValue = 500;
                cycler.stepEnd_Protect.End_Protects[i].EndSumWhKind = 0;
                cycler.stepEnd_Protect.End_Protects[i].EndSumWhValue = 6000;

                cycler.stepEnd_Protect.End_Protects[i].ProtectVoltUpper = 5000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectVoltLower = 4000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectCurrUpper = 6000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectCurrLower = 6000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectCapaUpper = 6000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectCapaLower = 6000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectResistanceUpper = 3000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectResistanceLower = 1000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectTempUpper = 8000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectTempLower = 7000;
                cycler.stepEnd_Protect.End_Protects[i].ProtectRestartTempUpper = 9100;
                cycler.stepEnd_Protect.End_Protects[i].ProtectRestartTempLower = 1000;
            }

            cycler.SetStepEnd_Protect();
        }

        private void CtrlBtn_Click(object sender, EventArgs e)
        {
            cycler.controlCommand.Command = CtrlCommand.EMG_STOP;
            cycler.controlCommand.EmgStatus = 100;
            cycler.controlCommand.TimeSync = 500;

            cycler.SetCtrlCommand();
        }

        private void ChamberSetBtn_Click(object sender, EventArgs e)
        {
            cycler.chamberManual.SetTemp = 105;
            cycler.chamberManual.SetHum = 500;
            cycler.chamberManual.ChamberCommand = 0;

            cycler.SetChamberCtrl();
        }

        private void PreCDBtn_Click(object sender, EventArgs e)
        {
            cycler.preCharDischar.SetMode = 1;
            cycler.preCharDischar.SetVolt = 2000;
            cycler.preCharDischar.SetCurr = 3000;
            cycler.preCharDischar.SetEndTIme = 5000;
            cycler.preCharDischar.SetEndVolt = 65420;
            cycler.preCharDischar.SetEndCurr = 8520;
            cycler.preCharDischar.VoltLimit = 1230;
            cycler.preCharDischar.CurrLimit = 7745;

            cycler.SetPreCD();
        }

        private void CouplingTestBtn_Click(object sender, EventArgs e)
        {
            cycler.couplingTest.SetMode = 1;
            cycler.couplingTest.SetChargeVolt = 15000;
            cycler.couplingTest.SetDishargeVolt = 15000;
            cycler.couplingTest.SetCurr = 1000;
            cycler.couplingTest.SetEndTIme = 6000;
            cycler.couplingTest.JudgeChargeIRMax = 5000;
            cycler.couplingTest.JudgeChargeIRMix = 6000;
            cycler.couplingTest.JudgeDischargeIRMax = 7000;
            cycler.couplingTest.JudgeDischargeIRMix = 8000;

            cycler.SetCouplingTest();
        }

        private void CalPointCheckBtn_Click(object sender, EventArgs e)
        {
            cycler.SetCalPointCheck();
        }

        private void CalOutChangeBtn_Click(object sender, EventArgs e)
        {
            cycler.outputChange.OutputMode = 1;
            cycler.outputChange.Mode = 1;
            cycler.outputChange.SetVolt = 2000;
            cycler.outputChange.SetCurr = 2000;

            cycler.SetCalOutputChange();
        }

        private void CalDmmBtn_Click(object sender, EventArgs e)
        {
            
            cycler.calDmm.SetVolt = 10000;
            cycler.calDmm.SetCurr = 5000;
            cycler.calDmm.DmmValue = 45612;

            cycler.SetCalDmm();
        }

        private void CalOutStopBtn_Click(object sender, EventArgs e)
        {
            cycler.SetCalOutputStop();
        }

        private void CalRelayBtn_Click(object sender, EventArgs e)
        {
            cycler.calMainRelay.Mode = 0;

            cycler.SetCalMainRelay();
        }

        private void SafetyBtn_Click(object sender, EventArgs e)
        {
            cycler.safetyCondition.UseOverCharge = 0;
            cycler.safetyCondition.OverChargeVolt = 1000;
            cycler.safetyCondition.UseOverDischarge = 0;
            cycler.safetyCondition.OverDischargeVolt = 3000;
            cycler.safetyCondition.UseOverTemp = 0;
            cycler.safetyCondition.OverTemp = 500;

            cycler.SetSafetyCondition();
        }

        private void PatternBtn_Click(object sender, EventArgs e)
        {
            cycler.patternFile.PatternsInfo = new CyclerModule.Protocol.Pattern[5];

            cycler.patternFile.StepNo = 5;
            cycler.patternFile.PatternStartIndex = 1;
            cycler.patternFile.PatternCount = 5;
            cycler.patternFile.PatternMode = 1;
            cycler.patternFile.PatternEnd = 0;

            for(int i = 0; i < 5; i++)
            {
                cycler.patternFile.PatternsInfo[i].PatternTime = 1000;
                cycler.patternFile.PatternsInfo[i].PatternValue = 5000;
            }

            cycler.SetPatternInfo();
        }

        private void ChamGroupBtn_Click(object sender, EventArgs e)
        {
            cycler.chamberGroup.ChamberGroup = 32;
            cycler.chamberGroup.ChamberGroupUse = -1;
            cycler.chamberGroup.ChamberGroupID = 0;
            cycler.chamberGroup.ChamberMode = 0;

            cycler.SetChamberGroup();
        }
    }
}