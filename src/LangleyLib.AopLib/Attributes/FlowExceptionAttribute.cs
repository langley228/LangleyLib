using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib
{
    /// <summary>
    /// 標示 例外 Flow
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FlowExceptionAttribute : Attribute
    {
        /// <summary>
        /// FlowExceptionAttribute 建構子
        /// </summary>
        /// <param name="name">例外 Flow</param>
        public FlowExceptionAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 例外 Flow
        /// </summary>
        public string Name { get; set; }
    }
}
