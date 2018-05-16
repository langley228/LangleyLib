using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions
{
    /// <summary>
    /// int 相關功能 的 Extensions 
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>Int 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">Int 值</param>
        /// <param name="defaultValue">列舉預設值</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this int? value, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            TEnum? tryValue = value.ToEnumOrNull<TEnum>();
            if (tryValue.HasValue)
                return tryValue.Value;
            else
                return defaultValue;
        }
        /// <summary>Int 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">Int 值</param>
        /// <param name="defaultValue">列舉預設值</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            TEnum? tryValue= value.ToEnumOrNull<TEnum>();
            if (tryValue.HasValue)
                return tryValue.Value;
            else
                return defaultValue;
         }

        /// <summary>字串 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">字串值</param>
        /// <returns>null 表示 Mapping 不到</returns>
        public static TEnum? ToEnumOrNull<TEnum>(this int value) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (type.IsEnum)
            {
                try
                {
                    foreach (var intValue in Enum.GetValues(type))
                    {
                        TEnum mapping = (TEnum)Enum.Parse(type, intValue.ToString(), true);
                        if (mapping.ToIntValue() == value)
                            return mapping;
                    }
                }
                catch (Exception) { }
            }
            return null;
        }
        /// <summary>字串 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">字串值</param>
        /// <returns>null 表示 Mapping 不到</returns>
        public static TEnum? ToEnumOrNull<TEnum>(this int? value) where TEnum : struct, IConvertible
        {
            if (!value.HasValue)
                return null;
            return value.Value.ToEnumOrNull<TEnum>();
        }
    }
}
