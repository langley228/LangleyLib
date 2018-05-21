using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>    
    /// FlowNext 標示
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FlowNextAttribute : AopMethodAttribute
    {
        /// <summary>
        /// FlowNextAttribute 建構子
        /// </summary>
        /// <param name="name">下一個 Flow</param>
        public FlowNextAttribute(string name)
        {
            Name = name;
        }
        /// <summary>執行後</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>  
        /// <param name="outArgs">標記為 ref 或 out 參數的指定引數</param>
        /// <param name="returnValue">傳回值</pa
        internal override void OnMethodAfter(MarshalByRefObject obj, MethodBase method, Exception ex, object[] outArgs, object returnValue)
        {
            var propFlow = obj.GetType().GetProperty("NextMethod");
            if (propFlow != null)
            {
                if (!string.IsNullOrEmpty(this.Name) && ex == null)
                {
                    var fun = obj.GetType().GetMethod(this.Name);
                    propFlow.SetValue(obj, fun);
                }
            }
        }
        /// <summary>執行前</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>      
        /// <param name="inArgs"> 未標記為 out 參數的引數的陣列。</param>
        internal override void OnMethodBefore(MarshalByRefObject obj, MethodBase method, object[] inArgs)
        {

        }
        /// <summary>發生例外</summary>
        /// <param name="obj">Object</param>
        /// <param name="method">MethodBase</param>
        /// <param name="ex"></param>
        internal override void OnMethodException(MarshalByRefObject obj, MethodBase method, Exception ex)
        {
            var propFlow = obj.GetType().GetProperty("NextMethod");
            if (propFlow != null)
            {
                MethodInfo fun = null;
                if (!string.IsNullOrEmpty(this.Exception))
                    fun = obj.GetType().GetMethod(this.Exception);
                if (fun == null)
                {
                    var attrFlowException = obj.GetType().GetCustomAttribute<FlowExceptionAttribute>();
                    if (attrFlowException != null)
                        fun = obj.GetType().GetMethod(attrFlowException.Name);
                }
                if (fun != null)
                    propFlow.SetValue(obj, fun);
            }
        }

        /// <summary>
        /// 下一個 Flow
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 例外 Flow 
        /// </summary>
        public string Exception { get; set; }

    }
}
