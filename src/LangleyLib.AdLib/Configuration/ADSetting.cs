using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Configuration
{

    /// <summary>
    /// AD 設定檔
    /// </summary>
    public class AdSetting : ConfigurationElement
    {
        /// <summary>
        /// key
        /// </summary>
        [ConfigurationProperty("Key", DefaultValue = "AdSetting", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value.ToString(); }
        }
        /// <summary>
        /// AD 伺服器類型
        /// </summary>
        [ConfigurationProperty("AdType", DefaultValue = "WinNT", IsRequired = true)]
        public string AdType
        {
            get { return (string)this["AdType"]; }
            set { this["AdType"] = value; }
        }
        /// <summary>
        /// AD Path (類似URL的路徑)
        /// </summary>
        [ConfigurationProperty("Path", DefaultValue = "Path", IsRequired = true)]
        public string Path
        {
            get { return (string)this["Path"]; }
            set { this["Path"] = value; }
        }
        /// <summary>
        /// 網域名稱
        /// </summary>
        [ConfigurationProperty("Domain", DefaultValue = "Domain", IsRequired = true)]
        public string Domain
        {
            get { return (string)this["Domain"]; }
            set { this["Domain"] = value; }
        }
        /// <summary>
        /// 要在驗證用戶端時使用的使用者名稱
        /// </summary>
        [ConfigurationProperty("UserName", DefaultValue = "UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["UserName"]; }
            set { this["UserName"] = value; }
        }
        /// <summary>
        /// 要在驗證用戶端時使用的使用者密碼
        /// </summary>
        [ConfigurationProperty("Password", DefaultValue = "Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
        /// <summary>
        /// 網域+帳號
        /// </summary>
        public string DomainUser
        {
            get { return GetDomainUser(Domain, UserName); }
        }

        /// <summary>
        /// 取得 網域+帳號
        /// </summary>
        public string GetDomainUser(string domain, string userName)
        {
            if (string.IsNullOrEmpty(domain))
                return userName;
            else
                return string.Format("{0}\\{1}", domain, userName);
        }
    }
}
