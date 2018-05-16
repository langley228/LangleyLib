using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LangleyLib.MailServerLib.Configuration
{   
    public class SmtpSettingCollection : ConfigurationElementCollection
    {
        public new SmtpSetting this[string key]
        {
            get { return (SmtpSetting)this.BaseGet(key); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SmtpSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SmtpSetting)element).Key;
        }

    }
}
