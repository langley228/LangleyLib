using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.DateFormat.Extensions
{
    /// <summary>
    /// 日期相關 Extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>日期格式 轉 民國年字串
        /// 利用民國月曆轉成任何的 date time format</summary>
        /// <param name="value">日期值</param>
        /// <param name="format">
        /// 字串所顯示的日期格式 ex: yyyMMdd HH:mm:ss、yyyMMdd、yyMMdd
        /// </param>
        /// <returns></returns>
        public static string ToTWDateString(this DateTime? value, string format = "yyyMMdd")
        {
            if (value.HasValue)
                return value.Value.ToTWDateString(format);
            else
                return "0";
        }
        /// <summary>日期格式 轉 民國年字串
        /// 利用民國月曆轉成任何的 date time format</summary>
        /// <param name="value">日期值</param>
        /// <param name="dateFormat">
        /// 字串所顯示的日期格式 ex: yyyMMdd HH:mm:ss、yyyMMdd、yyMMdd
        /// </param>
        /// <returns></returns>
        public static string ToTWDateString(this DateTime value, string dateFormat = "yyyMMdd")
        {
            CultureInfo tw = new CultureInfo("zh-TW");
            tw.DateTimeFormat.Calendar = DateFormatCalendar.TW;
            DateTime minimumDate = DateFormatCalendar.TW.MinSupportedDateTime;

            if (value < minimumDate)
            {
                return null;
            }

            //年有可能在不同位置 所以抽出來另外處理
            string formatTmp = dateFormat.Replace("y", "x");    //先把y 換成x避免年被格式化
            string ret = value.ToString(formatTmp);
            //string sYear = value.ToString("yy", tw);             //取得民國年
            string sYear = value.ToString("yy", tw);             //取得民國年
            ret = ret.Replace("xxxx", sYear.PadLeft(4, '0'));   //格式化4碼
            ret = ret.Replace("xxx", sYear.PadLeft(3, '0'));    //格式化3碼
            ret = ret.Replace("xx", sYear.PadLeft(2, '0'));     //格式化2碼
            ret = ret.Replace("x", sYear);                      //格式化1碼
            return ret;
        }
    }
}
