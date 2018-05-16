using LangleyLib.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions
{
    /// <summary>
    /// 字串相關 Extensions
    /// </summary>
    public static class StringExtensions
    {
        public static string MaskString(this string source, char charMask, int start, int length, bool isWChar)
        {
            int startIndex = start - 1;
            if (string.IsNullOrEmpty(source))
                return source;
            if (start < 1)
                return source;
            if (start > source.Length)
                return source;
            string strMask = "".PadRight(length, charMask);
            if (isWChar)
                strMask = strMask.ToWchr();
            if (startIndex + length -1> source.Length)
                return string.Format("{0}{1}", source.Substring(0, startIndex),
                                                       strMask.Substring(0, source.Length - startIndex));
            else
                return string.Format("{0}{1}{2}", source.Substring(0, startIndex), strMask,
                                                       source.Substring(startIndex + length , source.Length - startIndex - length));
        }
        /// <summary>根據陣列中的字串分割字串成子字串。您可以指定子字串是否包含空的陣列元素。</summary>
        /// <param name="source">字串值</param>
        /// <param name="separators">字串陣列 (可分隔這個字串中的子字串)</param>
        /// <returns></returns>
        public static string[] Split(this string source, string[] separators)
        {
            return source.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>根據陣列中的字串分割字串成子字串。您可以指定子字串是否包含空的陣列元素。</summary>
        /// <param name="source">字串值</param>
        /// <param name="separator">字串(可分隔這個字串中的子字串)</param>
        /// <returns></returns>
        public static string[] Split(this string source, string separator)
        {
            return source.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary> Substring 改良版</summary>
        /// <param name="source">字串值</param>
        /// <param name="indexStartOrNext">起始字元位置, 且回傳下一個起始位置</param>
        /// <param name="length">字元數</param>
        /// <returns></returns>
        public static string Substring(this string source, ref int indexStartOrNext, int length)
        {
            int indexStart = indexStartOrNext;
            indexStartOrNext += length;
            return source.Substring(indexStart, length);
        }
        /// <summary>傳回值，這個值表示指定的子字串是否會出現在這個字串內。</summary>
        /// <param name="source">字串值</param>
        /// <param name="opt">搜尋模式</param>
        /// <param name="finds">要搜尋的字串。</param>
        /// <returns></returns>
        public static bool Contains(this string source, StringFindOpt opt, params string[] finds)
        {
            bool bRet = false;
            foreach (var item in finds)
            {
                bool bOne = source.Contains(item);
                switch (opt)
                {
                    case StringFindOpt.Or:
                        if (bOne)
                            return bOne;
                        break;
                    case StringFindOpt.And:
                        if (!bOne)
                            return bOne;
                        break;
                    default:
                        break;
                }
            }
            return bRet;
        }

        /// <summary>字串 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">字串值</param>
        /// <param name="defaultValue">列舉預設值</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            TEnum? retValue = value.ToEnumOrNull<TEnum>();
            return retValue ?? defaultValue;
        }
        /// <summary>字串 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">字串值</param>
        /// <returns>null 表示 Mapping 不到</returns>
        public static TEnum? ToEnumOrNull<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (type.IsEnum)
            {
                try
                {
                    foreach (var intValue in Enum.GetValues(type))
                    {
                        TEnum mapping = (TEnum)Enum.Parse(type, intValue.ToString(), true);
                        if (mapping.ToString() == value)
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
        /// <param name="defaultValue">列舉預設值</param>
        /// <returns></returns>
        public static TEnum MappingToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            TEnum? retValue = value.ToEnumOrNull<TEnum>();
            return retValue ?? defaultValue;
        }
        /// <summary>字串 轉 列舉</summary>
        /// <typeparam name="TEnum">列舉型態</typeparam>
        /// <param name="value">字串值</param>
        /// <returns>null 表示 Mapping 不到</returns>
        public static TEnum? MappingToEnumOrNull<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (type.IsEnum)
            {
                try
                {
                    foreach (var intValue in Enum.GetValues(type))
                    {
                        TEnum mapping = (TEnum)Enum.Parse(type, intValue.ToString(), true);
                        if (mapping.MappingValue() == value)
                            return mapping;
                    } 
                }
                catch (Exception) { }
            }
            return null;
        }
    }
}
