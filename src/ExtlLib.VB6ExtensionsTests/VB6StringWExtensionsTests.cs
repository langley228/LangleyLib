using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtlLib.VB6Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtlLib.VB6Extensions.Tests
{
    [TestClass()]
    public class VB6StringWExtensionsTests
    {
        [TestMethod()]
        public void VBMidMbcsTest()
        {
            if ("A你B我C他".VBMidMbcs(1, 4) != "A你B")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBMidMbcsTest1()
        {
            if ("A你B我C他".VBMidMbcs(5, 4, Encoding.UTF8) != "B我")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLenMbcsTest()
        {
            if ("A你B我C他".VBLenMbcs() != 9)
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLenMbcsTest1()
        {
            if ("A你B我C他".VBLenMbcs(Encoding.UTF8) != 12)
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLeftMbcsTest()
        {
            if ("A你B我C他".VBLeftMbcs(4) != "A你B")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLeftMbcsTest1()
        {
            if ("A你B我C他".VBLeftMbcs(4, Encoding.UTF8) != "A你")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBRightMbcsTest()
        {
            if ("A你B我C他".VBRightMbcs(5) != "我C他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBRightMbcsTest1()
        {
            if ("A你B我C他".VBRightMbcs(4, Encoding.UTF8) != "C他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBInsertReplaceMbcsTest()
        {
            if ("A你B我C他".VBInsertReplaceMbcs(4, "Z他Y") != "A你Z他Y他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBInsertReplaceMbcsTest1()
        {
            if ("A你B我C他".VBInsertReplaceMbcs(4, 3, "Z他Y") != "A你Z他YC他")
                Assert.Fail();
        }
    }
}