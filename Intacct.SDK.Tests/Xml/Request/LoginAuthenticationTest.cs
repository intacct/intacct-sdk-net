/*
 * Copyright 2020 Sage Intacct, Inc.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * or in the "LICENSE" file accompanying this file. This file is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
 * express or implied. See the License for the specific language governing 
 * permissions and limitations under the License.
 */

using System;
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