using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{

    /// <summary>
    /// 標示 Model 的 屬性 所對應的 AD 欄位
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// 建構子 : 標示 Model 的 屬性 所對應的 AD 欄位
        /// </summary>
        /// <param name="adType">AD 伺服器類型</param>
        /// <param name="propName">AD 欄位名稱</param>
        public PropertyAttribute(AdType adType, string propName)
        {
            AdType = adType;
            PropName = propName;
        }
        /// <summary>
        /// AD 欄位名稱
        /// </summary>
        public string PropName { get; set; }
        /// <summary>
        /// AD 伺服器類型
        /// </summary>
        public AdType AdType { get; set; }
    }
}
