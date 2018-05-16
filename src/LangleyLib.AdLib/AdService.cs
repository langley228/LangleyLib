using LangleyLib.AdLib.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{
    /// <summary>
    /// 用來存取 AD 資料的 抽象類別
    /// </summary>
    public abstract class AdServiceBase : IAdService, IDisposable
    {
        /// <summary>
        /// AD 設定檔
        /// </summary>
        protected AdSetting m_Setting;
        /// <summary>
        /// AD 網域服務階層
        /// </summary>
        protected DirectoryEntry m_Root;
        /// <summary>
        /// 建構子: 用來存取 AD 資料的 抽象類別
        /// </summary>
        /// <param name="settingkey">AD 設定檔 Key</param>
        public AdServiceBase(string settingkey)
            : this(ConfigManager.GetConfig().GetAdSetting(settingkey))
        {
        }
        /// <summary>
        /// 建構子: 用來存取 AD 資料的 抽象類別
        /// </summary>
        /// <param name="setting">AD 設定檔</param>
        public AdServiceBase(AdSetting setting)
        {
            m_Setting = setting;
        }
        /// <summary>
        /// AD 設定檔
        /// </summary>
        public AdSetting Setting
        {
            get { return m_Setting; }
        }
        /// <summary>
        /// AD 網域服務階層
        /// </summary>
        public DirectoryEntry Root
        {
            get { return m_Root ?? (m_Root = new DirectoryEntry(m_Setting.Path, m_Setting.DomainUser, m_Setting.Password)); }
        }

        /// <summary>
        /// 釋放 資源
        /// </summary>
        public virtual void Dispose()
        {
            if (m_Root != null)
                m_Root.Dispose();
        }
        /// <summary>   
        /// 產生 AdSet
        /// </summary>                       
        /// <typeparam name="T">Mdoel 型別</typeparam> 
        /// <param name="classFilterOrNames">
        /// 過濾 LDAP   的  classFilter 
        ///     不過濾(預設) : "(objectClass=*)" , 只找 AD User : "(objectClass=user)(objectCategory=person)"
        /// 過濾 WinNT  的  className  
        ///     不過濾(預設) : "" 或 不需傳入 , 只找 AD User : "USER"
        /// </param>
        /// <returns></returns>
        protected IAdSet<T> CreateAdSet<T>(params string[] classFilterOrNames) where T : class
        {
            IAdSet<T> adSet = null;
            switch (m_Setting.AdType)
            {
                case "WinNT":
                    adSet = new WinNTAdSet<T>(this, classFilterOrNames);
                    break;
                case "LDAP":
                    adSet = new LDAPAdSet<T>(this, classFilterOrNames);
                    break;
                default:
                    break;
            }
            return adSet;
        }
    }
}
