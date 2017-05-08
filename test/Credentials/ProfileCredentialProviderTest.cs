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
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using IniParser;
using System.IO;
using System.Text;
using IniParser.Model;

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class ProfileCredentialProviderTest
    {

        protected ProfileCredentialProvider provider;

        [TestInitialize()]
        public void Initialize()
        {
            provider = new ProfileCredentialProvider();
        }

        [TestMethod()]
        public void GetCredentialsFromDefaultProfileTest()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[default]
sender_id = defsenderid
sender_password = defsenderpass
company_id = defcompanyid
user_id = defuserid
user_password = defuserpass
endpoint_url = https://unittest.intacct.com/ia/xmlgw.phtml

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
            };

            SdkConfig loginCreds = provider.GetLoginCredentials(config);

            SdkConfig expectedLogin = new SdkConfig()
            {
                CompanyId = "defcompanyid",
                UserId = "defuserid",
                UserPassword = "defuserpass",
            };
            Assert.AreEqual(expectedLogin.CompanyId, loginCreds.CompanyId);
            Assert.AreEqual(expectedLogin.UserId, loginCreds.UserId);
            Assert.AreEqual(expectedLogin.UserPassword, loginCreds.UserPassword);

            SdkConfig senderCreds = provider.GetSenderCredentials(config);

            SdkConfig expectedSender = new SdkConfig()
            {
                SenderId = "defsenderid",
                SenderPassword = "defsenderpass",
                EndpointUrl = "https://unittest.intacct.com/ia/xmlgw.phtml",
            };
            Assert.AreEqual(expectedSender.SenderId, senderCreds.SenderId);
            Assert.AreEqual(expectedSender.SenderPassword, senderCreds.SenderPassword);
            Assert.AreEqual(expectedSender.EndpointUrl, senderCreds.EndpointUrl);
        }

        [TestMethod()]
        public void GetLoginCredentialsFromSpecificProfileTest()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[default]
sender_id = defsenderid
sender_password = defsenderpass
company_id = defcompanyid
user_id = defuserid
user_password = defuserpass

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

            SdkConfig loginCreds = provider.GetLoginCredentials(config);

            SdkConfig expectedLogin = new SdkConfig()
            {
                CompanyId = "inicompanyid",
                UserId = "iniuserid",
                UserPassword = "iniuserpass",
            };
            Assert.AreEqual(expectedLogin.CompanyId, loginCreds.CompanyId);
            Assert.AreEqual(expectedLogin.UserId, loginCreds.UserId);
            Assert.AreEqual(expectedLogin.UserPassword, loginCreds.UserPassword);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Profile name \"default\" not found in credentials file")]
        public void GetLoginCredentialsMissingDefault()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[notdefault]
sender_id = testsenderid
sender_password = testsenderpass";

            string tempFile = Path.Combine(Path.GetTempPath(), ".intacct", "credentials.ini");
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                sw.WriteLine(ini);
            }

            SdkConfig config = new SdkConfig()
            {
                ProfileFile = tempFile,
            };

            SdkConfig loginCreds = provider.GetLoginCredentials(config);
        }
    }
}