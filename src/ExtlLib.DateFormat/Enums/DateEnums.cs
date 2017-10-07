using ExtlLib.Extensions.Attributes;
using ExtlLib.DateFormat.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.DateFormat.Enums
{
    /// <summary>
    /// 日期相關列舉
    /// </summary>
    public class DateEnums
    {
        /// <summary>年類型</summary>
        public enum Year
        {
            /// <summary></summary>
            [Description("未知")]
            [Mapping("")]
            None = 0,
            /// <summary>民國年</summary>
            [Description("民國年")]
            [Mapping("TW")]
            TW = 1,
            /// <summary>西元年</summary>
            [Description("西元年")]
            [Mapping("CE")]
            CE = 2
        }
    }
}
