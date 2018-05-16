using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LangleyLib.MailServerLib.Configuration
{
    public class MailServer : ConfigurationSection
    {
        [ConfigurationProperty("SmtpSettings"), ConfigurationCollection(typeof(SmtpSetting))]
        public SmtpSettingCollection SmtpSettings
        {
            get { return this["SmtpSettings"] as SmtpSettingCollection; }
            set { this["SmtpSettings"] = value; }
        }
    }
}
