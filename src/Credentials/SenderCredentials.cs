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

namespace Intacct.Sdk.Credentials
{
    public class SenderCredentials
    {

        const string SenderProfileEnvName = "INTACCT_SENDER_PROFILE";
        
        const string SenderIdEnvName = "INTACCT_SENDER_ID";
        
        const string SenderPasswordEnvName = "INTACCT_SENDER_PASSWORD";

        const string DefaultSenderProfile = "default";

        public string SenderId;

        public string Password;

        public Endpoint Endpoint;

        public SenderCredentials(SdkConfig config)
        {
            string envProfileName = Environment.GetEnvironmentVariable(SenderProfileEnvName);
            if (String.IsNullOrWhiteSpace(envProfileName))
            {
                envProfileName = DefaultSenderProfile;
            }
            if (String.IsNullOrWhiteSpace(config.ProfileName))
            {
                config.ProfileName = envProfileName;
            }
            if (String.IsNullOrWhiteSpace(config.SenderId))
            {
                config.SenderId = Environment.GetEnvironmentVariable(SenderIdEnvName);
            }
            if (String.IsNullOrWhiteSpace(config.SenderPassword))
            {
                config.SenderPassword = Environment.GetEnvironmentVariable(SenderPasswordEnvName);
            }

            if (
                String.IsNullOrWhiteSpace(config.SenderId)
                && String.IsNullOrWhiteSpace(config.SenderPassword)
                && !String.IsNullOrWhiteSpace(config.ProfileName)
            )
            {
                ProfileCredentialProvider profileProvider = new ProfileCredentialProvider();
                SdkConfig profileCreds = profileProvider.GetSenderCredentials(config);
                config.SenderId = !String.IsNullOrWhiteSpace(profileCreds.SenderId) ? profileCreds.SenderId : config.SenderId;
                config.SenderPassword = !String.IsNullOrWhiteSpace(profileCreds.SenderPassword) ? profileCreds.SenderPassword : config.SenderPassword;

                // Stop overwriting the Endpoint URL if it was passed in already
                config.EndpointUrl = !String.IsNullOrWhiteSpace(config.EndpointUrl) ? config.EndpointUrl : profileCreds.EndpointUrl;
            }


            if (String.IsNullOrWhiteSpace(config.SenderId))
            {
                throw new ArgumentException("Required SenderId not supplied in params or env variable \"" + SenderIdEnvName + "\"");
            }
            if (String.IsNullOrWhiteSpace(config.SenderPassword))
            {
                throw new ArgumentException("Required SenderPassword not supplied in params or env variable \"" + SenderPasswordEnvName + "\"");
            }

            SenderId = config.SenderId;
            Password = config.SenderPassword;
            Endpoint = new Endpoint(config);
        }

    }
}