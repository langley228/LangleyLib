# LangleyLib

## 目錄
- [dll Release](#dll-release)
- [LangleyLib.Extensions.dll](#langleylibextensionsdll)
- [LangleyLib.DateFormat.dll](#langleylibdateformatdll)
- [LangleyLib.VB6Extensions.dll](#langleylibvb6extensionsdll)
- [LangleyLib.AdLib.dll](#langleylibadlibdll)
- [LangleyLib.MailServerLib.dll](#langleylibmailserverlibdll)
- [LangleyLib.AopLib.dll](#langleylibaoplibdll)

---

## dll Release

```
LangleyLib.Extensions.dll
LangleyLib.DateFormat.dll
LangleyLib.VB6Extensions.dll
LangleyLib.AdLib.dll
LangleyLib.MailServerLib.dll
LangleyLib.AopLib.dll
```

---

## LangleyLib.Extensions.dll

### using namespace

```csharp
using LangleyLib.Extensions;
```

### Sample

```csharp
"a".ToWchr() // => "ａ"
"ａ".ToNchr() // => "a"
"!EWR$$!%我".ToEncodeBase64() // => "IUVXUiQkISXmiJE="
"Hello world".ToHexEncrypt() // => "48656C6C6F20776F726C64"
```

---

## LangleyLib.DateFormat.dll

### using namespace

```csharp
using LangleyLib.DateFormat;
using LangleyLib.DateFormat.Attributes;
using LangleyLib.DateFormat.Enums;
```

**Custom enum**

```csharp
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
```

### Sample

```csharp
"191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYMMDD) // => "1061019"
"290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYMMDD) // => "890229"
```

---

## LangleyLib.VB6Extensions.dll

### using namespace

```csharp
using LangleyLib.VB6Extensions;
```

### Sample

```csharp
"A你B我C他".VBMid(4) // => "我C他"
"A你B我C他".VBMid(2, 4) // => "你B我C"
"A你B我C他".VBLeft(4) // => "A你B我"
"A你B我C他".VBInStr(4, "他") // => 6
"A".VBAsc() // => 65
65.VBChr() // => 'A'
1.VBSpace() // => " "
```

---

## LangleyLib.AdLib.dll

### LangleyLib.AdLib.dll.config 範例

```xml
<add Key="LDAP" AdType="LDAP" Path="LDAP://127.0.0.1:389/DC=domain,DC=com" 
 Domain="domain" UserName="xxx" Password="xxx" >
```

### using namespace

```csharp
using LangleyLib.AdLib;
```

### Custom Model

```csharp
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

### Custom Service

```csharp
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

### Sample

```csharp
using (var ad = new LDAPADService("LDAP"))
{
    string find = "xxx";
    Console.WriteLine("Find LDAP EmployeId : {0}", find);
    var users = ad.LDAPUser.FindAll("EmployeId", find);
    if (users != null && users.Count > 0)
    {
        foreach (var item in users)
        {
            Console.WriteLine("UserName : {0}", item.UserName);
            Console.WriteLine("EmployeId : {0}", item.EmployeId);
            Console.WriteLine("Email : {0}", item.Email);
        }
    }
}
```

---

## LangleyLib.MailServerLib.dll

### LangleyLib.MailServerLib.dll.config 範例

```xml
<add Key="Smtp" DisplayName="Smtp" Address="Smtp@Smtp.com" UserName="Smtp"
 Password="" Host="127.0.0.1" Port="25" EnableSsl="false" />
```

### using namespace

```csharp
using LangleyLib.MailServerLib;
```

### Sample

```csharp
SmtpSender send = null;
using (send = new SmtpSender("Smtp"))
{
    string tomail = "yourmail@mailserver.com";
    send.To.Add(tomail, "displayName");
    send.Subject = "LangleyLib.MailServerLib Test";
    send.Body = "LangleyLib.MailServerLib Test";
    send.Send();
}
```

---

## LangleyLib.AopLib.dll

### using namespace

```csharp
using LangleyLib.AopLib;
```

### LogFilterAttribute 範例

```csharp
public class LogFilterAttribute : AopFilterAttribute
{
    public override void OnMethodException(string className, string methodName, Exception ex)
    {
        // ...
    }
    public override void OnMethodAfter(string className, string methodName, object[] outArgs, object returnValue)
    {        
        // ...
    }
    public override void OnMethodBefore(string className, string methodName, object[] inArgs)
    {
        // ...
    }
}
```

### LogSample 範例

```csharp
[LogFilterAttribute]
public class LogSample : AopContext
{
    public string Sample(string strIn, ref string refS, out string outS)
    {
        refS = "ref Output";
        outS = "out Output";
        Console.WriteLine("Run LogSample.Sample");
        return "This is Sample";
    }
}

// 使用範例
string refS = "ref Input";
string outS;
LogSample logsample = new LogSample();
logsample.Sample("Input", ref refS, out outS);
```

### WorkFlowSample 範例

```csharp
public class WorkFlowSample : FlowController<Routes.RouteSample>
{
}

[LogFilter]
[FlowException("FlowException")]
[FlowStart("Start")]
public class RouteSample : FlowRoute
{
    [FlowNext("Flow1")]
    public void Start()
    {
        Console.WriteLine("Run LogSample.Start");
    }
    // ...
    [FlowNext("Flow4")]
    public void Flow3()
    {
        Console.WriteLine("Run LogSample.Flow3");
        throw new Exception("Try Exception");
    }
    // ...
    [FlowEnd]
    public void FlowEnd()
    {
        Console.WriteLine("Run LogSample.FlowEnd");
    }

    [FlowNext("FlowEnd")]
    public void FlowException()
    {
        Console.WriteLine("Run ExceptionFlow");
    }
}

// 啟動流程
var o = new WorkFlows.Controllers.WorkFlowSample();
Thread s = new Thread(o.Start);
s.Start();
```

