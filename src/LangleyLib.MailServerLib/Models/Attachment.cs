using System;
using System.Collections.Generic;
using System.Text;

namespace LangleyLib.MailServerLib.Models
{
    public class Attachment
    {
        internal Attachment() { }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
    }
}
