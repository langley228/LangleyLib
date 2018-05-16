using LangleyLib.DateFormat.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LangleyLib.Extensions;
using LangleyLib.Extensions.Attributes;
using LangleyLib.DateFormat.Attributes;
using LangleyLib.DateFormat.Enums;

namespace LangleyLib.DateFormat.Extensions.Tests
{
    /// <summary>日期格式</summary>
    public enum DataFormat
    {
        /// <summary>民國曆(YYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYMMDD)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMMdd")]
        TW_YYMMDD = 1,
        /// <summary>民國曆(YYYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYYMMDD)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyMMdd")]
        TW_YYYMMDD = 2,
        /// <summary>西元曆(YYMMDD)</summary>
        [System.ComponentModel.Description("西元曆(YYMMDD)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyMMdd")]
        CE_YYMMDD = 3,
        /// <summary>西元曆(YYYYMMDD)</summary>
        [System.ComponentModel.Description("西元曆(YYYYMMDD)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyyyMMdd")]
        CE_YYYYMMDD = 4,
        /// <summary>西元曆(YYMM)</summary>
        [System.ComponentModel.Description("西元曆(YYMM)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyMM")]
        CE_YYMM = 5,
        /// <summary>民國曆(YYMM)</summary>
        [System.ComponentModel.Description("民國曆(YYMM)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMM")]
        TW_YYMM = 6,
        /// <summary>民國曆(YYYMM)</summary>
        [System.ComponentModel.Description("民國曆(YYYMM)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyMM")]
        TW_YYYMM = 7,
        /// <summary>民國曆(YYYYMMDD)</summary>
        [System.ComponentModel.Description("民國曆(YYYYMMDD)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyyMMdd")]
        TW_YYYYMMDD = 8,

        /// <summary>西元曆(DDMMYY)</summary>
        [System.ComponentModel.Description("西元曆(DDMMYY)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "ddMMyy")]
        CE_DDMMYY = 9,

        /// <summary>民國曆(MMYYY)</summary>
        [System.ComponentModel.Description("民國曆(MMYYY)")]
        [DateFormat(Type = DateEnums.Year.TW, Format = "MMyyy")]
        TW_MMYYY = 101,

        /// <summary>西元曆(MMYY)</summary>
        [System.ComponentModel.Description("西元曆(MMYY)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "MMyy")]
        CE_MMYY = 102,

        /// <summary>西元曆(YYYYMM)</summary>
        [System.ComponentModel.Description("西元曆(YYYYMM)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyyyMM")]
        CE_YYYYMM = 103,

        /// <summary>西元曆(MMYYYY)</summary>
        [System.ComponentModel.Description("西元曆(MMYYYY)")]
        [DateFormat(Type = DateEnums.Year.CE, Format = "MMyyyy")]
        CE_MMYYYY = 104,
        /// <summary>民國曆(YYMMDDHHS)</summary>
        [System.ComponentModel.Description("民國曆(YYMMDD)")]
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
            foreach (var dt in dtList)
            {
                foreach (var iE1 in Enum.GetValues(typeof(DataFormat)))
                {
                    DataFormat fmSet = (DataFormat)iE1;
                    string s = dt.ToDataFormat(fmSet);

                    foreach (var iE2 in Enum.GetValues(typeof(DataFormat)))
                    {
                        DataFormat toSet = (DataFormat)iE2;
                        string dss = s.ToDataFormat(fmSet, toSet);
                        DateTime? ds = dss.ToDateTime(toSet);
                        bool isPass = false;
                        if (ds == null || (ds.Value != dt && ds.Value.Day != 1))
                            isPass = false;
                        else
                            isPass = true;
                        if (isPass)
                            iPass++;
                        iCount++;
                    }
                }
                if (iPass != iCount)
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void ToTWDateTimeTest()
        {
            if ("1061016171819".ToTWDateTime("yyyMMddHHmmss") != new DateTime(2017, 10, 16, 17, 18, 19))
                Assert.Fail();
            if ("891016171819".ToTWDateTime("yyyMMddHHmmss") != new DateTime(2000, 10, 16, 17, 18, 19))
                Assert.Fail();
            if ("890229".ToTWDateTime("yyMMdd") != new DateTime(2000, 2, 29))
                Assert.Fail();
            if ("022989".ToTWDateTime("MMddyy") != new DateTime(2000, 2, 29))
                Assert.Fail();
            if ("1016106".ToTWDateTime("MMddyyy") != new DateTime(2017, 10, 16))
                Assert.Fail();
        }

        [TestMethod()]
        public void ToDateTimeTest()
        {
            if ("20171016171819".ToDateTime("yyyyMMddHHmmss") != new DateTime(2017, 10, 16, 17, 18, 19))
                Assert.Fail();
            if ("001016171819".ToDateTime("yyMMddHHmmss") != new DateTime(2000, 10, 16, 17, 18, 19))
                Assert.Fail();
            if ("001016".ToDateTime("yyMMdd") != new DateTime(2000, 10, 16))
                Assert.Fail();
            if ("101600".ToDateTime("MMddyy") != new DateTime(2000, 10, 16))
                Assert.Fail();
            if ("101617".ToDateTime("MMddyy") != new DateTime(2017, 10, 16))
                Assert.Fail();
        }

        [TestMethod()]
        public void ToDataFormatTest1()
        {
            if ("191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY,DataFormat.TW_YYYYMMDD) != "01061019")
                Assert.Fail();
            if ("290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYYMMDD) != "00890229")
                Assert.Fail();

            if ("191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYMMDD) != "1061019")
                Assert.Fail();
            if ("290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYMMDD) != "890229")
                Assert.Fail();
        }
    }
}