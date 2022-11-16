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

namespace Intacct.SDK.Credentials
{
    public class SenderCredentials
    {

        public const string SenderProfileEnvName = "INTACCT_SENDER_PROFILE";
        
        public const string SenderIdEnvName = "INTACCT_SENDER_ID";
        
        public const string SenderPasswordEnvName = "INTACCT_SENDER_PASSWORD";

        public const string DefaultSenderProfile = "default";

        public string SenderId;

        public string Password;

        public Endpoint Endpoint;

        public SenderCredentials(ClientConfig config)
        {
            string envProfileName = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(SenderCredentials.SenderProfileEnvName))
                ? Environment.GetEnvironmentVariable(SenderCredentials.SenderProfileEnvName)
                : SenderCredentials.DefaultSenderProfile;

            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = envProfileName;
            }
            if (string.IsNullOrEmpty(config.SenderId))
            {
                config.SenderId = Environment.GetEnvironmentVariable(SenderCredentials.SenderIdEnvName);
            }
            if (string.IsNullOrEmpty(config.SenderPassword))
            {
                config.SenderPassword = Environment.GetEnvironmentVariable(SenderCredentials.SenderPasswordEnvName);
            }

            if (
                string.IsNullOrEmpty(config.SenderId)
                && string.IsNullOrEmpty(config.SenderPassword)
                && !string.IsNullOrEmpty(config.ProfileName)
            )
            {
                ClientConfig profile = ProfileCredentialProvider.GetSenderCredentials(config);

                if (!string.IsNullOrEmpty(profile.SenderId))
                {
                    config.SenderId = profile.SenderId;
                }
                if (!string.IsNullOrEmpty(profile.SenderPassword))
                {
                    config.SenderPassword = profile.SenderPassword;
                }
                if (string.IsNullOrEmpty(config.EndpointUrl))
                {
                    // Only set the endpoint URL if it was never passed in to begin with
                    config.EndpointUrl = profile.EndpointUrl;
                }
            }
            
            if (string.IsNullOrEmpty(config.SenderId))
            {
                throw new ArgumentException("Required Sender ID not supplied in config or env variable \"" + SenderIdEnvName + "\"");
            }
            if (string.IsNullOrEmpty(config.SenderPassword))
            {
                throw new ArgumentException("Required Sender Password not supplied in config or env variable \"" + SenderPasswordEnvName + "\"");
            }

            this.SenderId = config.SenderId;
            this.Password = config.SenderPassword;
            this.Endpoint = new Endpoint(config);
        }

    }
}