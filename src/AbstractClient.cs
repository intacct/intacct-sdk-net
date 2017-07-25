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

using Intacct.Sdk.Credentials;
using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intacct.Sdk
{

    abstract public class AbstractClient
    {
        
        const string ProfileEnvName = "INTACCT_PROFILE";

        public ClientConfig Config;

        public AbstractClient(ClientConfig config = null)
        {
            if (config == null)
            {
                config = new ClientConfig();
            }

            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = Environment.GetEnvironmentVariable(AbstractClient.ProfileEnvName);
            }

            if (config.Credentials != null)
            {
                // Do not try and load credentials if they are already set in config
            }
            else if (!string.IsNullOrEmpty(config.SessionId))
            {
                config.Credentials = new SessionCredentials(config, new SenderCredentials(config));
            }
            else
            {
                config.Credentials = new LoginCredentials(config, new SenderCredentials(config));
            }
            Config = config;
        }
        
        protected async Task<OnlineResponse> ExecuteOnlineRequest(List<IFunction> functions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }

            RequestHandler requestHandler = new RequestHandler(this.Config, requestConfig);

            OnlineResponse response = await requestHandler.ExecuteOnline(functions);

            return response;
        }

        protected async Task<OfflineResponse> ExecuteOfflineRequest(List<IFunction> functions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }

            RequestHandler requestHandler = new RequestHandler(this.Config, requestConfig);

            OfflineResponse response = await requestHandler.ExecuteOffline(functions);

            return response;
        }
    }
}
