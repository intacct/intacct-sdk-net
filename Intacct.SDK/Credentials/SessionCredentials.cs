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
    public class SessionCredentials : ICredentials
    {
        public string SessionId;

        public Endpoint Endpoint;

        public SenderCredentials SenderCredentials { get; set; }

        public SessionCredentials(ClientConfig config, SenderCredentials senderCreds)
        {
            if (string.IsNullOrEmpty(config.SessionId))
            {
                throw new ArgumentException("Required Session ID not supplied in config");
            }

            this.SessionId = config.SessionId;

            this.Endpoint = !string.IsNullOrEmpty(config.EndpointUrl)
                ? new Endpoint(config)
                : senderCreds.Endpoint;

            this.SenderCredentials = senderCreds;
        }
    }
}