/*
 * Copyright 2017 Intacct Corporation.
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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Xml.Request.Operation;

namespace Intacct.Sdk.Tests.Xml.Request.Operation
{

    [TestClass]
    public class LoginAuthenticationTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            SdkConfig config = new SdkConfig()
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
            };

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <login>
        <userid>testuser</userid>
        <companyid>testcompany</companyid>
        <password>testpass</password>
    </login>
</authentication>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            LoginAuthentication loginAuth = new LoginAuthentication(config);
            loginAuth.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required CompanyId not supplied in params")]
        public void InvalidCompanyIdTest()
        {
            SdkConfig config = new SdkConfig()
            {
                CompanyId = null,
                UserId = "testuser",
                UserPassword = "testpass",
            };

            LoginAuthentication loginAuth = new LoginAuthentication(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required UserId not supplied in params")]
        public void InvalidUserIdTest()
        {
            SdkConfig config = new SdkConfig()
            {
                CompanyId = "testcompany",
                UserId = null,
                UserPassword = "testpass",
            };

            LoginAuthentication loginAuth = new LoginAuthentication(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required UserPassword not supplied in params")]
        public void InvalidUserPasswordTest()
        {
            SdkConfig config = new SdkConfig()
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = null,
            };

            LoginAuthentication loginAuth = new LoginAuthentication(config);
        }

    }

}
