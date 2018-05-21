using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LangleyLib.AopLib
{
    //貼在類上的標簽
    /// <summary>
    /// 標示 Filter 物件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class AopContextAttribute : ContextAttribute, IContributeObjectSink
    {
        /// <summary>
        /// FilterContextAttribute
        /// </summary>
        public AopContextAttribute()
            : base("AopContext")
        { }

        //實現IContributeObjectSink接口當中的消息接收器接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink next)
        {
            return new AopHandler(obj, next);
        }

        //public override void GetPropertiesForNewContext(IConstructionCallMessage ctor)
        //{
        //    ctor.ContextProperties.Add(new ContextProperty());
        //}
        //public override bool IsContextOK(Context ctx,
        //    IConstructionCallMessage ctorMsg)
        //{ return false; }
    }

}
