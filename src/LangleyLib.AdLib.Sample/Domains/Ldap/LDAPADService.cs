using LangleyLib.AdLib.Sample.Domains.Ldap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Sample.Domains.Ldap
{
    public class LDAPADService : AdServiceBase
    {
        public LDAPADService(string settingkey)
            : base(settingkey)
        {
        }
        private IAdSet<LDAPUser> m_LDAPUser;
        public IAdSet<LDAPUser> LDAPUser
        {
            get { return m_LDAPUser ?? (m_LDAPUser = CreateAdSet<LDAPUser>("(objectClass=user)(objectCategory=person)")); }
        }
    }
}
