using Microsoft.VisualStudio.TestTools.UnitTesting;
using LyExtLib.VB6Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.VB6Extensions.Tests
{
    [TestClass()]
    public class VBIntExtensionsTests
    {
        [TestMethod()]
        public void VBChrTest()
        {
            if (65.VBChr() != 'A')
                Assert.Fail();
        }

        [TestMethod()]
        public void VBSpaceTest()
        {
            if (5.VBSpace() != "     ")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBZeroTest()
        {
            if (5.VBZero() != "00000")
                Assert.Fail();
        }
    }
}