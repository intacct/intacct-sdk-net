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
    public class LoginAuthenticationTest : XmlObjectTestHelper
    {
        [Fact]
        public void WriteXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <login>
        <userid>testuser</userid>
        <companyid>testcompany</companyid>
        <password>testpass</password>
    </login>
</authentication>";

            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "testcompany", "testpass");
            
            this.CompareXml(expected, loginAuth);
        }
        
        [Fact]
        public void WriteXmlWithEntityTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <login>
        <userid>testuser</userid>
        <companyid>testcompany</companyid>
        <password>testpass</password>
        <locationid>testentity</locationid>
    </login>
</authentication>";

            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "testcompany", "testpass", "testentity");
            
            this.CompareXml(expected, loginAuth);
        }
        
        [Fact]
        public void WriteXmlWithEmptyEntityTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <login>
        <userid>testuser</userid>
        <companyid>testcompany</companyid>
        <password>testpass</password>
        <locationid />
    </login>
</authentication>";

            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "testcompany", "testpass", "");
            
            this.CompareXml(expected, loginAuth);
        }

        [Fact]
        public void InvalidCompanyIdTest()
        {
            var ex = Record.Exception(() => new LoginAuthentication("testuser", "", "testpass"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Company ID is required and cannot be blank", ex.Message);
        }

        [Fact]
        public void InvalidUserIdTest()
        {
            var ex = Record.Exception(() => new LoginAuthentication("", "testcompany", "testpass"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("User ID is required and cannot be blank", ex.Message);
        }

        [Fact]
        public void InvalidUserPasswordTest()
        {
            var ex = Record.Exception(() => new LoginAuthentication("testuser", "testcompany", ""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("User Password is required and cannot be blank", ex.Message);
        }
    }
}