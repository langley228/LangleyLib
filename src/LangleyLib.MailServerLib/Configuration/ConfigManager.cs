using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.MailServerLib.Configuration
{

    public class ConfigManager
    {
        private static MailServerConfig Current;
        private static readonly object syncRoot = new object();
        public static MailServerConfig GetConfig()
        {
            if (Current == null) //確保當前沒有這個物件存在
            {
                lock (syncRoot)//鎖住當前狀態
                {
                    if (Current == null)
                        Current = OpenConfig(typeof(MailServerConfig).Module.Name);
                }
            }
            return Current;
        }
        public static MailServerConfig OpenConfig(string filename)
        {
            return new MailServerConfig(filename);
        }
    }
}
