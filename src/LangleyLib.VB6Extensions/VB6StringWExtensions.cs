using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.VB6Extensions
{
    /// <summary>
    /// 仿造 VB6 相關功能 的 Extensions 半全形混合版
    /// </summary>
    public static class VB6StringWExtensions
    {
        /// <summary> 等同 VB Mid 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <param name="byteSize">位元組數</param>
        /// <returns></returns>
        public static string VBMidMbcs(this string source, int start, int byteSize)
        {
            return source.VBMidMbcs(start, byteSize, System.Text.Encoding.Default);
        }

        /// <summary> 等同 VB Mid 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <param name="byteSize">位元組數</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string VBMidMbcs(this string source, int start, int byteSize, Encoding encoding)
        {
            byte[] aByte = encoding.GetBytes(source);

            //起始位置超過就取空字串
            if (start - 1 > aByte.Length)
                return "";

            int iCount = aByte.Length - start + 1;
            if (byteSize < iCount)
                iCount = byteSize;

            return encoding.GetString(aByte, start - 1, iCount);
        }

        /// <summary> 等同 VB Len 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static int VBLenMbcs(this string source)
        {
            return source.VBLenMbcs(System.Text.Encoding.Default);
        }

        /// <summary> 等同 VB Len 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static int VBLenMbcs(this string source, Encoding encoding)
        {
            return encoding.GetBytes(source).Length;
        }

        /// <summary> 等同 VB Left 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="byteSize">位元組數</param>
        /// <returns></returns>
        public static string VBLeftMbcs(this string source, int byteSize)
        {
            return source.VBLeftMbcs(byteSize, System.Text.Encoding.Default);
        }

        /// <summary> 等同 VB Left 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="byteSize">位元組數</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string VBLeftMbcs(this string source, int byteSize, Encoding encoding)
        {
            return source.VBMidMbcs(1, byteSize, encoding);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="byteSize">位元組數</param>
        /// <returns></returns>
        public static string VBRightMbcs(this string source, int byteSize)
        {
            return source.VBRightMbcs(byteSize, System.Text.Encoding.Default);
        }

        /// <summary> 等同 VB Right 半全形混和</summary>
        /// <param name="source">字串值</param>
        /// <param name="byteSize">位元組數</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string VBRightMbcs(this string source, int byteSize, Encoding encoding)
        {
            byte[] aByte = encoding.GetBytes(source);
            int iStart = aByte.Length - byteSize + 1;
            int iCount = byteSize;
            if (iStart < 1)
            {
                iStart = 0;
                iCount = aByte.Length;
            }
            return source.VBMidMbcs(iStart, iCount, encoding);
        }



        /// <summary> 插入字串( 相當於C# Insert) 並取代 半全形混合版</summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static string VBInsertReplaceMbcs(this string source, int start, string insert)
        {
            return source.VBInsertReplaceMbcs(start, insert.VBLenMbcs(), insert);
        }

        /// <summary> 插入字串( 相當於C# Insert) 並取代 半全形混合版</summary>
        /// <param name="source">字串值</param>
        /// <param name="start">開始位置 (以 1 為起始)。</param>
        /// <param name="byteSize">位元組數</param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static string VBInsertReplaceMbcs(this string source, int start, int byteSize, string insert)
        {
            int startIndex = start - 1;
            if (string.IsNullOrEmpty(source))
                return source;
            if (start < 1)
                return source;
            if (start > source.VBLenMbcs())
                return source;
            string ret = string.Format("{0}{1}{2}", source.VBMidMbcs(1, start - 1), insert,
                                                    source.VBMidMbcs(start + byteSize, source.VBLenMbcs() - start - byteSize + 1));
            return ret;
        }
    }
}
