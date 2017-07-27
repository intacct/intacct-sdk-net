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
using System.IO.Abstractions;

namespace Intacct.Sdk.Credentials
{

    public class LoginCredentials : ICredentials
    {

        const string CompanyProfileEnvName = "INTACCT_COMPANY_PROFILE";
        
        const string CompanyIdEnvName = "INTACCT_COMPANY_ID";
        
        const string UserIdEnvName = "INTACCT_USER_ID";

        const string UserPasswordEnvName = "INTACCT_USER_PASSWORD";

        const string DefaultCompanyProfile = "default";

        public string CompanyId;

        public string UserId;

        public string Password;

        private SenderCredentials senderCredentials;

        public SenderCredentials SenderCredentials
        {
            get { return this.senderCredentials; }
            set { this.senderCredentials = value; }
        }

        public LoginCredentials(ClientConfig config, SenderCredentials senderCreds)
        {
            this.loginCredentials(config, senderCreds, new FileSystem());
        }
        public LoginCredentials(ClientConfig config, SenderCredentials senderCreds, IFileSystem fileSystem)
        {
            this.loginCredentials(config, senderCreds, fileSystem);
        }

        private void loginCredentials(ClientConfig config, SenderCredentials senderCreds, IFileSystem fileSystem)
        {
            string envProfileName = Environment.GetEnvironmentVariable(LoginCredentials.CompanyProfileEnvName);
            if (string.IsNullOrEmpty(envProfileName))
            {
                envProfileName = LoginCredentials.DefaultCompanyProfile;
            }
            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = envProfileName;
            }

            if (string.IsNullOrEmpty(config.CompanyId))
            {
                config.CompanyId = Environment.GetEnvironmentVariable(LoginCredentials.CompanyIdEnvName);
            }
            if (string.IsNullOrEmpty(config.UserId))
            {
                config.UserId = Environment.GetEnvironmentVariable(LoginCredentials.UserIdEnvName);
            }
            if (string.IsNullOrEmpty(config.UserPassword))
            {
                config.UserPassword = Environment.GetEnvironmentVariable(LoginCredentials.UserPasswordEnvName);
            }
            
            if (
                string.IsNullOrEmpty(config.CompanyId)
                && string.IsNullOrEmpty(config.UserId)
                && string.IsNullOrEmpty(config.UserPassword)
                && !string.IsNullOrEmpty(config.ProfileName)
            )
            {
                ClientConfig profile = ProfileCredentialProvider.GetLoginCredentials(config, fileSystem);

                if (!string.IsNullOrEmpty(profile.CompanyId))
                {
                    config.CompanyId = profile.CompanyId;
                }
                if (!string.IsNullOrEmpty(profile.UserId))
                {
                    config.UserId = profile.UserId;
                }
                if (!string.IsNullOrEmpty(profile.UserPassword))
                {
                    config.UserPassword = profile.UserPassword;
                }
            }
            
            if (string.IsNullOrEmpty(config.CompanyId))
            {
                throw new ArgumentException("Required Company ID not supplied in config or env variable \"" + LoginCredentials.CompanyIdEnvName + "\"");
            }
            if (string.IsNullOrEmpty(config.UserId))
            {
                throw new ArgumentException("Required User ID not supplied in config or env variable \"" + LoginCredentials.UserIdEnvName + "\"");
            }
            if (string.IsNullOrEmpty(config.UserPassword))
            {
                throw new ArgumentException("Required User Password not supplied in config or env variable \"" + LoginCredentials.UserPasswordEnvName + "\"");
            }

            this.CompanyId = config.CompanyId;
            this.UserId = config.UserId;
            this.Password = config.UserPassword;
            this.SenderCredentials = senderCreds;
        }

    }
}