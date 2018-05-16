using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LangleyLib.MailServerLib.Configuration
{        
    public class SmtpSetting : ConfigurationElement
    {
        [ConfigurationProperty("Key", DefaultValue = "SmtpSetting", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value.ToString(); }
        }
        [ConfigurationProperty("DisplayName", DefaultValue = "Name", IsRequired = true)]
        public string DisplayName
        {
            get { return (string)this["DisplayName"]; }
            set { this["DisplayName"] = value; }
        }
        [ConfigurationProperty("Address", DefaultValue = "Address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["Address"]; }
            set { this["Address"] = value; }
        }
        [ConfigurationProperty("UserName", DefaultValue = "UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["UserName"]; }
            set { this["UserName"] = value; }
        }
        [ConfigurationProperty("Password", DefaultValue = "Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
        [ConfigurationProperty("Host", DefaultValue = "Host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["Host"]; }
            set { this["Password"] = value; }
        }
        [ConfigurationProperty("Port", DefaultValue = 25, IsRequired = true)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }
        [ConfigurationProperty("EnableSsl", DefaultValue = false, IsRequired = true)]
        public bool EnableSsl
        {
            get { return (bool)this["EnableSsl"]; }
            set { this["EnableSsl"] = value; }
        }
    }
}
