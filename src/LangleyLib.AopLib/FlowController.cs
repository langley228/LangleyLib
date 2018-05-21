using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// 流程執行控制 Controller
    /// </summary>
    /// <typeparam name="TFlow">FlowRoute</typeparam>
    public abstract class FlowController<TFlow> where TFlow : FlowRoute 
    {
        /// <summary>
        /// 開始執行
        /// </summary>
        public void Start()
        {
            FlowRoute  flow = System.Activator.CreateInstance<TFlow>();
            var attrFlowStart = flow.GetType().GetCustomAttribute<FlowStartAttribute>();
            if (attrFlowStart != null)
                flow.NextMethod = flow.GetType().GetMethod(attrFlowStart.Name);
            if (flow.NextMethod == null)
                flow.NextMethod = typeof(TFlow).GetMethod("Start");
            while (flow.NextMethod != null)
            {
                var f = flow.NextMethod;
                flow.NextMethod = null;
                try
                {
                    f.Invoke(flow, null);
                }
                catch (Exception)
                {
                }
            }

        }
    }
}
