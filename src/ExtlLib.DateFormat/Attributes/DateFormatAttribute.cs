using ExtlLib.DateFormat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.DateFormat.Attributes
{
    /// <summary>
    /// DateFormat
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DateFormatAttribute : Attribute
    {
        /// <summary>
        /// DateFormat
        /// </summary>
        public DateFormatAttribute()
        {
        }
        /// <summary>
        /// 年類型
        /// </summary>
        public DateEnums.Year Type { get; set; } = DateEnums.Year.CE;
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; } = "yyyyMMdd";
    }
}
