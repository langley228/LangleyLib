using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.Extensions
{
    /// <summary>
    /// 字串相關 Extensions
    /// </summary>
    public static class StringToExtensions
    {
        /// <summary>半型 轉 全型</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static string ToWchr(this String source)
        {
            StringBuilder sb = new StringBuilder();
            int iAsc = 0;
            foreach (char item in source)
            {
                iAsc = Convert.ToInt32(item);
                if (iAsc == 32)
                    sb.Append(Convert.ToChar(12288));
                else
                {
                    if (iAsc < 127)
                        iAsc = iAsc + 65248;
                    sb.Append(Convert.ToChar(iAsc));
                }
            }
            return sb.ToString();
        }
        /// <summary>全型 轉 半型</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static string ToNchr(this String source)
        {
            StringBuilder sb = new StringBuilder();
            int iAsc = 0;
            foreach (char item in source.Replace("〔", "[").Replace("〕", "]").Replace("'", "'").ToCharArray())
            {
                iAsc = Convert.ToInt32(item);
                if (iAsc == 12288)
                    sb.Append(Convert.ToChar(32));
                else
                {
                    if (iAsc > 65280 && iAsc < 65375)
                        iAsc = iAsc - 65248;
                    sb.Append(Convert.ToChar(iAsc));
                }
            }
            return sb.ToString();
        }
    }
}
