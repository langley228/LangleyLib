
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.VB6Extensions
{
    /// <summary>
    /// 仿造 VB6 相關功能 的 Extensions
    /// </summary>
    public static class VBStringExtensions
    {
        /// <summary>第一個字元長度 (全形算2個長度)</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        internal static int FirstLen(this string source)
        {
            //字串之第一個字的位元組長度
            if (source.Length == 0)
                return 0;
            else
            {
                int iAsc = Convert.ToInt32(source[0]);
                if (iAsc >= 0 && iAsc <= 255)
                    return 1;
                else
                    return 2;
            }
        }
        /// <summary> 等同 VB Mid </summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <returns>字串，包含字串中從指定之位置開始的指定數目字元。</returns>
        public static string VBMid(this string source, int start)
        {
            return source.VBMid(start, source.Length);
        }
        /// <summary> 等同 VB Mid </summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <param name="maxLength">字元數。</param>
        /// <returns>字串，包含字串中從指定之位置開始的指定數目字元。</returns>
        public static string VBMid(this string source, int start, int maxLength)
        {
            int startIndex = start - 1;
            int length = maxLength;
            if (start > source.Length)
                return "";
            if (startIndex + length > source.Length)
                length = source.Length - startIndex;
            string ret = source.Substring(startIndex, length);
            return ret;
        }

        /// <summary> 等同 VB Left </summary>
        /// <param name="source">字串值</param>
        /// <param name="maxLength">字元數</param>
        /// <returns></returns>
        public static string VBLeft(this string source, int maxLength)
        {
            return source.VBMid(1, maxLength);
        }

        /// <summary> 等同 VB Right </summary>
        /// <param name="source">字串值</param>
        /// <param name="maxLength">字元數</param>
        /// <returns></returns>
        public static string VBRight(this string source, int maxLength)
        {
            if (maxLength >= source.Length)
                return source;
            else
                return source.VBMid(source.Length - maxLength + 1, maxLength);
        }
        /// <summary>等同 VB InStr</summary>
        /// <param name="source">字串值</param>
        /// <param name="start">起始位置 (以 1 為起始)</param>
        /// <param name="find">要尋找的字串</param>
        /// <returns></returns>
        public static int VBInStr(this string source, int start, string find)
        {
            return source.IndexOf(find, start - 1) + 1;
        }
        /// <summary>等同 VB LenB</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static int VBLenB(this string source)
        {
            //如同VB 6.0的LenB函數，傳回字串aStr的位元組長度
            int i = 0;
            int k = 0;
            for (i = 0; i < source.Length; i++)
            {
                k += source.Substring(i, 1).FirstLen();
            }
            return k;
            //return System.Text.Encoding.Unicode.GetByteCount(source);
        }

        /// <summary>
        /// 傳回輸入字元的「字碼指標」(Code Point) 或字元碼。
        /// </summary>
        /// <param name="source">字串的第一個字元</param>
        /// <returns></returns>
        public static int VBAsc(this string source)
        {
            int N = Convert.ToInt32(source[0]);
            return N;
        }

        /// <summary> 插入字串( 相當於C# insert) </summary>
        /// <param name="source">原始字串值</param>
        /// <param name="start">插入的位置 (以 1 為起始)。</param>
        /// <param name="insert">要插入的字串</param>
        /// <returns>
        /// 回傳在原始字串值(source)中的位置(start)插入字串 (insert)的新字串
        /// 若插入的位置不在原始字串值(source)中，則回傳原始字串值(source)
        /// </returns>
        public static string VBInsert(this string source, int start, string insert)
        {
            int startIndex = start - 1;
            if (string.IsNullOrEmpty(source))
                return source;
            if (start < 1)
                return source;
            if (start > source.Length)
                return source;

            string ret = source.Insert(startIndex, insert);
            return ret;
        }

        /// <summary> 插入字串( 相當於C# insert) 並取代 </summary>
        /// <param name="source">原始字串值</param>
        /// <param name="start">插入的位置 (以 1 為起始)。</param>
        /// <param name="insert">要插入的字串</param>
        /// <returns></returns>
        public static string VBInsertReplace(this string source, int start, string insert)
        {
            return source.VBInsertReplace(start, insert.Length, insert);
        }
        /// <summary> 插入字串( 相當於C# Insert) 並取代 </summary>
        /// <param name="source">原始字串值</param>
        /// <param name="start">插入的位置 (以 1 為起始)。</param>
        /// <param name="length">要置換的長度</param>
        /// <param name="insert">要插入的字串</param>
        /// <returns></returns>
        public static string VBInsertReplace(this string source, int start, int length, string insert)
        {
            int startIndex = start - 1;
            if (string.IsNullOrEmpty(source))
                return source;
            if (start < 1)
                return source;
            if (start > source.Length)
                return source;
            string ret = string.Format("{0}{1}{2}", source.VBMid(1, start - 1), insert,
                                                    source.VBMid(start + length, source.Length - start - length + 1));
            return ret;
        }

    }
}
