using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// 標示 結束的 Flow
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FlowEndAttribute : FlowNextAttribute
    {
        /// <summary>
        /// FlowEndAttribute 建構子
        /// </summary>
        public FlowEndAttribute() : base(string.Empty)
        {
        }
        /// <summary>執行後</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>  
        /// <param name="outArgs">標記為 ref 或 out 參數的指定引數</param>
        /// <param name="returnValue">傳回值</param>
        internal override void OnMethodAfter(MarshalByRefObject RefObj, MethodBase method, Exception ex, object[] outArgs, object returnValue)
        {

        }
        /// <summary>發生例外</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>
        /// <param name="ex"></param>
        internal override void OnMethodException(MarshalByRefObject RefObj, MethodBase method, Exception ex)
        {
            var propFlow = RefObj.GetType().GetProperty("NextMethod");
            if (propFlow != null)
            {
                MethodInfo fun = null;
                if (!string.IsNullOrEmpty(this.Exception))
                    fun = RefObj.GetType().GetMethod(this.Exception);
                if (fun != null)
                    propFlow.SetValue(RefObj, fun);
            }
        }
    }
}
