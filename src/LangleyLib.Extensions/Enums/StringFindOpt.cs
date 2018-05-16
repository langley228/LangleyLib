using LangleyLib.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions.Enums
{
    /// <summary>搜尋方式</summary>
    public enum StringFindOpt
    {
        /// <summary></summary>
        [Description("Or")]
        [Mapping("Or")]
        Or = 0,
        /// <summary></summary>
        [Description("And")]
        [Mapping("And")]
        And = 1
    }
}
