﻿/*
 * Copyright 2018 Sage Intacct, Inc.
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

    public class LoginCredentialsTest
    {
        
        protected SenderCredentials SenderCreds;

        public LoginCredentialsTest()
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
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass"
            };

            LoginCredentials loginCreds = new LoginCredentials(config, this.SenderCreds);

            Assert.Equal("testcompany", loginCreds.CompanyId);
            Assert.Equal("testuser", loginCreds.UserId);
            Assert.Equal("testpass", loginCreds.Password);
            Endpoint endpoint = loginCreds.SenderCredentials.Endpoint;
            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.ToString());
            Assert.IsType<SenderCredentials>(loginCreds.SenderCredentials);
        }
        
        [Fact]
        public void CredsFromProfileTest()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials\\Ini\\default.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "unittest",
            };

            LoginCredentials loginCreds = new LoginCredentials(config, this.SenderCreds);
            Assert.Equal("inicompanyid", loginCreds.CompanyId);
            Assert.Equal("iniuserid", loginCreds.UserId);
            Assert.Equal("iniuserpass", loginCreds.Password);
        }
        
        [Fact]
        public void CredsFromArrayNoCompanyIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "",
                UserId = "testuser",
                UserPassword = "testpass"
            };
            
            var ex = Record.Exception(() => new LoginCredentials(config, this.SenderCreds));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required Company ID not supplied in config or env variable \"INTACCT_COMPANY_ID\"", ex.Message);
        }
        
        [Fact]
        public void CredsFromArrayNoUserIdTest()
        {
            ClientConfig config = new ClientConfig
            {
                CompanyId = "testcompany",
                UserId = "",
                UserPassword = "testpass"
            };
            
            var ex = Record.Exception(() => new LoginCredentials(config, this.SenderCreds));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required User ID not supplied in config or env variable \"INTACCT_USER_ID\"", ex.Message);
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
            
            var ex = Record.Exception(() => new LoginCredentials(config, this.SenderCreds));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Required User Password not supplied in config or env variable \"INTACCT_USER_PASSWORD\"", ex.Message);
        }
    }
}