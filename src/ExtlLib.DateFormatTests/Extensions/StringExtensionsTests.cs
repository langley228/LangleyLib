using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ExtlLib.Extensions;
using ExtlLib.Extensions.Attributes;
using ExtlLib.DateFormat.Attributes;
using ExtlLib.DateFormat.Enums;

namespace ExtlLib.DateFormat.Extensions.Tests
{
    /// <summary>日期格式</summary>
    public enum DataFormat
    {
        /// <summary>民國曆(YYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYMMDD)")]
        [Mapping("1")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMMdd")]
        TW_YYMMDD = 1,
        /// <summary>民國曆(YYYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYYMMDD)")]
        [Mapping("2")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyMMdd")]
        TW_YYYMMDD = 2,
        /// <summary>西元曆(YYMMDD)</summary>
        [System.ComponentModel.Description("西元曆(YYMMDD)")]
        [Mapping("3")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyMMdd")]
        CE_YYMMDD = 3,
        /// <summary>西元曆(YYYYMMDD)</summary>
        [System.ComponentModel.Description("西元曆(YYYYMMDD)")]
        [Mapping("4")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyyyMMdd")]
        CE_YYYYMMDD = 4,
        /// <summary>西元曆(YYMM)</summary>
        [System.ComponentModel.Description("西元曆(YYMM)")]
        [Mapping("5")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyMM")]
        CE_YYMM = 5,
        /// <summary>民國曆(YYMM)</summary>
        [System.ComponentModel.Description("民國曆(YYMM)")]
        [Mapping("6")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMM")]
        TW_YYMM = 6,
        /// <summary>民國曆(YYYMM)</summary>
        [System.ComponentModel.Description("民國曆(YYYMM)")]
        [Mapping("7")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyMM")]
        TW_YYYMM = 7,
        /// <summary>民國曆(YYYYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYYYMMDD)")]
        [Mapping("8")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyyMMdd")]
        TW_YYYYMMDD = 8,

        /// <summary>西元曆(DDMMYY)</summary>
        [System.ComponentModel.Description("西元曆(DDMMYY)")]
        [Mapping("9")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "ddMMyy")]
        CE_DDMMYY = 9,

        /// <summary>民國曆(MMYYY)</summary>
        [System.ComponentModel.Description("民國曆(MMYYY)")]
        [Mapping("A")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "MMyyy")]
        TW_MMYYY = 101,

        /// <summary>西元曆(MMYY)</summary>
        [System.ComponentModel.Description("西元曆(MMYY)")]
        [Mapping("B")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "MMyy")]
        CE_MMYY = 102,

        /// <summary>西元曆(YYYYMM)</summary>
        [System.ComponentModel.Description("西元曆(YYYYMM)")]
        [Mapping("C")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyyyMM")]
        CE_YYYYMM = 103,

        /// <summary>西元曆(MMYYYY)</summary>
        [System.ComponentModel.Description("西元曆(MMYYYY)")]
        [Mapping("D")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "MMyyyy")]
        CE_MMYYYY = 104,
        /// <summary>民國曆(YYMMDDHHS)</summary>
        [System.ComponentModel.Description("民國曆(YYMMDD)")]
        [Mapping("999")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMMddHHmmss")]
        TW_YYMMDDHHmmss = 999,
    }
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ToDataFormatTest()
        {
            List<DateTime> dtList = new List<DateTime>();
            for (int i = -3; i < 2; i++)
            {
                dtList.Add(new DateTime(2008 + i * 4, 2, 29));
            }
            int iCount = 0;
            int iPass = 0;
            string msgErr = "";
            foreach (var dt in dtList)
            {
                foreach (var iE1 in Enum.GetValues(typeof(DataFormat)))
                {
                    DataFormat fmSet = (DataFormat)iE1;
                    string s = dt.ToDataFormat(fmSet);
                    string x = dt.ToString("yyyymmdd");

                    foreach (var iE2 in Enum.GetValues(typeof(DataFormat)))
                    {
                        DataFormat toSet = (DataFormat)iE2;
                        string dss = s.ToDataFormat(fmSet, toSet);
                        DateTime? ds = dss.ToDateTime(toSet);
                        bool isPass = false;
                        string msgOne = $"source {fmSet.MappingValue()} format:{fmSet.ToString()} value:{s} to {toSet.MappingValue()} format:{toSet.ToString()} value:{dss}";
                        if (ds == null || (ds.Value != dt && ds.Value.Day != 1))
                            isPass = false;
                        else
                            isPass = true;
                        //Debug.WriteLine($"{(isPass ? "OK " : "Err")}  source {fmSet.MappingValue()} format:{fmSet.ToString()} value:{s} to {toSet.MappingValue()} format:{toSet.ToString()} value:{dss}");
                        if (!isPass)
                            msgErr += $"{msgOne}\r\n";
                        else
                            iPass++;
                        iCount++;
                    }
                }
                if (iPass != iCount)
                    Assert.Fail();
            }
        }
    }
}