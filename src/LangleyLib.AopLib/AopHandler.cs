using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{

    //AOP方法處理類，實現了IMessageSink接口，以便返回给IContributeObjectSink接口的GetObjectSink方法
    public sealed class AopHandler : IMessageSink
    {
        //下一個接收器
        private IMessageSink nextSink;
        private MarshalByRefObject refObj;
        public IMessageSink NextSink
        {
            get { return nextSink; }
        }
        public MarshalByRefObject RefObj
        {
            get { return refObj; }
        }
        public AopHandler(MarshalByRefObject obj, IMessageSink nextSink)
        {
            this.refObj = obj;
            this.nextSink = nextSink;
        }

        //同步處理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage retMsg = null;

            //方法調用消息接口
            IMethodCallMessage call = msg as IMethodCallMessage;

            Attribute[] methodAttrs = Attribute.GetCustomAttributes(call.MethodBase, typeof(AopMethodAttribute), true);
            List<AopMethodAttribute> methodFilters = null;
            if (methodAttrs != null && methodAttrs.Length > 0)
                methodFilters = methodAttrs.Select(m => m as AopMethodAttribute).Where(m => m != null).ToList();
            bool hasMethodFilter = (methodFilters != null && methodFilters.Any());
            Attribute[] classAttrs = Attribute.GetCustomAttributes(call.MethodBase.DeclaringType, typeof(AopMethodAttribute));
            List<AopMethodAttribute> classFilters = null;
            if (classAttrs != null && classAttrs.Length > 0)
                classFilters = classAttrs.Select(m => m as AopMethodAttribute).Where(m => m != null).ToList();
            bool hasClassFilter = (classFilters != null && classFilters.Any());
            bool hasFilter = hasMethodFilter || hasClassFilter;


            //如果被調用的方法沒打MethodFilterAttribute標簽
            //methodAttrs == null || methodAttrs.Length == 0)
            //if (call == null || call.MethodName == ".ctor" || !hasFilter)
            if (call == null || call.MethodBase.IsSpecialName || !hasFilter)
                retMsg = nextSink.SyncProcessMessage(msg);

            //如果打了MethodFilterAttribute標簽
            else
            {
                #region OnMethodBefore
                if (classFilters != null && classFilters.Count > 0)
                    classFilters.ForEach(f => f.OnMethodBefore(refObj, call.MethodBase, call.InArgs));
                if (methodFilters != null && methodFilters.Count > 0)
                    methodFilters.ForEach(f => f.OnMethodBefore(refObj, call.MethodBase, call.InArgs));
                #endregion

                retMsg = nextSink.SyncProcessMessage(msg);
                IMethodReturnMessage methodReturn = retMsg as IMethodReturnMessage;
                Exception ex = methodReturn.Exception;
                if (ex != null)
                {
                    #region OnMethodBefore
                    if (methodFilters != null && methodFilters.Count > 0)
                        methodFilters.ForEach(f => f.OnMethodException(refObj, call.MethodBase, methodReturn.Exception));
                    if (classFilters != null && classFilters.Count > 0)
                        classFilters.ForEach(f => f.OnMethodException(refObj, call.MethodBase, methodReturn.Exception));
                    #endregion
                }

                #region OnMethodAfter

                object returnValue = methodReturn.ReturnValue;
                if (returnValue != null && returnValue.GetType().ToString() == "System.Void")
                    returnValue = null;

                if (methodFilters != null && methodFilters.Count > 0)
                    methodFilters.ForEach(f => f.OnMethodAfter(refObj, call.MethodBase, ex, methodReturn.OutArgs, returnValue));
                if (classFilters != null && classFilters.Count > 0)
                    classFilters.ForEach(f => f.OnMethodAfter(refObj, call.MethodBase, ex, methodReturn.OutArgs, returnValue));
                #endregion
            }

            return retMsg;
        }

        //異步處理方法（不需要）
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }
    }
}
