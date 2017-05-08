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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Credentials;
using Intacct.Sdk.Xml.Request;
using System.Net.Http;
using System.Collections.Generic;
using IniParser;
using System.IO;

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class LoginCredentialsTest
    {

        protected SenderCredentials senderCreds;

        [TestInitialize()]
        public void Initialize()
        {
            SdkConfig config = new SdkConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!"
            };
            senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        public void CredsFromArrayTest()
        {
            SdkConfig config = new SdkConfig
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass"
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);

            Assert.AreEqual("testcompany", loginCreds.CompanyId);
            Assert.AreEqual("testuser", loginCreds.UserId);
            Assert.AreEqual("testpass", loginCreds.Password);
            StringAssert.Equals("https://api.intacct.com/ia/xml/xmlgw.phtml", loginCreds.Endpoint);
            Assert.IsInstanceOfType(loginCreds.SenderCreds, typeof(SenderCredentials));
        }
        
        [TestMethod()]
        public void CredsFromProfileTest()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[unittest]
company_id = inicompanyid
user_id = iniuserid
user_password = iniuserpass";

            string tempFile = Path.Combine(Path.GetTempPath(), ".intacct", "credentials.ini");
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                sw.WriteLine(ini);
            }

            SdkConfig config = new SdkConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "unittest",
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
            Assert.AreEqual("inicompanyid", loginCreds.CompanyId);
            Assert.AreEqual("iniuserid", loginCreds.UserId);
            Assert.AreEqual("iniuserpass", loginCreds.Password);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required CompanyId not supplied in params or env variable \"INTACCT_COMPANY_ID\"")]
        public void CredsFromArrayNoCompanyIdTest()
        {
            SdkConfig config = new SdkConfig
            {
                CompanyId = null,
                UserId = "testuser",
                UserPassword = "testpass"
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required UserId not supplied in params or env variable \"INTACCT_USER_ID\"")]
        public void CredsFromArrayNoUserIdTest()
        {
            SdkConfig config = new SdkConfig
            {
                CompanyId = "testcompany",
                UserId = null,
                UserPassword = "testpass"
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required UserPassword not supplied in params or env variable \"INTACCT_USER_PASSWORD\"")]
        public void CredsFromArrayNoUserPasswordTest()
        {
            SdkConfig config = new SdkConfig
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = null
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }

        [TestMethod()]
        public void GetMockHandlerTest()
        {
            HttpResponseMessage mockResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig()
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
                MockHandler = mockHandler,
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);

            Assert.IsInstanceOfType(loginCreds.MockHandler, typeof(MockHandler));
        }
    }

}