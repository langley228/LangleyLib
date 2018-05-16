using LangleyLib.DateFormat.Attributes;
using LangleyLib.DateFormat.Enums;
using LangleyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.DateFormat.Extensions
{
    /// <summary>
    /// 字串相關 Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 取得 DateFormatAttribute
        /// </summary>
        /// <param name="source">日期格式列舉</param>
        /// <returns></returns>
        public static DateFormatAttribute GetDateAttr<TEnum>(this TEnum source) where TEnum : struct, IConvertible
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DateFormatAttribute[] attributes = null;
            DateFormatAttribute attr = null;
            if (fi != null)
            {
                attributes =
                    (DateFormatAttribute[])fi.GetCustomAttributes(typeof(DateFormatAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    attr = attributes[0];
                }
            }
            return attr;
        }

        /// <summary>
        /// 取得 DateFormatAttribute 中的格式
        /// </summary>
        /// <param name="source">日期格式列舉</param>
        /// <returns></returns>
        public static string GetDateFormat<TEnum>(this TEnum source) where TEnum : struct, IConvertible
        {
            DateFormatAttribute attr = source.GetDateAttr();
            if (attr != null)
                return attr.Format;
            else
                return "";
        }
        /// <summary>
        /// 取得 DateFormatAttribute 中的年度類型
        /// </summary>
        /// <param name="source">日期格式列舉</param>
        /// <returns></returns>
        public static DateEnums.Year GetDateType<TEnum>(this TEnum source) where TEnum : struct, IConvertible
        {
            DateFormatAttribute attr = source.GetDateAttr();
            if (attr != null)
                return attr.Type;
            else
                return DateEnums.Year.None;
        }


        /// <summary>
        /// 將字串轉日期
        /// </summary>
        /// <param name="source">字串值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <returns></returns>
        public static DateTime? ToDateTime<TEnum>(this string source, TEnum? fmformat) where TEnum : struct, IConvertible
        {
            if (fmformat == null)
                return null;
            return source.ToDateTime(fmformat.Value);
        }
        /// <summary>
        /// 將字串轉日期
        /// </summary>
        /// <param name="source">字串值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <returns></returns>
        public static DateTime? ToDateTime<TEnum>(this string source, TEnum fmformat) where TEnum : struct, IConvertible
        {
            DateTime? dt = null;
            DateFormatAttribute attr = fmformat.GetDateAttr();
            if (attr != null)
            {
                string format = attr.Format;

                if (!string.IsNullOrEmpty(attr.Format))
                {
                    string txt = source;
                    #region 格式 缺日 補日
                    switch (attr.Type)
                    {
                        case DateEnums.Year.None:
                            break;
                        case DateEnums.Year.TW:
                        case DateEnums.Year.CE:
                            if (format.IndexOf('d') == -1)
                            {
                                //月(M) 在 年(y) 前面 日(d) 就補在前面
                                if (format.IndexOf("y") != -1 && format.IndexOf("M") != -1
                                    && format.IndexOf("M") < format.IndexOf("y"))
                                {
                                    format = $"dd{format}";
                                    txt = $"01{txt}";
                                }
                                else
                                {
                                    format = $"{format}dd";
                                    txt = $"{txt}01";
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    switch (attr.Type)
                    {
                        case DateEnums.Year.None:
                            break;
                        case DateEnums.Year.TW:
                            dt = txt.ToTWDateTime(format);
                            break;
                        case DateEnums.Year.CE:
                            dt = txt.ToDateTime(format);
                            break;
                        default:
                            break;
                    }
                }
            }
            return dt;
        }

        /// <summary>民國年字串轉日期</summary>
        /// <param name="twDate">字串值</param>
        /// <param name="inInformat">
        /// 字串所顯示的日期格式 ex: yyyMMdd HH:mm:ss、yyyMMdd、yyMMdd
        /// </param>
        /// <returns></returns>
        public static DateTime? ToTWDateTime(this string twDate, string inInformat = "yyyMMdd")
        {
            DateTime tryDate = DateTime.Now;
            if (inInformat.IndexOf('y') == 0)
            {
                if (inInformat.IndexOf("yyyy") == 0)
                    inInformat = inInformat.Replace("yyyy", "yyyy");
                else if (inInformat.IndexOf("yyy") == 0)
                    inInformat = inInformat.Replace("yyy", "yyyy");
                else if (inInformat == "yyMMdd")
                {
                    int iTry = 0;
                    iTry = twDate.ToAnyType<int>();
                    if (iTry < 701231 && iTry != 0)
                        twDate = iTry.ToString("01000000");
                    else
                        twDate = iTry.ToString("00000000");
                    inInformat = "yyyyMMdd";
                }
                else if (inInformat.IndexOf("yy") == 0)
                    inInformat = inInformat.Replace("yy", "yyyy");
                else if (inInformat.IndexOf("y") == 0)
                    inInformat = inInformat.Replace("y", "yyyy");
                //補 0
                twDate = twDate.PadLeft(inInformat.Length, '0');
            }
            
            CultureInfo twCulture = new CultureInfo("zh-TW");
            twCulture.DateTimeFormat.Calendar = DateFormatCalendar.TW;

            //讀入民國年轉西元
            if (DateTime.TryParseExact(twDate, inInformat, twCulture, DateTimeStyles.None, out tryDate))
                return tryDate;
            else
                return null; //轉換失敗
            
        }

        /// <summary>字串轉日期</summary>
        /// <param name="value">字串值</param>
        /// <param name="format">字串所顯示的日期格式 ex: yyyy/MM/dd HH:mm:ss</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value, string format = "yyyy/MM/dd")
        {
            if (string.IsNullOrEmpty(value)) return null;

            //特定狀況修正
            if (format.IndexOf('y') == 0)
            {
                if (format.IndexOf("yyyy") == 0)
                { } //不需補0
                else if (format.IndexOf("yy") == 0)
                    value = value.PadLeft(format.Length, '0');  //yy 補 0
            }

            DateTime dt = DateTime.Now;
            if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out dt))
                return dt;
            else
                return null;
            
        }

        #region 日期轉換擴充方法 ToDataFormat


        /// <summary>
        /// 將字串 轉日期 轉字串
        /// </summary>
        /// <param name="source">字串值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <param name="toformat">轉換格式</param>
        /// <returns></returns>
        public static string ToDataFormat<TEnum>(this string source, TEnum fmformat, TEnum toformat) where TEnum : struct, IConvertible
        {
            string sRet = source;
            if (sRet.Trim('0') == "")
            {
                string format = toformat.GetDateFormat();
                if (!string.IsNullOrEmpty(format))
                {
                    sRet = format;
                    sRet = sRet.Replace('y', '0');
                    sRet = sRet.Replace('M', '0');
                    sRet = sRet.Replace('d', '0');
                }
            }
            else
            {
                DateTime? dt = source.ToDateTime(fmformat);
                if (dt == null)
                    dt = source.ToDateTime(toformat);
                sRet = dt.ToDataFormat(toformat);
            }
            return sRet;
        }

        /// <summary>
        /// 將字串 轉日期 轉字串
        /// </summary>
        /// <param name="source">字串值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <param name="toformat">轉換格式</param>
        /// <returns></returns>
        public static string ToDataFormat<TEnum>(this string source, TEnum? fmformat, TEnum? toformat) where TEnum : struct, IConvertible
        {
            if (fmformat != null && toformat == null)
                return source.ToDataFormat(fmformat.Value, fmformat.Value);         //一個格式不存在就用另外一個格式
            else if (fmformat == null && toformat != null)
                return source.ToDataFormat(toformat.Value, toformat.Value);         //一個格式不存在就用另外一個格式
            else if (fmformat == null && toformat == null)
                return source;                                              //兩個格式不存就不轉換      
            else
                return source.ToDataFormat(fmformat.Value, toformat.Value);
        }

        /// <summary> DateTime? 轉 字串</summary>
        /// <param name="source">DateTime? 值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <returns></returns>
        public static string ToDataFormat<TEnum>(this DateTime? source, TEnum fmformat) where TEnum : struct, IConvertible
        {
            string sRet = string.Empty;
            if (source == null)
            {
                string format = fmformat.GetDateFormat();
                if (!string.IsNullOrEmpty(format))
                {
                    sRet = format;
                    sRet = sRet.Replace('y', '0');
                    sRet = sRet.Replace('M', '0');
                    sRet = sRet.Replace('d', '0');
                }
            }
            else
            {
                sRet = source.Value.ToDataFormat(fmformat);
            }
            return sRet;
        }
        /// <summary> DateTime 轉 字串</summary>
        /// <param name="source">DateTime? 值</param>
        /// <param name="fmformat">轉換格式</param>
        /// <returns></returns>
        public static string ToDataFormat<TEnum>(this DateTime source, TEnum fmformat) where TEnum : struct, IConvertible
        {
            string sRet = "";
            FieldInfo fi = fmformat.GetType().GetField(fmformat.ToString());
            DateFormatAttribute[] attributes = null;
            if (fi != null)
            {
                attributes =
                    (DateFormatAttribute[])fi.GetCustomAttributes(typeof(DateFormatAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    string format = attributes[0].Format;
                    switch (attributes[0].Type)
                    {
                        case DateEnums.Year.None:
                            break;
                        case DateEnums.Year.TW:
                            sRet = source.ToTWDateString(format);
                            break;
                        case DateEnums.Year.CE:
                            sRet = source.ToString(format);
                            break;
                        default:
                            break;
                    }
                }
            }
            return sRet;
        }
        #endregion

    }
}
