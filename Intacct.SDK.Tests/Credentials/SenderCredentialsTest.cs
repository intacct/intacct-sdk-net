/*
 * Copyright 2022 Sage Intacct, Inc.
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
using System.IO;
using Intacct.SDK.Credentials;
using Xunit;

namespace Intacct.SDK.Tests.Credentials
{

    public class SenderCredentialsTest
    {
        
        [Fact]
        public void CredsFromArrayTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                EndpointUrl = "https://unittest.intacct.com/ia/xml/xmlgw.phtml"
            };

            SenderCredentials senderCreds = new SenderCredentials(config);

            Assert.Equal("testsenderid", senderCreds.SenderId);
            Assert.Equal("pass123!", senderCreds.Password);
            Assert.Equal("https://unittest.intacct.com/ia/xml/xmlgw.phtml", senderCreds.Endpoint.ToString());
        }
        
        [Fact]
        public void CredsFromEnvTest()
        {
            Environment.SetEnvironmentVariable("INTACCT_SENDER_ID", "envsender");
            Environment.SetEnvironmentVariable("INTACCT_SENDER_PASSWORD", "envpass");

            ClientConfig config = new ClientConfig();

            SenderCredentials senderCreds = new SenderCredentials(config);

            Assert.Equal("envsender", senderCreds.SenderId);
            Assert.Equal("envpass", senderCreds.Password);

            Environment.SetEnvironmentVariable("INTACCT_SENDER_ID", null);
            Environment.SetEnvironmentVariable("INTACCT_SENDER_PASSWORD", null);
        }
        
        [Fact]
        public void CredsFromArrayNoSenderIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderPassword = "pass123!"
            };
            
            var ex = Record.Exception(() => new SenderCredentials(config));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required Sender ID not supplied in config or env variable \"INTACCT_SENDER_ID\"", ex.Message);
        }
        
        [Fact]
        public void CredsFromArrayNoSenderPasswordTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid"
            };
            
            var ex = Record.Exception(() => new SenderCredentials(config));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required Sender Password not supplied in config or env variable \"INTACCT_SENDER_PASSWORD\"",
                ex.Message);
        }
        
        [Fact]
        public void CredsFromProfileTest()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "Ini", "default.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "anothertest",
            };

            SenderCredentials senderCreds = new SenderCredentials(config);
            Assert.Equal("inisenderid", senderCreds.SenderId);
            Assert.Equal("inisenderpass", senderCreds.Password);
            Assert.Equal("https://unittest.intacct.com/ia/xmlgw.phtml", senderCreds.Endpoint.Url);
        }
        
        [Fact]
        public void CredsFromProfileOverrideEndpointTest()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "Ini", "default.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "anothertest",
                EndpointUrl = "https://somethingelse.intacct.com/ia/xmlgw.phtml",
            };

            SenderCredentials senderCreds = new SenderCredentials(config);
            Assert.Equal("inisenderid", senderCreds.SenderId);
            Assert.Equal("inisenderpass", senderCreds.Password);
            Assert.Equal("https://somethingelse.intacct.com/ia/xmlgw.phtml", senderCreds.Endpoint.Url);
        }
    }
}