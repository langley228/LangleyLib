using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Configuration
{
    /// <summary>
    /// AdConfig 管理物件
    /// </summary>
    public class ConfigManager
    {
        private static AdConfig Current;
        private static readonly object syncRoot = new object();
        /// <summary>
        /// 取得目前的 AdConfig
        /// </summary>
        /// <returns></returns>
        public static AdConfig GetConfig()
        {
            if (Current == null) //確保當前沒有這個物件存在
            {
                lock (syncRoot)//鎖住當前狀態
                {
                    if (Current == null)
                        Current = OpenConfig();
                }
            }
            return Current;
        }
        /// <summary>
        /// 設定目前的 AdConfig 
        /// </summary>
        /// <param name="filename">Config 檔案路徑</param>
        public static void SetConfig(string filename)
        {
            Current = new AdConfig(filename);
        }
        /// <summary>
        /// 開啟並取得 AdConfig
        /// </summary>
        /// <param name="filename">Config 檔案路徑</param>
        /// <returns></returns>
        public static AdConfig OpenConfig(string filename)
        {
            return new AdConfig(filename);
        }
        /// <summary>
        /// 開啟並取得 AdConfig
        /// </summary>
        /// <returns></returns>
        public static AdConfig OpenConfig()
        {
            return new AdConfig();
        }
    }
}
