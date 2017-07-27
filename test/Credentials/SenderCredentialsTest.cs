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
    public class SenderCredentialsTest
    {

        [TestMethod()]
        public void CredsFromArrayTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                EndpointUrl = "https://unittest.intacct.com/ia/xmlgw.phtml"
            };

            SenderCredentials senderCreds = new SenderCredentials(config);

            Assert.AreEqual("testsenderid", senderCreds.SenderId);
            Assert.AreEqual("pass123!", senderCreds.Password);
            StringAssert.Equals("https://unittest.intacct.com/ia/xml/xmlgw.phtml", senderCreds.Endpoint);
        }

        [TestMethod()]
        public void CredsFromEnvTest()
        {
            Environment.SetEnvironmentVariable("INTACCT_SENDER_ID", "envsender");
            Environment.SetEnvironmentVariable("INTACCT_SENDER_PASSWORD", "envpass");

            ClientConfig config = new ClientConfig();

            SenderCredentials senderCreds = new SenderCredentials(config);

            Assert.AreEqual("envsender", senderCreds.SenderId);
            Assert.AreEqual("envpass", senderCreds.Password);

            Environment.SetEnvironmentVariable("INTACCT_SENDER_ID", null);
            Environment.SetEnvironmentVariable("INTACCT_SENDER_PASSWORD", null);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required Sender ID not supplied in config or env variable \"INTACCT_SENDER_ID\"")]
        public void CredsFromArrayNoSenderIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderPassword = "pass123!"
            };
            SenderCredentials senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required Sender Password not supplied in config or env variable \"INTACCT_SENDER_PASSWORD\"")]
        public void CredsFromArrayNoSenderPasswordTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid"
            };
            SenderCredentials senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        public void CredsFromProfileTest()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[unittest]
sender_id = inisenderid
sender_password = inisenderpass
endpoint_url = https://unittest.intacct.com/ia/xmlgw.phtml";

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

            SenderCredentials senderCreds = new SenderCredentials(config, fileSystem);
            Assert.AreEqual("inisenderid", senderCreds.SenderId);
            Assert.AreEqual("inisenderpass", senderCreds.Password);
            Assert.AreEqual("https://unittest.intacct.com/ia/xmlgw.phtml", senderCreds.Endpoint.Url);
        }
        
        [TestMethod()]
        public void CredsFromProfileOverrideEndpointTest()
        {
            var parser = new FileIniDataParser();

            string ini = @"
[unittest]
sender_id = inisenderid
sender_password = inisenderpass
endpoint_url = https://unittest.intacct.com/ia/xmlgw.phtml";

            string tempFile = Path.Combine(Path.GetTempPath(), ".intacct", "credentials.ini");

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { tempFile, new MockFileData(ini) },
            });

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "unittest",
                EndpointUrl = "https://somethingelse.intacct.com/ia/xmlgw.phtml",
            };

            SenderCredentials senderCreds = new SenderCredentials(config, fileSystem);
            Assert.AreEqual("inisenderid", senderCreds.SenderId);
            Assert.AreEqual("inisenderpass", senderCreds.Password);
            Assert.AreEqual("https://somethingelse.intacct.com/ia/xmlgw.phtml", senderCreds.Endpoint.Url);
        }
    }

}