# LangleyLib

<H3>dll Release<H3>

```
LangleyLib.Extensions.dll
LangleyLib.DateFormat.dll
LangleyLib.VB6Extensions.dll
LangleyLib.AdLib.dll
LangleyLib.MailServerLib.dll
LangleyLib.AopLib.dll
```


<H3>LangleyLib.Extensions.dll</H3>
<H4>using namespace</H4>

```
    using LangleyLib.Extensions;
```

<H4>Sample</H4>

```
    "a".ToWchr() => "ａ"
    "ａ".ToNchr() => "a"
    "!EWR$$!%我".ToEncodeBase64() => "IUVXUiQkISXmiJE="
    "Hello world".ToHexEncrypt() => "48656C6C6F20776F726C64"
```


<H3>LangleyLib.DateFormat.dll</H3>
<H4>using namespace</H4>

```
    using LangleyLib.DateFormat;
    using LangleyLib.DateFormat.Attributes;
    using LangleyLib.DateFormat.Enums;
```    
    
<P><B>Custom enum</B></p>

```
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
  
<H4>Sample</H4>  
    
```
    "191017".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYYMMDD) => "1061019"
    "290200".ToDataFormat<DataFormat>(DataFormat.CE_DDMMYY, DataFormat.TW_YYMMDD) => "890229"
```
    
<H3>LangleyLib.VB6Extensions.dll</H3>
<H4>using namespace</H4>

```
    using LangleyLib.VB6Extensions;
```    

<H4>Sample</H4>

```
    "A你B我C他".VBMid(4) => "我C他"
    "A你B我C他".VBMid(2, 4) => "你B我C"
    "A你B我C他".VBLeft(4) => "A你B我"
    "A你B我C他".VBInStr(4, "他") => 6
    "A".VBAsc() => 65
    65.VBChr() => 'A'
    1.VBSpace() => " "
```

<H3>LangleyLib.AdLib.dll</H3>
<H4>LangleyLib.AdLib.dll.config Sample </H4>

```
   <add Key="LDAP" AdType="LDAP" Path="LDAP://127.0.0.1:389/DC=domain,DC=com" 
   Domain="domain" UserName="xxx" Password="xxx" >
```

<H4>using namespace</H4>
   
```
    using LangleyLib.AdLib;
```    

<H4>Custom Model</H4>

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

<H4>Custom Service</H4> 

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

<H4>Sample</H4> 

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


<H3>LangleyLib.MailServerLib.dll</H3>
<H4>LangleyLib.MailServerLib.dll.config Sample </H4>

```
      <add Key="Smtp" DisplayName="Smtp" Address="Smtp@Smtp.com" UserName="Smtp"
         Password="" Host="127.0.0.1" Port="25" EnableSsl="false" />
```

<H4>using namespace</H4>
   
```
    using LangleyLib.MailServerLib;
```    

<H4>Sample</H4> 

```

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


<H3>LangleyLib.AopLib.dll</H3>
<H4>using namespace</H4>
   
```
    using LangleyLib.AopLib;
```   

<H4>LogFilterAttributeg Sample </H4>

```
    public class LogFilterAttribute : AopFilterAttribute
    {
        public override void OnMethodException(string className, string methodName, Exception ex)
        {
            .................
        }
        public override void OnMethodAfter(string className, string methodName, object[] outArgs, object returnValue)
        {        
            .................
        }
        public override void OnMethodBefore(string className, string methodName, object[] inArgs)
        {
            .................
        }
    }
```


<H4>LogSample Sample </H4>

```
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
    
    .......    
            string refS="ref Input";
            string outS;
            LogSample logsample = new LogSample();
            logsample.Sample("Input", ref refS, out outS);
```
 

<H4>WorkFlowSample Sample</H4> 

```
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
        [FlowNext("Flow2")]
        public void Flow1()
        {
            Console.WriteLine("Run LogSample.Flow1");
        }
        [FlowNext("Flow3")]
        public void Flow2()
        {
            Console.WriteLine("Run LogSample.Flow2");
        }
        [FlowNext("Flow4")]
        public void Flow3()
        {
            Console.WriteLine("Run LogSample.Flow3");
            throw new Exception("Try Exception");
        }
        [FlowNext("FlowEnd")]
        public void Flow4()
        {
            Console.WriteLine("Run LogSample.Flow4");
        }
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

            .............
            var o = new WorkFlows.Controllers.WorkFlowSample();
            Thread s = new Thread(o.Start);
            s.Start();
```

