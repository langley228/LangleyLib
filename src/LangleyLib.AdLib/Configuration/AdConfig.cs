using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Configuration
{
    /// <summary>
    /// AdConfig
    /// </summary>
    public class AdConfig
    {
        private System.Configuration.Configuration m_Config;
        private AppSettingsSection m_AppSettings;
        private string m_ModuleName = typeof(AdConfig).Module.Name;
        private string m_FileName;
        internal AdConfig()
            : this(string.Empty)
        {
        }
        internal AdConfig(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                m_FileName = GetModuleConfigPath();
            else
                m_FileName = filename;
            m_AppSettings = Config.AppSettings;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public System.Configuration.Configuration Config
        {
            get
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = m_FileName;
                return m_Config ?? (m_Config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None));
            }
        }

        /// <summary>
        /// MailServer 模組 (dll) 的 config 路徑
        /// </summary>
        /// <returns></returns>
        private string GetModuleConfigPath()
        {
            return string.Format(@"{0}\{1}.config", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, m_ModuleName);
        }

        /// <summary>      
        /// 取得 AppSetting 設定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public string GetSetting(string key, string defValue = "")
        {
            string value = defValue;
            if (m_AppSettings.Settings[key] != null)
                value = m_AppSettings.Settings[key].Value;
            return value;
        }


        /// <summary>
        /// 取得AD 設定檔
        /// </summary>
        /// <param name="settingKey">key</param>
        /// <returns></returns>
        public AdSetting GetAdSetting(string settingKey)
        {
            AdServer adServerSetting = (AdServer)Config.GetSection("AdServer");
            return adServerSetting.AdSettings[settingKey];
        }
    }
}
