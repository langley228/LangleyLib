using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Configuration
{
    public class ConfigManager
    {
        private static AdConfig Current;
        private static readonly object syncRoot = new object();
        public static AdConfig GetConfig()
        {
            if (Current == null) //確保當前沒有這個物件存在
            {
                lock (syncRoot)//鎖住當前狀態
                {
                    if (Current == null)
                        Current = OpenConfig(typeof(AdConfig).Module.Name);
                }
            }
            return Current;
        }
        public static AdConfig OpenConfig(string filename)
        {
            return new AdConfig(filename);
        }
    }
}
