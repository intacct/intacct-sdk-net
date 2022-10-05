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

    public class SessionCredentialsTest
    {
        
        protected SenderCredentials SenderCreds;

        public SessionCredentialsTest()
        {
            ClientConfig config = new ClientConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!"
            };
            this.SenderCreds = new SenderCredentials(config);            
        }
        
        [Fact]
        public void CredsFromConfigTest()
        {
            ClientConfig config = new ClientConfig
            {
                SessionId = "faKEsesSiOnId..",
                EndpointUrl = "https://p1.intacct.com/ia/xml/xmlgw.phtml"
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, this.SenderCreds);

            Assert.Equal("faKEsesSiOnId..", sessionCreds.SessionId);
            Assert.Equal("https://p1.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.Endpoint.ToString());
            Assert.IsType<SenderCredentials>(sessionCreds.SenderCredentials);
        }
        
        [Fact]
        public void CredsFromArrayNoEndpointTest()
        {
            ClientConfig config = new ClientConfig
            {
                SessionId = "faKEsesSiOnId..",
                EndpointUrl = ""
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, this.SenderCreds);

            Assert.Equal("faKEsesSiOnId..", sessionCreds.SessionId);
            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.Endpoint.ToString());
        }
        
        [Fact]
        public void CredsFromArrayNoUserPasswordTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = ""
            };
            
            var ex = Record.Exception(() => new SessionCredentials(config, this.SenderCreds));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required Session ID not supplied in config", ex.Message);
        }
    }
}