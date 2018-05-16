using LangleyLib.AdLib.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{
    /// <summary>  
    /// 用來存取 AD 資料的 介面
    /// </summary>
    public interface IAdService : IDisposable
    {
        /// <summary>
        /// AD 設定檔
        /// </summary>
        AdSetting Setting { get; }
        /// <summary>
        /// AD 網域服務階層
        /// </summary>
        DirectoryEntry Root { get; }
    }
}
