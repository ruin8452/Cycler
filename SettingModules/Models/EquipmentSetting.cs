using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models
{
    public class EquipmentSetting
    {
        #region .ini File 작성을 위한 DLL 추가
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion


        public static string equipmentName;      // 장비명

        public static string backupPcIp;         // 백업 PC IP
        public static int backupPcPort;          // 백업 PC 포트

        public static string calIp;              // 캘 장비 IP
        public static int calPort;               // 캘 장비 포트

        public static string totalMoniPcIp;      // 통합 모니터링 PC IP
        public static int totalMoniPcPort;       // 통합 모니터링 PC 포트

        public static int chamberCount;          // 챔버 수량 설정
        public static double chamberTimeScale;   // 챔버 온도 변경 시 시간 확장
        public static double chamberTempErrRate; // 챔버 온도 유지 시 오차
        public static double chamberDefTemp;     // 챔버 이상 시 기본 온도
        public static bool chamberAutoEnd;       // 항온 챔버 공정 종료 후 자동 종료

        public static int ChCount;
        public static ChSettingList[] chSettingList;

        static string filePath = @".\Settings\EquipmentSetting.ini";

        public static void WriteSetting()
        {
            WritePrivateProfileString("Equipment", "equipmentName",      equipmentName,                 filePath);

            WritePrivateProfileString("Equipment", "backupPcIp",         backupPcIp,                    filePath);
            WritePrivateProfileString("Equipment", "backupPcPort",       backupPcPort.ToString(),       filePath);

            WritePrivateProfileString("Equipment", "calIp",              calIp,                         filePath);
            WritePrivateProfileString("Equipment", "calPort",            calPort.ToString(),            filePath);

            WritePrivateProfileString("Equipment", "totalMoniPcIp",      totalMoniPcIp,                 filePath);
            WritePrivateProfileString("Equipment", "totalMoniPcPort",    totalMoniPcPort.ToString(),    filePath);

            WritePrivateProfileString("Equipment", "chamberCount",       chamberCount.ToString(),       filePath);
            WritePrivateProfileString("Equipment", "chamberTimeScale",   chamberTimeScale.ToString(),   filePath);
            WritePrivateProfileString("Equipment", "chamberTempErrRate", chamberTempErrRate.ToString(), filePath);
            WritePrivateProfileString("Equipment", "chamberDefTemp",     chamberDefTemp.ToString(),     filePath);
            WritePrivateProfileString("Equipment", "chamberAutoEnd",     chamberAutoEnd.ToString(),     filePath);

            WritePrivateProfileString("ChInfo", "ChCount", chSettingList.Length.ToString(), filePath);

            for (int i = 0; i < chSettingList.Length; i++)
            {
                WritePrivateProfileString("ChInfo", "No_" + i,           chSettingList[i].No.ToString(),           filePath);
                WritePrivateProfileString("ChInfo", "ChName_" + i,       chSettingList[i].ChName,                  filePath);
                WritePrivateProfileString("ChInfo", "ChamberGroup_" + i, chSettingList[i].ChamberGroup.ToString(), filePath);
                WritePrivateProfileString("ChInfo", "IP_" + i,           chSettingList[i].IP,                      filePath);
                WritePrivateProfileString("ChInfo", "Port_" + i,         chSettingList[i].Port.ToString(),         filePath);
                WritePrivateProfileString("ChInfo", "Enable_" + i,       chSettingList[i].Enable.ToString(),       filePath);
            }
        }

        public static void ReadSetting()
        {
            equipmentName      = IniRead("Equipment", "equipmentName", string.Empty, filePath);

            backupPcIp         = IniRead("Equipment", "backupPcIp", "0.0.0.0", filePath);
            backupPcPort       = int.Parse(IniRead("Equipment", "backupPcPort", "1000", filePath));

            calIp              = IniRead("Equipment", "calIp", "0.0.0.0", filePath);
            calPort            = int.Parse(IniRead("Equipment", "calPort", "1000", filePath));

            totalMoniPcIp      = IniRead("Equipment", "totalMoniPcIp", "0.0.0.0", filePath);
            totalMoniPcPort    = int.Parse(IniRead("Equipment", "totalMoniPcPort", "1000", filePath));

            chamberCount       = int.Parse(IniRead("Equipment", "chamberCount", "4", filePath));
            chamberTimeScale   = double.Parse(IniRead("Equipment", "chamberTimeScale", "5.0", filePath));
            chamberTempErrRate = double.Parse(IniRead("Equipment", "chamberTempErrRate", "5.0", filePath));
            chamberDefTemp     = double.Parse(IniRead("Equipment", "chamberDefTemp", "25.0", filePath));
            chamberAutoEnd     = bool.Parse(IniRead("Equipment", "chamberAutoEnd", "true", filePath));

            ChCount = int.Parse(IniRead("ChInfo", "ChCount", "0", filePath));
            chSettingList = new ChSettingList[ChCount];
            for (int i = 0; i < ChCount; i++)
            {
                ChSettingList temp = new ChSettingList
                {
                    No           = int.Parse(IniRead("ChInfo", "No_" + i, (i + 1).ToString(), filePath)),
                    ChName       = IniRead("ChInfo", "ChName_" + i, "CH" + (i + 1).ToString(), filePath),
                    ChamberGroup = int.Parse(IniRead("ChInfo", "ChamberGroup_" + i, "1", filePath)),
                    IP           = IniRead("ChInfo", "IP_" + i, "0.0.0.0", filePath),
                    Port         = int.Parse(IniRead("ChInfo", "Port_" + i, "1000", filePath)),
                    Enable       = bool.Parse(IniRead("ChInfo", "Enable_" + i, "true", filePath)),
                };

                chSettingList[temp.No - 1] = temp;
            }
        }

        static string IniRead(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(100);

            GetPrivateProfileString(section, key, def, sb, sb.Capacity, filePath);
            return sb.ToString();
        }
    }
}
