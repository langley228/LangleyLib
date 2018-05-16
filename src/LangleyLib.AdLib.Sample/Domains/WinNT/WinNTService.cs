using LangleyLib.AdLib.Sample.Domains.WinNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Sample.Domains.WinNT
{
    public class WinNTService : AdServiceBase
    {
        public WinNTService(string settingkey)
            : base(settingkey)
        {
        }
        private IAdSet<WinUser> m_WinUser;
        public IAdSet<WinUser> WinUser
        {
            get { return m_WinUser ?? (m_WinUser = CreateAdSet<WinUser>("USER")); }
        }
    }
}
