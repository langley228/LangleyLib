using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// 標示 起始 Flow
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FlowStartAttribute : Attribute
    {
        /// <summary>
        /// FlowStartAttribute 建構子
        /// </summary>
        /// <param name="name">起始 Flow</param>
        public FlowStartAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 起始 Flow
        /// </summary>
        public string Name { get; set; }
    }
}
