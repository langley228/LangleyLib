using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Sample.Domains.WinNT.Models
{
    public class WinUser
    {
        [Property(AdType.LDAP, "SamAccountname")]
        [Property(AdType.WinNT, "Name")]
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
