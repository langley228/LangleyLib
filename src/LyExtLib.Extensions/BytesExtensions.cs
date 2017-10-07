using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.Extensions
{

    /// <summary>Byte陣列 相關功能 的 Extensions</summary>
    public static class BytesExtensions
    {
        /// <summary> Byte陣列 Substring </summary>
        /// <param name="source">Byte陣列值</param>
        /// <param name="indexStartOrNext">起始字元位置, 且回傳下一個起始位置</param>
        /// <param name="length">字元數</param>
        /// <returns></returns>

        public static string Substring(this byte[] source, ref int indexStartOrNext, int length)
        {
            int startIndex = indexStartOrNext;
            indexStartOrNext += length;
            return Encoding.Default.GetString(source, startIndex, length);
        }

        /// <summary> Byte陣列 Substring </summary>
        /// <param name="source">Byte陣列值</param>
        /// <param name="indexStart">起始字元位置</param>
        /// <param name="length">字元數</param>
        /// <returns></returns>
        public static string Substring(this byte[] source, int indexStart, int length)
        {
            return Encoding.Default.GetString(source, indexStart, length);
        }
        /// <summary> Byte陣列 Substring </summary>
        /// <param name="source">Byte陣列值</param>
        /// <param name="indexStart">起始字元位置</param>
        /// <returns></returns>
        public static string Substring(this byte[] source, int indexStart)
        {
            int length = source.Length - indexStart;
            return Encoding.Default.GetString(source, indexStart, length);
        }
    }
}
