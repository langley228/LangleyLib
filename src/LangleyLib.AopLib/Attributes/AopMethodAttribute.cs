using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LangleyLib.AopLib
{
    /// <summary>    
    /// Method 抽象類別
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public abstract class AopMethodAttribute : Attribute//, IMethodFilter
    {
        /// <summary>執行前</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>      
        /// <param name="inArgs"> 未標記為 out 參數的引數的陣列。</param>
        internal abstract void OnMethodBefore(MarshalByRefObject obj, MethodBase method, object[] inArgs);
        /// <summary>執行後</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>  
        /// <param name="outArgs">標記為 ref 或 out 參數的指定引數</param>
        /// <param name="returnValue">傳回值</param>
        internal abstract void OnMethodAfter(MarshalByRefObject obj, MethodBase method, Exception ex, object[] outArgs, object returnValue);
        /// <summary>發生例外</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>
        /// <param name="ex"></param>
        internal abstract void OnMethodException(MarshalByRefObject obj, MethodBase method, Exception ex);

    }
}
