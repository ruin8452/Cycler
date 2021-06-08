using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models
{
    public class StatusSetting
    {
        #region .ini File 작성을 위한 DLL 추가
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        // 연결끊김
        public static string disconnFt;
        public static string disconnBg;
        // 대기
        public static string waitFt;
        public static string waitBg;
        // 정지
        public static string stopFt;
        public static string stopBg;
        // 비상정지
        public static string emgFt;
        public static string emgBg;
        // 초기화
        public static string initFt;
        public static string initBg;
        // 일시정지
        public static string pauseFt;
        public static string pauseBg;
        // 챔버 온도대기
        public static string chamTempFt;
        public static string chamTempBg;
        // 공정종료
        public static string finishFt;
        public static string finishBg;
        // 공정정보 전송
        public static string processInfoSendFt;
        public static string processInfoSendBg;
        // CC 충전
        public static string ccChargeFt;
        public static string ccChargeBg;
        // CC/CV 충전
        public static string cccvChargeFt;
        public static string cccvChargeBg;
        // CP 충전
        public static string cpChargeFt;
        public static string cpChargeBg;
        // CR 충전
        public static string crChargeFt;
        public static string crChargeBg;
        // CC 방전
        public static string ccDischargeFt;
        public static string ccDischargeBg;
        // CC/CV 방전
        public static string cccvDischargeFt;
        public static string cccvDischargeBg;
        // CP 방전
        public static string cpDischargeFt;
        public static string cpDischargeBg;
        // CR 방전
        public static string crDischargeFt;
        public static string crDischargeBg;
        // 휴지
        public static string restFt;
        public static string restBg;
        // OCV
        public static string ocvFt;
        public static string ocvBg;
        // 간이 충방전
        public static string preCDFt;
        public static string preCDBg;
        // 밸런싱
        public static string balancingFt;
        public static string balancingBg;
        // 체결 테스트
        public static string connTestFt;
        public static string connTestBg;
        // 캘
        public static string calFt;
        public static string calBg;
        // 패턴
        public static string patternFt;
        public static string patternBg;
        // 데이터 체크
        public static string dataCheckFt;
        public static string dataCheckBg;



        // OCV 스텝
        public static string ocvStepFt;
        public static string ocvStepBg;
        // 충전 스텝
        public static string chargeStepFt;
        public static string chargeStepBg;
        // 방전 스텝
        public static string dischargeStepFt;
        public static string dischargeStepBg;
        // 휴지 스텝
        public static string restStepFt;
        public static string restStepBg;
        // 패턴 스텝
        public static string patternStepFt;
        public static string patternStepBg;
        // 임피던스 스텝
        public static string impedanceStepFt;
        public static string impedanceStepBg;
        // 사이클 스텝
        public static string cycleStepFt;
        public static string cycleStepBg;
        // 루프 스텝
        public static string loopStepFt;
        public static string loopStepBg;
        // 종료 스텝
        public static string endStepFt;
        public static string endStepBg;


        // 전압 항목
        public static int voltUnitIndex;
        public static int voltRoundIndex;
        // 전류 항목
        public static int currUnitIndex;
        public static int currRoundIndex;
        // 용량 항목
        public static int capaUnitIndex;
        public static int capaRoundIndex;
        // 전력 항목
        public static int powerUnitIndex;
        public static int powerRoundIndex;
        // 전력량 항목
        public static int watthourUnitIndex;
        public static int watthourRoundIndex;
        // 임피던스 항목
        public static int resiUnitIndex;
        public static int resiRoundIndex;


        // 데이터 갱신주기
        public static int dataRenuwal;
        // 모니터링 폰트 크기
        public static int moniFontSize;


        public static ChMoniList[] chMoniList = new ChMoniList[20];

        static string filePath = @".\Settings\StatusSetting.ini";

        public static void WriteSetting()
        {
            WritePrivateProfileString("StatusColor", "disconnFt",         disconnFt,         filePath);
            WritePrivateProfileString("StatusColor", "disconnBg",         disconnBg,         filePath);
            WritePrivateProfileString("StatusColor", "waitFt",            waitFt,            filePath);
            WritePrivateProfileString("StatusColor", "waitBg",            waitBg,            filePath);
            WritePrivateProfileString("StatusColor", "stopFt",            stopFt,            filePath);
            WritePrivateProfileString("StatusColor", "stopBg",            stopBg,            filePath);
            WritePrivateProfileString("StatusColor", "emgFt",             emgFt,             filePath);
            WritePrivateProfileString("StatusColor", "emgBg",             emgBg,             filePath);
            WritePrivateProfileString("StatusColor", "initFt",            initFt,            filePath);
            WritePrivateProfileString("StatusColor", "initBg",            initBg,            filePath);
            WritePrivateProfileString("StatusColor", "pauseFt",           pauseFt,           filePath);
            WritePrivateProfileString("StatusColor", "pauseBg",           pauseBg,           filePath);
            WritePrivateProfileString("StatusColor", "chamTempFt",        chamTempFt,        filePath);
            WritePrivateProfileString("StatusColor", "chamTempBg",        chamTempBg,        filePath);
            WritePrivateProfileString("StatusColor", "finishFt",          finishFt,          filePath);
            WritePrivateProfileString("StatusColor", "finishBg",          finishBg,          filePath);
            WritePrivateProfileString("StatusColor", "processInfoSendFt", processInfoSendFt, filePath);
            WritePrivateProfileString("StatusColor", "processInfoSendBg", processInfoSendBg, filePath);
            WritePrivateProfileString("StatusColor", "ccChargeFt",        ccChargeFt,        filePath);
            WritePrivateProfileString("StatusColor", "ccChargeBg",        ccChargeBg,        filePath);
            WritePrivateProfileString("StatusColor", "cccvChargeFt",      cccvChargeFt,      filePath);
            WritePrivateProfileString("StatusColor", "cccvChargeBg",      cccvChargeBg,      filePath);
            WritePrivateProfileString("StatusColor", "cpChargeFt",        cpChargeFt,        filePath);
            WritePrivateProfileString("StatusColor", "cpChargeBg",        cpChargeBg,        filePath);
            WritePrivateProfileString("StatusColor", "crChargeFt",        crChargeFt,        filePath);
            WritePrivateProfileString("StatusColor", "crChargeBg",        crChargeBg,        filePath);
            WritePrivateProfileString("StatusColor", "ccDischargeFt",     ccDischargeFt,     filePath);
            WritePrivateProfileString("StatusColor", "ccDischargeBg",     ccDischargeBg,     filePath);
            WritePrivateProfileString("StatusColor", "cccvDischargeFt",   cccvDischargeFt,   filePath);
            WritePrivateProfileString("StatusColor", "cccvDischargeBg",   cccvDischargeBg,   filePath);
            WritePrivateProfileString("StatusColor", "cpDischargeFt",     cpDischargeFt,     filePath);
            WritePrivateProfileString("StatusColor", "cpDischargeBg",     cpDischargeBg,     filePath);
            WritePrivateProfileString("StatusColor", "crDischargeFt",     crDischargeFt,     filePath);
            WritePrivateProfileString("StatusColor", "crDischargeBg",     crDischargeBg,     filePath);
            WritePrivateProfileString("StatusColor", "restFt",            restFt,            filePath);
            WritePrivateProfileString("StatusColor", "restBg",            restBg,            filePath);
            WritePrivateProfileString("StatusColor", "ocvFt",             ocvFt,             filePath);
            WritePrivateProfileString("StatusColor", "ocvBg",             ocvBg,             filePath);
            WritePrivateProfileString("StatusColor", "preCDFt",           preCDFt,           filePath);
            WritePrivateProfileString("StatusColor", "preCDBg",           preCDBg,           filePath);
            WritePrivateProfileString("StatusColor", "balancingFt",       balancingFt,       filePath);
            WritePrivateProfileString("StatusColor", "balancingBg",       balancingBg,       filePath);
            WritePrivateProfileString("StatusColor", "connTestFt",        connTestFt,        filePath);
            WritePrivateProfileString("StatusColor", "connTestBg",        connTestBg,        filePath);
            WritePrivateProfileString("StatusColor", "calFt",             calFt,             filePath);
            WritePrivateProfileString("StatusColor", "calBg",             calBg,             filePath);
            WritePrivateProfileString("StatusColor", "patternFt",         patternFt,         filePath);
            WritePrivateProfileString("StatusColor", "patternBg",         patternBg,         filePath);
            WritePrivateProfileString("StatusColor", "dataCheckFt",       dataCheckFt,       filePath);
            WritePrivateProfileString("StatusColor", "dataCheckBg",       dataCheckBg,       filePath);

            
            WritePrivateProfileString("StepTypeColor", "ocvStepFt",       ocvStepFt,       filePath);
            WritePrivateProfileString("StepTypeColor", "ocvStepBg",       ocvStepBg,       filePath);
            WritePrivateProfileString("StepTypeColor", "chargeStepFt",    chargeStepFt,    filePath);
            WritePrivateProfileString("StepTypeColor", "chargeStepBg",    chargeStepBg,    filePath);
            WritePrivateProfileString("StepTypeColor", "dischargeStepFt", dischargeStepFt, filePath);
            WritePrivateProfileString("StepTypeColor", "dischargeStepBg", dischargeStepBg, filePath);
            WritePrivateProfileString("StepTypeColor", "restStepFt",      restStepFt,      filePath);
            WritePrivateProfileString("StepTypeColor", "restStepBg",      restStepBg,      filePath);
            WritePrivateProfileString("StepTypeColor", "patternStepFt",   patternStepFt,   filePath);
            WritePrivateProfileString("StepTypeColor", "patternStepBg",   patternStepBg,   filePath);
            WritePrivateProfileString("StepTypeColor", "impedanceStepFt", impedanceStepFt, filePath);
            WritePrivateProfileString("StepTypeColor", "impedanceStepBg", impedanceStepBg, filePath);
            WritePrivateProfileString("StepTypeColor", "cycleStepFt",     cycleStepFt,     filePath);
            WritePrivateProfileString("StepTypeColor", "cycleStepBg",     cycleStepBg,     filePath);
            WritePrivateProfileString("StepTypeColor", "loopStepFt",      loopStepFt,      filePath);
            WritePrivateProfileString("StepTypeColor", "loopStepBg",      loopStepBg,      filePath);
            WritePrivateProfileString("StepTypeColor", "endStepFt",       endStepFt,       filePath);
            WritePrivateProfileString("StepTypeColor", "endStepBg",       endStepBg,       filePath);


            WritePrivateProfileString("UnitSet", "voltUnitIndex",      voltUnitIndex.ToString(),      filePath);
            WritePrivateProfileString("UnitSet", "voltRoundIndex",     voltRoundIndex.ToString(),     filePath);
            WritePrivateProfileString("UnitSet", "currUnitIndex",      currUnitIndex.ToString(),      filePath);
            WritePrivateProfileString("UnitSet", "currRoundIndex",     currRoundIndex.ToString(),     filePath);
            WritePrivateProfileString("UnitSet", "capaUnitIndex",      capaUnitIndex.ToString(),      filePath);
            WritePrivateProfileString("UnitSet", "capaRoundIndex",     capaRoundIndex.ToString(),     filePath);
            WritePrivateProfileString("UnitSet", "powerUnitIndex",     powerUnitIndex.ToString(),     filePath);
            WritePrivateProfileString("UnitSet", "powerRoundIndex",    powerRoundIndex.ToString(),    filePath);
            WritePrivateProfileString("UnitSet", "watthourUnitIndex",  watthourUnitIndex.ToString(),  filePath);
            WritePrivateProfileString("UnitSet", "watthourRoundIndex", watthourRoundIndex.ToString(), filePath);
            WritePrivateProfileString("UnitSet", "resiUnitIndex",      resiUnitIndex.ToString(),      filePath);
            WritePrivateProfileString("UnitSet", "resiRoundIndex",     resiRoundIndex.ToString(),     filePath);


            WritePrivateProfileString("MoniSet", "dataRenuwal",  dataRenuwal.ToString(),  filePath);
            WritePrivateProfileString("MoniSet", "moniFontSize", moniFontSize.ToString(), filePath);

            for(int i = 0; i < 20; i++)
            {
                WritePrivateProfileString("ListSet",     "No_" + i, chMoniList[i].No.ToString(),     filePath);
                WritePrivateProfileString("ListSet",  "Width_" + i, chMoniList[i].Width.ToString(),  filePath);
                WritePrivateProfileString("ListSet", "Enable_" + i, chMoniList[i].Enable.ToString(), filePath);
                WritePrivateProfileString("ListSet",   "Name_" + i, chMoniList[i].Name,              filePath);
            }
        }
        public static void ReadSetting()
        {
            disconnFt         = IniRead("StatusColor", "disconnFt",         "Black", filePath);
            disconnBg         = IniRead("StatusColor", "disconnBg",         "White", filePath);
            waitFt            = IniRead("StatusColor", "waitFt",            "Black", filePath);
            waitBg            = IniRead("StatusColor", "waitBg",            "White", filePath);
            stopFt            = IniRead("StatusColor", "stopFt",            "Black", filePath);
            stopBg            = IniRead("StatusColor", "stopBg",            "White", filePath);
            emgFt             = IniRead("StatusColor", "emgFt",             "Black", filePath);
            emgBg             = IniRead("StatusColor", "emgBg",             "White", filePath);
            initFt            = IniRead("StatusColor", "initFt",            "Black", filePath);
            initBg            = IniRead("StatusColor", "initBg",            "White", filePath);
            pauseFt           = IniRead("StatusColor", "pauseFt",           "Black", filePath);
            pauseBg           = IniRead("StatusColor", "pauseBg",           "White", filePath);
            chamTempFt        = IniRead("StatusColor", "chamTempFt",        "Black", filePath);
            chamTempBg        = IniRead("StatusColor", "chamTempBg",        "White", filePath);
            finishFt          = IniRead("StatusColor", "finishFt",          "Black", filePath);
            finishBg          = IniRead("StatusColor", "finishBg",          "White", filePath);
            processInfoSendFt = IniRead("StatusColor", "processInfoSendFt", "Black", filePath);
            processInfoSendBg = IniRead("StatusColor", "processInfoSendBg", "White", filePath);
            ccChargeFt        = IniRead("StatusColor", "ccChargeFt",        "Black", filePath);
            ccChargeBg        = IniRead("StatusColor", "ccChargeBg",        "White", filePath);
            cccvChargeFt      = IniRead("StatusColor", "cccvChargeFt",      "Black", filePath);
            cccvChargeBg      = IniRead("StatusColor", "cccvChargeBg",      "White", filePath);
            cpChargeFt        = IniRead("StatusColor", "cpChargeFt",        "Black", filePath);
            cpChargeBg        = IniRead("StatusColor", "cpChargeBg",        "White", filePath);
            crChargeFt        = IniRead("StatusColor", "crChargeFt",        "Black", filePath);
            crChargeBg        = IniRead("StatusColor", "crChargeBg",        "White", filePath);
            ccDischargeFt     = IniRead("StatusColor", "ccDischargeFt",     "Black", filePath);
            ccDischargeBg     = IniRead("StatusColor", "ccDischargeBg",     "White", filePath);
            cccvDischargeFt   = IniRead("StatusColor", "cccvDischargeFt",   "Black", filePath);
            cccvDischargeBg   = IniRead("StatusColor", "cccvDischargeBg",   "White", filePath);
            cpDischargeFt     = IniRead("StatusColor", "cpDischargeFt",     "Black", filePath);
            cpDischargeBg     = IniRead("StatusColor", "cpDischargeBg",     "White", filePath);
            crDischargeFt     = IniRead("StatusColor", "crDischargeFt",     "Black", filePath);
            crDischargeBg     = IniRead("StatusColor", "crDischargeBg",     "White", filePath);
            restFt            = IniRead("StatusColor", "restFt",            "Black", filePath);
            restBg            = IniRead("StatusColor", "restBg",            "White", filePath);
            ocvFt             = IniRead("StatusColor", "ocvFt",             "Black", filePath);
            ocvBg             = IniRead("StatusColor", "ocvBg",             "White", filePath);
            preCDFt           = IniRead("StatusColor", "preCDFt",           "Black", filePath);
            preCDBg           = IniRead("StatusColor", "preCDBg",           "White", filePath);
            balancingFt       = IniRead("StatusColor", "balancingFt",       "Black", filePath);
            balancingBg       = IniRead("StatusColor", "balancingBg",       "White", filePath);
            connTestFt        = IniRead("StatusColor", "connTestFt",        "Black", filePath);
            connTestBg        = IniRead("StatusColor", "connTestBg",        "White", filePath);
            calFt             = IniRead("StatusColor", "calFt",             "Black", filePath);
            calBg             = IniRead("StatusColor", "calBg",             "White", filePath);
            patternFt         = IniRead("StatusColor", "patternFt",         "Black", filePath);
            patternBg         = IniRead("StatusColor", "patternBg",         "White", filePath);
            dataCheckFt       = IniRead("StatusColor", "dataCheckFt",       "Black", filePath);
            dataCheckBg       = IniRead("StatusColor", "dataCheckBg",       "White", filePath);

            
            ocvStepFt       = IniRead("StepTypeColor", "ocvStepFt",         "Black", filePath);
            ocvStepBg       = IniRead("StepTypeColor", "ocvStepBg",         "White", filePath);
            chargeStepFt    = IniRead("StepTypeColor", "chargeStepFt",      "Black", filePath);
            chargeStepBg    = IniRead("StepTypeColor", "chargeStepBg",      "White", filePath);
            dischargeStepFt = IniRead("StepTypeColor", "dischargeStepFt",   "Black", filePath);
            dischargeStepBg = IniRead("StepTypeColor", "dischargeStepBg",   "White", filePath);
            restStepFt      = IniRead("StepTypeColor", "restStepFt",        "Black", filePath);
            restStepBg      = IniRead("StepTypeColor", "restStepBg",        "White", filePath);
            patternStepFt   = IniRead("StepTypeColor", "patternStepFt",     "Black", filePath);
            patternStepBg   = IniRead("StepTypeColor", "patternStepBg",     "White", filePath);
            impedanceStepFt = IniRead("StepTypeColor", "impedanceStepFt",   "Black", filePath);
            impedanceStepBg = IniRead("StepTypeColor", "impedanceStepBg",   "White", filePath);
            cycleStepFt     = IniRead("StepTypeColor", "cycleStepFt",       "Black", filePath);
            cycleStepBg     = IniRead("StepTypeColor", "cycleStepBg",       "White", filePath);
            loopStepFt      = IniRead("StepTypeColor", "loopStepFt",        "Black", filePath);
            loopStepBg      = IniRead("StepTypeColor", "loopStepBg",        "White", filePath);
            endStepFt       = IniRead("StepTypeColor", "endStepFt",         "Black", filePath);
            endStepBg       = IniRead("StepTypeColor", "endStepBg",         "White", filePath);


            voltUnitIndex      = int.Parse(IniRead("UnitSet", "voltUnitIndex",      "1", filePath));
            voltRoundIndex     = int.Parse(IniRead("UnitSet", "voltRoundIndex",     "1", filePath));
            currUnitIndex      = int.Parse(IniRead("UnitSet", "currUnitIndex",      "1", filePath));
            currRoundIndex     = int.Parse(IniRead("UnitSet", "currRoundIndex",     "1", filePath));
            capaUnitIndex      = int.Parse(IniRead("UnitSet", "capaUnitIndex",      "1", filePath));
            capaRoundIndex     = int.Parse(IniRead("UnitSet", "capaRoundIndex",     "1", filePath));
            powerUnitIndex     = int.Parse(IniRead("UnitSet", "powerUnitIndex",     "1", filePath));
            powerRoundIndex    = int.Parse(IniRead("UnitSet", "powerRoundIndex",    "1", filePath));
            watthourUnitIndex  = int.Parse(IniRead("UnitSet", "watthourUnitIndex",  "1", filePath));
            watthourRoundIndex = int.Parse(IniRead("UnitSet", "watthourRoundIndex", "1", filePath));
            resiUnitIndex      = int.Parse(IniRead("UnitSet", "resiUnitIndex",      "1", filePath));
            resiRoundIndex     = int.Parse(IniRead("UnitSet", "resiRoundIndex",     "1", filePath));


            dataRenuwal  = int.Parse(IniRead("MoniSet", "dataRenuwal",   "1", filePath));
            moniFontSize = int.Parse(IniRead("MoniSet", "moniFontSize", "10", filePath));

            for(int i = 0; i < 20; i++)
            {
                ChMoniList temp = new ChMoniList
                {
                    No     = int.Parse(IniRead("ListSet", "No_" + i, (i + 1).ToString(), filePath)),
                    Width  = int.Parse(IniRead("ListSet", "Width_" + i, "65", filePath)),
                    Enable = bool.Parse(IniRead("ListSet", "Enable_" + i, "true", filePath)),
                    Name   = IniRead("ListSet", "Name_" + i, "채널상태", filePath)
                };

                chMoniList[temp.No-1] = temp;
            }
        }

        static string IniRead(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(100);

            GetPrivateProfileString(section, key, def, sb, sb.Capacity, filePath);
            return sb.ToString();
        }

        public static string GetVoltUnit()
        {
            switch(voltUnitIndex)
            {
                case 0:
                    return "kV";
                case 1:
                    return "V";
                case 2:
                    return "mV";
                case 3:
                    return "uV";
                default:
                    return "V";
            }
        }

        public static string GetCurrUnit()
        {
            switch (currUnitIndex)
            {
                case 0:
                    return "kA";
                case 1:
                    return "A";
                case 2:
                    return "mA";
                case 3:
                    return "uA";
                default:
                    return "A";
            }
        }

        public static string GetCapaUnit()
        {
            switch (capaUnitIndex)
            {
                case 0:
                    return "kAh";
                case 1:
                    return "Ah";
                case 2:
                    return "mAh";
                case 3:
                    return "uAh";
                default:
                    return "Ah";
            }
        }

        public static string GetPowerUnit()
        {
            switch (powerUnitIndex)
            {
                case 0:
                    return "kW";
                case 1:
                    return "W";
                case 2:
                    return "mW";
                case 3:
                    return "uW";
                default:
                    return "W";
            }
        }

        public static string GetWatthourUnit()
        {
            switch (watthourUnitIndex)
            {
                case 0:
                    return "kWh";
                case 1:
                    return "Wh";
                case 2:
                    return "mWh";
                case 3:
                    return "uWh";
                default:
                    return "Wh";
            }
        }

        public static string GetResiUnit()
        {
            switch (resiUnitIndex)
            {
                case 0:
                    return "kΩ";
                case 1:
                    return "Ω";
                case 2:
                    return "mΩ";
                case 3:
                    return "uΩ";
                default:
                    return "Ω";
            }
        }
    }
}
