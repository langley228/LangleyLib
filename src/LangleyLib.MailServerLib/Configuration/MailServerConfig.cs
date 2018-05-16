using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.MailServerLib.Configuration
{
    public class MailServerConfig
    {
        private System.Configuration.Configuration m_Config;
        private AppSettingsSection m_AppSettings;
        private string m_Filename = typeof(MailServerConfig).Module.Name;
        internal MailServerConfig()
            : this(typeof(MailServerConfig).Module.Name)
        {
        }
        internal MailServerConfig(string filename)
        {
            m_Filename = filename;
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
                string path = string.Format(@"{0}\{1}.config", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, m_Filename);
                fileMap.ExeConfigFilename = path;
                return m_Config ?? (m_Config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None));
            }
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
        public SmtpSetting GetSmtpSetting(string settingKey)
        {
            MailServer adServerSetting = (MailServer)Config.GetSection("MailServer");
            return adServerSetting.SmtpSettings[settingKey];
        }
    }
}
