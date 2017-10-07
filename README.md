# ExtlLib

<H3>ExtlLib.VB6Extensions.dll</H3>

<H4>Sample code</H4>
<p>using ExtlLib.VB6Extensions;</p>

<p>"A你B我C他".VBMid(4) => "我C他"</p>

<p>"A你B我C他".VBMid(2, 4) => "你B我C"</p>

<p>"A你B我C他".VBLeft(4) => "A你B我"</p>

<p>"A你B我C他".VBInStr(4, "他") => 6</p>

<p>"A".VBAsc() => 65</p>

<p>65.VBChr() => 'A'</p>

<p>1.VBSpace() => " "</p>

<H3>ExtlLib.DateFormat.dll</H3>

<H4>Sample code</H4>
<p>using ExtlLib.DateFormat;</p>
<p>using ExtlLib.DateFormat.Attributes;</p>
<p>using ExtlLib.DateFormat.Enums;</p>

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
    }
