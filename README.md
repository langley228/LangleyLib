# LangleyLib


<H3>LangleyLib.Extensions.dll</H3>
<H4>Sample code</H4>


    using LangleyLib.Extensions;
    "a".ToWchr() => "ａ"
    "ａ".ToNchr() => "a"
    "!EWR$$!%我".ToEncodeBase64() => "IUVXUiQkISXmiJE="
    "Hello world".ToHexEncrypt() => "48656C6C6F20776F726C64"



<H3>LangleyLib.DateFormat.dll</H3>
<H4>Sample code</H4>


    using LangleyLib.DateFormat;
    using LangleyLib.DateFormat.Attributes;
    using LangleyLib.DateFormat.Enums;
    
    
<P><B>Create enum</B></p>

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
    
    
    "191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYMMDD) => "1061019"
    "290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYMMDD) => "890229"
    
    
<H3>LangleyLib.VB6Extensions.dll</H3>
<H4>Sample code</H4>

    using LangleyLib.VB6Extensions;
    "A你B我C他".VBMid(4) => "我C他"
    "A你B我C他".VBMid(2, 4) => "你B我C"
    "A你B我C他".VBLeft(4) => "A你B我"
    "A你B我C他".VBInStr(4, "他") => 6
    "A".VBAsc() => 65
    65.VBChr() => 'A'
    1.VBSpace() => " "

