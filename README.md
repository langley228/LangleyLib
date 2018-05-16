# LangleyLib


<H3>LangleyLib.Extensions.dll</H3>
<H4>Sample code</H4>


```
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
    
```
    
<H3>LangleyLib.VB6Extensions.dll</H3>
<H4>Sample code</H4>

```
    using LangleyLib.VB6Extensions;
    "A你B我C他".VBMid(4) => "我C他"
    "A你B我C他".VBMid(2, 4) => "你B我C"
    "A你B我C他".VBLeft(4) => "A你B我"
    "A你B我C他".VBInStr(4, "他") => 6
    "A".VBAsc() => 65
    65.VBChr() => 'A'
    1.VBSpace() => " "
```

# LyAdLib.dll.config 設定範例
```
   <add Key="LDAP" AdType="LDAP" Path="LDAP://127.0.0.1:389/DC=domain,DC=com" 
   Domain="domain" UserName="xxx" Password="xxx" >
```
   
# Model 範例
```
    public class LDAPUser
    {
        [Property(AdType.LDAP, "SamAccountname")]
        public string UserName { get; set; }
        [Property(AdType.LDAP, "Attribute_1")]
        public string EmployeId { get; set; }
        [Property(AdType.LDAP, "mail")]
        public string Email { get; set; }
    }
```
    
       
# Service 範例
```
    public class LDAPADService : AdServiceBase
    {
        public LDAPADService(string settingkey)
            : base(settingkey)
        {
        }
        private IAdSet<LDAPUser> m_LDAPUser;
        public IAdSet<LDAPUser> LDAPUser
        {
            get { return m_LDAPUser ?? (m_LDAPUser = CreateAdSet<LDAPUser>("(objectClass=user)(objectCategory=person)")); }
        }
    }
```    

# 使用 範例
```
            using (var ad = new LDAPADService("LDAP"))
            {
                string find = "xxx";
                Console.WriteLine("Find LDAP EmployeId : {0}", find);
                var uesrs = ad.LDAPUser.FindAll("EmployeId", find);
                if (uesrs != null && uesrs.Count > 0)
                {
                    foreach (var item in uesrs)
                    {
                        Console.WriteLine("UserName : {0}", item.UserName);
                        Console.WriteLine("EmployeId : {0}", item.EmployeId);
                        Console.WriteLine("Email : {0}", item.Email);
                    }
                }

            }
```
