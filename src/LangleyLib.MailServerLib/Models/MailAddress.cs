using System;
using System.Collections.Generic;
using System.Text;

namespace LangleyLib.MailServerLib.Models
{
    public class MailAddress
    {
        internal MailAddress() { }
        public string Address { get; set; }
        public string DisplayName { get; set; }
    }
}
