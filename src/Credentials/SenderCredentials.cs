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
    public class SenderCredentials
    {

        const string SenderProfileEnvName = "INTACCT_SENDER_PROFILE";
        
        const string SenderIdEnvName = "INTACCT_SENDER_ID";
        
        const string SenderPasswordEnvName = "INTACCT_SENDER_PASSWORD";

        const string DefaultSenderProfile = "default";

        public string SenderId;

        public string Password;

        public Endpoint Endpoint;

        public SenderCredentials(ClientConfig config)
        {
            this.senderCredentials(config, new FileSystem());
        }
        public SenderCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            this.senderCredentials(config, fileSystem);
        }

        public void senderCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            string envProfileName = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(SenderProfileEnvName))
                ? Environment.GetEnvironmentVariable(SenderProfileEnvName)
                : SenderCredentials.DefaultSenderProfile;

            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = envProfileName;
            }
            if (string.IsNullOrEmpty(config.SenderId))
            {
                config.SenderId = Environment.GetEnvironmentVariable(SenderIdEnvName);
            }
            if (string.IsNullOrEmpty(config.SenderPassword))
            {
                config.SenderPassword = Environment.GetEnvironmentVariable(SenderPasswordEnvName);
            }

            if (
                string.IsNullOrEmpty(config.SenderId)
                && string.IsNullOrEmpty(config.SenderPassword)
                && !string.IsNullOrEmpty(config.ProfileName)
            )
            {
                ClientConfig profile = ProfileCredentialProvider.GetSenderCredentials(config, fileSystem);

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