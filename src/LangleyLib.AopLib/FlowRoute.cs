using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// 套用 Filter 的 FlowRoute 抽象類別
    /// </summary>
    public abstract class FlowRoute : AopContext
    {
        /// <summary>
        /// 下一個要執行的 Method
        /// </summary>
        public MethodInfo NextMethod { get; set; }
    }
}
