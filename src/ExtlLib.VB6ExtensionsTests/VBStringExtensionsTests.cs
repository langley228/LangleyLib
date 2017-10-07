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
    public class VBStringExtensionsTests
    {

        [TestMethod()]
        public void VBMidTest()
        {
            if ("A你B我C他".VBMid(4) != "我C他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBMidTest1()
        {
            if ("A你B我C他".VBMid(2, 4) != "你B我C")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLeftTest()
        {
            if ("A你B我C他".VBLeft(4) != "A你B我")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBRightTest()
        {
            if ("A你B我C他".VBRight(4) != "B我C他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBInStrTest()
        {
            if ("A你B我C他".VBInStr(4, "他") != 6)
                Assert.Fail();
            if ("A你B我C他".VBInStr(4, "x") != 0)
                Assert.Fail();
        }

        [TestMethod()]
        public void VBLenBTest()
        {
            if ("A你B我C他".VBLenB() != 9)
                Assert.Fail();
            if ("ABC".VBLenB() != 3)
                Assert.Fail();
        }

        [TestMethod()]
        public void VBAscTest()
        {
            if ("A".VBAsc() != 65)
                Assert.Fail();
        }


        [TestMethod()]
        public void VBInsertTest()
        {
            if ("A你B我C他".VBInsert(4, "Z他Y") != "A你BZ他Y我C他")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBInsertReplaceTest()
        {
            if ("A你B我C他".VBInsertReplace(4, "Z他Y") != "A你BZ他Y")
                Assert.Fail();
        }

        [TestMethod()]
        public void VBInsertReplaceTest1()
        {
            if ("A你B我C他".VBInsertReplace(4, 2, "Z他Y") != "A你BZ他Y他")
                Assert.Fail();
        }
    }
}