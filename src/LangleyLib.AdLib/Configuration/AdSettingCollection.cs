using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Configuration
{

    /// <summary>
    /// AD 設定檔 集合
    /// </summary>
    public class AdSettingCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// AD 設定檔
        /// </summary>
        /// <param name="key">AD 設定檔 key</param>
        /// <returns></returns>
        public new AdSetting this[string key]
        {
            get { return (AdSetting)this.BaseGet(key); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AdSetting();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AdSetting)element).Key;
        }

    }
}
