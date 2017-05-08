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

using Intacct.Sdk.Logging;
using Intacct.Sdk.Xml.Request;
using NLog;
using System;

namespace Intacct.Sdk.Credentials
{

    public class LoginCredentials
    {

        const string CompanyProfileEnvName = "INTACCT_COMPANY_PROFILE";
        
        const string CompanyIdEnvName = "INTACCT_COMPANY_ID";
        
        const string UserIdEnvName = "INTACCT_USER_ID";

        const string UserPasswordEnvName = "INTACCT_USER_PASSWORD";

        const string DefaultCompanyProfile = "default";

        public string CompanyId;

        public string UserId;

        public string Password;

        public SenderCredentials SenderCreds;

        public MockHandler MockHandler;

        public ILogger Logger;

        public MessageFormatter LogMessageFormat;

        public LogLevel LogLevel;

        public Endpoint Endpoint
        {
            get
            {
                return SenderCreds.Endpoint;
            }
            private set
            {
                //nothing to see here
            }
        }
        
        public LoginCredentials(SdkConfig config, SenderCredentials senderCreds)
        {
            string envProfileName = Environment.GetEnvironmentVariable(CompanyProfileEnvName);
            if (String.IsNullOrWhiteSpace(envProfileName))
            {
                envProfileName = DefaultCompanyProfile;
            }
            if (String.IsNullOrWhiteSpace(config.ProfileName))
            {
                config.ProfileName = envProfileName;
            }
            if (String.IsNullOrWhiteSpace(config.CompanyId))
            {
                config.CompanyId = Environment.GetEnvironmentVariable(CompanyIdEnvName);
            }
            if (String.IsNullOrWhiteSpace(config.UserId))
            {
                config.UserId = Environment.GetEnvironmentVariable(UserIdEnvName);
            }
            if (String.IsNullOrWhiteSpace(config.UserPassword))
            {
                config.UserPassword = Environment.GetEnvironmentVariable(UserPasswordEnvName);
            }
            
            if (
                String.IsNullOrWhiteSpace(config.CompanyId)
                && String.IsNullOrWhiteSpace(config.UserId)
                && String.IsNullOrWhiteSpace(config.UserPassword)
                && !String.IsNullOrWhiteSpace(config.ProfileName)
            )
            {
                ProfileCredentialProvider profileProvider = new ProfileCredentialProvider();
                SdkConfig profileCreds = profileProvider.GetLoginCredentials(config);
                config.CompanyId = !String.IsNullOrWhiteSpace(profileCreds.CompanyId) ? profileCreds.CompanyId : config.CompanyId;
                config.UserId = !String.IsNullOrWhiteSpace(profileCreds.UserId) ? profileCreds.UserId : config.UserId;
                config.UserPassword = !String.IsNullOrWhiteSpace(profileCreds.UserPassword) ? profileCreds.UserPassword : config.UserPassword;
            }
            
            if (String.IsNullOrWhiteSpace(config.CompanyId))
            {
                throw new ArgumentException("Required CompanyId not supplied in params or env variable \"" + CompanyIdEnvName + "\"");
            }
            if (String.IsNullOrWhiteSpace(config.UserId))
            {
                throw new ArgumentException("Required UserId not supplied in params or env variable \"" + UserIdEnvName + "\"");
            }
            if (String.IsNullOrWhiteSpace(config.UserPassword))
            {
                throw new ArgumentException("Required UserPassword not supplied in params or env variable \"" + UserPasswordEnvName + "\"");
            }

            CompanyId = config.CompanyId;
            UserId = config.UserId;
            Password = config.UserPassword;
            SenderCreds = senderCreds;
            MockHandler = config.MockHandler;

            Logger = config.Logger;
            LogMessageFormat = config.LogFormatter;
            LogLevel = config.LogLevel;
        }

    }
}