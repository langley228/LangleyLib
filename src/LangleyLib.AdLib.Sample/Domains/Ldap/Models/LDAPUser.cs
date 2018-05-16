using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Sample.Domains.Ldap.Models
{

    public class LDAPUser
    {
        [Property(AdType.LDAP, "SamAccountname")]
        [Property(AdType.WinNT, "Name")]
        public string UserName { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        [Property(AdType.LDAP, "Attribute_1")]
        public string EmployeId { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Property(AdType.LDAP, "mail")]
        public string Email { get; set; }
        /// <summary>
        /// 事業部
        /// </summary>
        [Property(AdType.LDAP, "Attribute_2")]
        public string Dept1 { get; set; }
        /// <summary>
        /// 處級部門
        /// </summary>
        [Property(AdType.LDAP, "Attribute_3")]
        public string Dept2 { get; set; }
        /// <summary>
        /// 部級部門
        /// </summary>
        [Property(AdType.LDAP, "Attribute_4")]
        public string Dept3 { get; set; }
        /// <summary>
        /// 上一階主管
        /// 格式例如: "CN=Xxx Yyy (姓名),OU=xxx部,OUxxx處,OU=xxx事業部,OU=User Accounts,DC=vibo,DC=corp"
        /// </summary>
        [Property(AdType.LDAP, "Attribute_5")]
        public string Manager { get; set; }
        /// <summary>
        /// CN (用來找主管 AD帳號)
        /// 格式例如: "Xxx Yyy (姓名)"
        /// </summary>
        [Property(AdType.LDAP, "cn")]
        public string DisplayName { get; set; }
    }
}
