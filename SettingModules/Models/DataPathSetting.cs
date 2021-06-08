using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SettingModules.Models
{
    public class DataPathSetting
    {
        #region .ini File 작성을 위한 DLL 추가
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion


        public static string dataPath;

        static string filePath = @".\Settings\DataPathSetting.ini";

        public static void WriteSetting()
        {
            WritePrivateProfileString("Path", "dataPath", dataPath, filePath);
        }

        public static void ReadSetting()
        {
            dataPath = IniRead("Path", "dataPath", "", filePath);
        }

        static string IniRead(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(100);

            GetPrivateProfileString(section, key, def, sb, sb.Capacity, filePath);
            return sb.ToString();
        }
    }
}
