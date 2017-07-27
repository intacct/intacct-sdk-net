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
using IniParser;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class LoginCredentialsTest
    {

        protected SenderCredentials senderCreds;

        [TestInitialize()]
        public void Initialize()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!"
            };
            this.senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        public void CredsFromConfigTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass"
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);

            Assert.AreEqual("testcompany", loginCreds.CompanyId);
            Assert.AreEqual("testuser", loginCreds.UserId);
            Assert.AreEqual("testpass", loginCreds.Password);
            Endpoint endpoint = loginCreds.SenderCredentials.Endpoint;
            StringAssert.Equals("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint);
            Assert.IsInstanceOfType(loginCreds.SenderCredentials, typeof(SenderCredentials));
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

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { tempFile, new MockFileData(ini) },
            });

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "unittest",
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds, fileSystem);
            Assert.AreEqual("inicompanyid", loginCreds.CompanyId);
            Assert.AreEqual("iniuserid", loginCreds.UserId);
            Assert.AreEqual("iniuserpass", loginCreds.Password);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required Company ID not supplied in config or env variable \"INTACCT_COMPANY_ID\"")]
        public void CredsFromArrayNoCompanyIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "",
                UserId = "testuser",
                UserPassword = "testpass"
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required User ID not supplied in config or env variable \"INTACCT_USER_ID\"")]
        public void CredsFromArrayNoUserIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "testcompany",
                UserId = "",
                UserPassword = "testpass"
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required User Password not supplied in config or env variable \"INTACCT_USER_PASSWORD\"")]
        public void CredsFromArrayNoUserPasswordTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = ""
            };
            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);
        }
    }

}