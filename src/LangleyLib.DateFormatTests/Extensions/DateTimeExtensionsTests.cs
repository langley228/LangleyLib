using Microsoft.VisualStudio.TestTools.UnitTesting;
using LangleyLib.DateFormat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.DateFormat.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void ToTWDateStringTest()
        {
            if ("1061016171819" != new DateTime(2017, 10, 16, 17, 18, 19).ToTWDateString("yyyMMddHHmmss"))
                Assert.Fail();
            if ("1061016171819" != new DateTime(2017, 10, 16, 17, 18, 19).ToTWDateString("yyMMddHHmmss"))
                Assert.Fail();
            if ("1010229171819" != new DateTime(2012, 2, 29, 17, 18, 19).ToTWDateString("yyMMddHHmmss"))
                Assert.Fail();
            if ("890229" != new DateTime(2000, 2, 29).ToTWDateString("yyMMdd"))
                Assert.Fail();
            if ("022989" != new DateTime(2000, 2, 29).ToTWDateString("MMddyy"))
                Assert.Fail();
            if ("0228" != new DateTime(2017, 2, 28).ToTWDateString("MMdd"))
                Assert.Fail();
        }
        
    }
}