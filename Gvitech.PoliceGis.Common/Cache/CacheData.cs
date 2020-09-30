using Microsoft.Win32;
using Mmc.Mspace.Common.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Cache
{
    public class CacheData
    {

        static CacheData()
        {
            CurrentLanguage = GetConfigAppSettingsValue("Language");
            if (string.IsNullOrEmpty(GetRegistData("MMCVersion")))
                CurrentVersion = "v " + GetConfigAppSettingsValue("CurrentVersion");
            else
                CurrentVersion = "v " + GetRegistData("MMCVersion");

        }

        private static string currentVersion;

        public static string CurrentVersion
        {
            get { return currentVersion; }
            set { currentVersion = value; }
        }


        private static UserInfo _userInfo;

        public static UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }


        private static string _currentLanguage;

        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set { _currentLanguage = value; }
        }

        public static void SaveLanguage(string language)
        {
            SaveConfig("Language", language);
            CurrentLanguage = GetConfigAppSettingsValue("Language");
        }
        /// <summary>
        /// 保存配置文件信息
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static bool SaveConfig(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (config.AppSettings.Settings[key] == null)
                {
                    config.AppSettings.Settings.Add(key,value);
                }
                else
                {
                    config.AppSettings.Settings[key].Value = value;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region GetConfigAppSettingsValue 获取配置文件的AppSettings的值
        /// <summary>
        /// 获取配置文件的AppSettings的值
        /// </summary>
        /// <param name="key">配置name名称</param>
        /// <returns>配置value值</returns>
        public static string GetConfigAppSettingsValue(string key)
        {

            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key];
            }
            return "";
        }
        #endregion


        public static string GetRegistData(string name)
        {
            try
            {
                string registData;
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE\\MMC\\Mspace", true);
                if (software == null)
                    return "";
                registData = software.GetValue(name).ToString();
                return registData;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
