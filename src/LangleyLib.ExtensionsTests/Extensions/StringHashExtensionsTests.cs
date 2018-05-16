using Microsoft.VisualStudio.TestTools.UnitTesting;
using LangleyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions.Tests
{
    [TestClass()]
    public class StringHashExtensionsTests
    {
        [TestMethod()]
        public void ToEncodeBase64Test()
        {
            if ("!EWR$$!%我".ToEncodeBase64() != "IUVXUiQkISXmiJE=")
                Assert.Fail();
        }

        [TestMethod()]
        public void ToEncodeBase64Test1()
        {
            if ("!EWR$$!%".ToEncodeBase64(Encoding.ASCII) != "IUVXUiQkISU=")
                Assert.Fail();
        }

        [TestMethod()]
        public void ToDecodeBase64Test()
        {
            if ("!EWR$$!%我" != "IUVXUiQkISXmiJE=".ToDecodeBase64())
                Assert.Fail();
        }

        [TestMethod()]
        public void ToDecodeBase64Test1()
        {
            if ("!EWR$$!%" != "IUVXUiQkISU=".ToDecodeBase64(Encoding.ASCII))
                Assert.Fail();
        }

        [TestMethod()]
        public void ToHexEncryptTest()
        {
            if ("Hello world".ToHexEncrypt() != "48656C6C6F20776F726C64")
                Assert.Fail();
        }

        [TestMethod()]
        public void ToHexDecryptTest()
        {
            if ("Hello world" != "48656C6C6F20776F726C64".ToHexDecrypt())
                Assert.Fail();
        }

        [TestMethod()]
        public void ToMd5HashTest()
        {
            if ("Hello world".ToMd5Hash() != "3e25960a79dbc69b674cd4ec67a72c62".ToUpper())
                Assert.Fail();
        }

        [TestMethod()]
        public void ToMd5HashTest1()
        {
            if ("Hello world".ToMd5Hash(Encoding.UTF8) != "3e25960a79dbc69b674cd4ec67a72c62".ToUpper())
                Assert.Fail();
        }

        [TestMethod()]
        public void VerifyMd5HashTest()
        {
            if (!"Hello world".VerifyMd5Hash("3e25960a79dbc69b674cd4ec67a72c62"))
                Assert.Fail();
        }

        [TestMethod()]
        public void VerifyMd5HashTest1()
        {
            if (!"Hello world".VerifyMd5Hash("3e25960a79dbc69b674cd4ec67a72c62", Encoding.UTF8))
                Assert.Fail();
        }
    }
}