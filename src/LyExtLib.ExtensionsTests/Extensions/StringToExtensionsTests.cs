using Microsoft.VisualStudio.TestTools.UnitTesting;
using LyExtLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.Extensions.Tests
{
    [TestClass()]
    public class StringToExtensionsTests
    {
        [TestMethod()]
        public void ToWchrTest()
        {
            string[] s = { "a", "b", "c" };
            string[] m = { "ａ", "ｂ", "ｃ" };
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToWchr() != m[i])
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void ToNchrTest()
        {
            string[] s = { "a", "b", "c" };
            string[] m = { "ａ", "ｂ", "ｃ" };
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != m[i].ToNchr())
                    Assert.Fail();
            }
        }
    }
}