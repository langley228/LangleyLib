using System.Configuration;

namespace LangleyLib.AdLib.Configuration
{

    /// <summary>
    /// AD 組態檔區段
    /// </summary>
    public class AdServer : ConfigurationSection
    {
        /// <summary>
        /// AD 設定檔 集合
        /// </summary>
        [ConfigurationProperty("AdSettings"), ConfigurationCollection(typeof(AdSetting))]
        public AdSettingCollection AdSettings
        {
            get { return this["AdSettings"] as AdSettingCollection; }
            set { this["AdSettings"] = value; }
        }
    }
}
