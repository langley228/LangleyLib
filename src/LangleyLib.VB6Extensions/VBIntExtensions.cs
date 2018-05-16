using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.VB6Extensions
{
    /// <summary>
    /// 仿造 VB6 相關功能 的 Extensions
    /// </summary>
    public static class VBIntExtensions
    {
        /// <summary>
        /// 傳回與指定字元碼關聯的字元
        /// </summary>
        /// <param name="Num">字元的「字碼指標」或字元碼的 Integer 運算式。</param>
        /// <returns></returns>
        public static char VBChr(this int Num)
        {
            char C = Convert.ToChar(Num);
            return C;
        }
        /// <summary>
        /// 回傳數個空白的字串
        /// </summary>
        /// <param name="Number">空白數量</param>
        /// <returns></returns>
        public static string VBSpace(this int Number)
        {
            if (Number > 0)
                return "".PadRight(Number);
            else
                return "";
        }
        /// <summary>
        /// 回傳數個0的字串
        /// </summary>
        /// <param name="Number">0數量</param>
        /// <returns></returns>
        public static string VBZero(this int Number)
        {
            if (Number > 0)
                return "0".PadRight(Number, '0');
            else
                return "";
        }
    }
}
