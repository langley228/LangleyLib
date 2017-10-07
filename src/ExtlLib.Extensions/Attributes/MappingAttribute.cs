﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.Extensions.Attributes
{
    /// <summary>
    /// Mapping 值
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MappingAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public MappingAttribute(string value)
        {
            Value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
    }
}