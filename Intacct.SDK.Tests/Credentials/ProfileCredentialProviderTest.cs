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
    public class ProfileCredentialProviderTest
    {
        [Fact]
        public void GetLoginCredentialsFromSpecificProfileTest()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "Ini", "default.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "unittest",
            };

            ClientConfig loginCreds = ProfileCredentialProvider.GetLoginCredentials(config);

            Assert.Equal("inicompanyid", loginCreds.CompanyId);
            Assert.Null(loginCreds.EntityId);
            Assert.Equal("iniuserid", loginCreds.UserId);
            Assert.Equal("iniuserpass", loginCreds.UserPassword);
        }
        
        [Fact]
        public void GetLoginCredentialsWithEntityFromSpecificProfileTest()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "Ini", "default.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
                ProfileName = "entity",
            };

            ClientConfig loginCreds = ProfileCredentialProvider.GetLoginCredentials(config);

            Assert.Equal("inicompanyid", loginCreds.CompanyId);
            Assert.Equal("inientityid", loginCreds.EntityId);
            Assert.Equal("iniuserid", loginCreds.UserId);
            Assert.Equal("iniuserpass", loginCreds.UserPassword);
        }

        [Fact]
        public void GetLoginCredentialsMissingDefault()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "Ini", "notdefault.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
            };

            var ex = Record.Exception(() => ProfileCredentialProvider.GetLoginCredentials(config));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Profile name \"default\" not found in credentials file", ex.Message);
        }

        [Fact]
        public void GetLoginCredentialsFromMissingIni()
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "invalid", "file.ini");

            ClientConfig config = new ClientConfig()
            {
                ProfileFile = tempFile,
            };

            ClientConfig loginCreds = ProfileCredentialProvider.GetLoginCredentials(config);

            Assert.Null(loginCreds.CompanyId);
            Assert.Null(loginCreds.EntityId);
            Assert.Null(loginCreds.UserId);
            Assert.Null(loginCreds.UserPassword);
        }
    }
}