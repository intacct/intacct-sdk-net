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
using System.Collections.Generic;
using System.Threading.Tasks;
using Intacct.SDK.Credentials;
using Intacct.SDK.Functions;
using Intacct.SDK.Xml;

namespace Intacct.SDK
{
    public abstract class AbstractClient
    {
        protected const string ProfileEnvName = "INTACCT_PROFILE";

        public ClientConfig Config;

        protected AbstractClient(ClientConfig config = null)
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
            this.Config = config;
        }
        
        protected async Task<OnlineResponse> ExecuteOnlineRequest(List<IFunction> functions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }
            
            RequestHandler requestHandler = new RequestHandler(this.Config, requestConfig);

            OnlineResponse response = await requestHandler.ExecuteOnline(functions).ConfigureAwait(false);

            return response;
        }

        protected async Task<OfflineResponse> ExecuteOfflineRequest(List<IFunction> functions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }
            
            RequestHandler requestHandler = new RequestHandler(this.Config, requestConfig);

            OfflineResponse response = await requestHandler.ExecuteOffline(functions).ConfigureAwait(false);

            return response;
        }
    }
}