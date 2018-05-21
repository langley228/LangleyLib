using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// Filter 抽象類別
    /// </summary>
    public abstract class AopFilterAttribute : AopMethodAttribute
    {
        /// <summary>發生例外</summary>
        /// <param name="className">className</param>
        /// <param name="methodName">methodName</param>
        /// <param name="ex"></param>
        public abstract void OnMethodException(string className, string methodName, Exception ex);
        /// <summary>執行後</summary>
        /// <param name="className">className</param>
        /// <param name="methodName">methodName</param>
        /// <param name="outArgs">標記為 ref 或 out 參數的指定引數</param>
        /// <param name="returnValue">傳回值</param>
        public abstract void OnMethodAfter(string className, string methodName, object[] outArgs, object returnValue);

        /// <summary>執行前</summary>
        /// <param name="className">className</param>
        /// <param name="methodName">methodName</param>    
        /// <param name="inArgs"> 未標記為 out 參數的引數的陣列。</param>
        public abstract void OnMethodBefore(string className, string methodName, object[] inArgs);

        /// <summary>執行前</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>      
        /// <param name="inArgs"> 未標記為 out 參數的引數的陣列。</param>
        internal override void OnMethodBefore(MarshalByRefObject obj, MethodBase method, object[] inArgs)
        {
            OnMethodBefore(obj.GetType().Name, method.Name, inArgs);
        }
        /// <summary>執行後</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>  
        /// <param name="outArgs">標記為 ref 或 out 參數的指定引數</param>
        /// <param name="returnValue">傳回值</param>
        internal override void OnMethodAfter(MarshalByRefObject obj, MethodBase method, Exception ex, object[] outArgs, object returnValue)
        {
            OnMethodAfter(obj.GetType().Name, method.Name, outArgs, returnValue);
        }
        /// <summary>發生例外</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>
        /// <param name="ex"></param>
        internal override void OnMethodException(MarshalByRefObject obj, MethodBase method, Exception ex)
        {
            OnMethodException(obj.GetType().Name, method.Name, ex);
        }
    }
}
