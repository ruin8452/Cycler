using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models
{
    public class SafetySetting
    {
        #region .ini File 작성을 위한 DLL 추가
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion


        public static bool cellOverCharEnable;     // 과충전 방지 활성화
        public static int cellOverCharLimit;       // 과충전 제한

        public static bool cellOverDischarEnable;  // 과방전 방지 활성화
        public static int cellOverDischarLimit;    // 과방전 제한

        public static bool cellOverTempEnable;     // 과열 방지 활성화
        public static int cellOverTempLimit;       // 과열 제한

        static string filePath = @".\Settings\SafetySetting.ini";
        
        public static void WriteSetting()
        {
            WritePrivateProfileString("Safety", "cellOverCharEnable",    cellOverCharEnable.ToString(),    filePath);
            WritePrivateProfileString("Safety", "cellOverCharLimit",     cellOverCharLimit.ToString(),     filePath);
            WritePrivateProfileString("Safety", "cellOverDischarEnable", cellOverDischarEnable.ToString(), filePath);
            WritePrivateProfileString("Safety", "cellOverDischarLimit",  cellOverDischarLimit.ToString(),  filePath);
            WritePrivateProfileString("Safety", "cellOverTempEnable",    cellOverTempEnable.ToString(),    filePath);
            WritePrivateProfileString("Safety", "cellOverTempLimit",     cellOverTempLimit.ToString(),     filePath);
        }

        public static void ReadSetting()
        {
            cellOverCharEnable    = bool.Parse(IniRead("Safety", "cellOverCharEnable", "false", filePath));
            cellOverCharLimit     = int.Parse(IniRead("Safety", "cellOverCharLimit", "4500", filePath));

            cellOverDischarEnable = bool.Parse(IniRead("Safety", "cellOverDischarEnable", "false", filePath));
            cellOverDischarLimit  = int.Parse(IniRead("Safety", "cellOverDischarLimit", "500", filePath));

            cellOverTempEnable    = bool.Parse(IniRead("Safety", "cellOverTempEnable", "false", filePath));
            cellOverTempLimit     = int.Parse(IniRead("Safety", "cellOverTempLimit", "70", filePath));
        }

        static string IniRead(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(100);

            GetPrivateProfileString(section, key, def, sb, sb.Capacity, filePath);
            return sb.ToString();
        }
    }
}
