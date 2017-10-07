# LyExtLib


<H3>LyExtLib.Extensions.dll</H3>
<H4>Sample code</H4>
<p>"a".ToWchr() => "ａ"</p>
<p>"ａ".ToNchr() => "a"</p>
<p>"!EWR$$!%我".ToEncodeBase64() => "IUVXUiQkISXmiJE="</p>
<p>"Hello world".ToHexEncrypt() => "48656C6C6F20776F726C64"</p>

<H3>LyExtLib.DateFormat.dll</H3>
<H4>Sample code</H4>
<p>using LyExtLib.DateFormat;</p>
<p>using LyExtLib.DateFormat.Attributes;</p>
<p>using LyExtLib.DateFormat.Enums;</p>
<p><B>Create enum</B></p>

    public enum DataFormat
    {
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyMMdd")]
        TW_YYMMDD = 1,
        [DateFormat(Type = DateEnums.Year.TW, Format = "yyyMMdd")]
        TW_YYYMMDD = 2,
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyMMdd")]
        CE_YYMMDD = 3,
        [DateFormat(Type = DateEnums.Year.CE, Format = "yyyyMMdd")]
        CE_YYYYMMDD = 4,
    }
    
<p>"191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYMMDD) => "1061019"</p>
<p>"290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYMMDD) => "890229"</p>
    
    
<H3>LyExtLib.VB6Extensions.dll</H3>
<H4>Sample code</H4>
<p>using LyExtLib.VB6Extensions;</p>
<p>"A你B我C他".VBMid(4) => "我C他"</p>
<p>"A你B我C他".VBMid(2, 4) => "你B我C"</p>
<p>"A你B我C他".VBLeft(4) => "A你B我"</p>
<p>"A你B我C他".VBInStr(4, "他") => 6</p>
<p>"A".VBAsc() => 65</p>
<p>65.VBChr() => 'A'</p>
<p>1.VBSpace() => " "</p>

