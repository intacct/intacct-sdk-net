using System;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Tests.Functions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Request;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Request
{
    public class SessionAuthenticationTest : XmlObjectTestHelper
    {
        [Fact]
        public void WriteXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <sessionid>testsessionid..</sessionid>
</authentication>";

            SessionAuthentication sessionAuth = new SessionAuthentication("testsessionid..");
            
            this.CompareXml(expected, sessionAuth);
        }

        [Fact]
        public void InvalidSessionIdTest()
        {
            var ex = Record.Exception(() => new SessionAuthentication(""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Session ID is required and cannot be blank", ex.Message);
        }
    }
}