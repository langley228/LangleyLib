using LyExtLib.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.Extensions
{
    /// <summary>
    /// Enum 相關功能 的 Extensions 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 將Enum轉為數字
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int ToIntValue<TEnum>(this TEnum self) where TEnum : struct, IConvertible
        {
            return Convert.ToInt16(self);
        }


        /// <summary>
        /// 取得Enum的描述標籤內容
        /// </summary>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum self) where TEnum : struct, IConvertible
        {
            FieldInfo fi = self.GetType().GetField(self.ToString());
            DescriptionAttribute[] attributes = null;

            if (fi != null)
            {
                attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
            }

            return self.ToString();
        }
        /// <summary>
        /// 列出所有名稱
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string[] ToStringArray<TEnum>(this TEnum self) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Enum expected.", "TEnum");
            }
            return Enum.GetNames(type);
        }
        /// <summary>
        /// 列出所有值
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Array ToIntArray<TEnum>(this TEnum self) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Enum expected.", "TEnum");
            }
            return Enum.GetValues(type);
        }


        /// <summary>
        /// 將Enum轉為 DBValue
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string MappingValue<TEnum>(this TEnum self) where TEnum : struct, IConvertible
        {
            FieldInfo fi = self.GetType().GetField(self.ToString());
            MappingAttribute[] attributes = null;
            if (fi != null)
            {
                attributes =
                    (MappingAttribute[])fi.GetCustomAttributes(typeof(MappingAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Value;
            }

            return self.ToIntValue().ToString();
        }
    }
}
