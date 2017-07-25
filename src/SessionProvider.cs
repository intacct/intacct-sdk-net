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
using Intacct.Sdk.Xml.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Intacct.Sdk
{
    public static class SessionProvider
    {
        
        /// <summary>
        /// Create a new ClientConfig based on another ClientConfig
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static async Task<ClientConfig> Factory(ClientConfig config = null)
        {
            if (config == null)
            {
                config = new ClientConfig();
            }

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "sessionProvider",
                NoRetryServerErrorCodes = new int[] { }, // Retry all 500 level errors
            };

            RequestHandler requestHandler = new RequestHandler(config, requestConfig);
            List<IFunction> content = new List<IFunction>();
            content.Add(new ApiSessionCreate());
            OnlineResponse response = await requestHandler.ExecuteOnline(content);

            Authentication authentication = response.Authentication;
            Result result = response.Results[0];

            result.EnsureStatusSuccess(); // Throw any result errors

            List<XElement> data = result.Data;
            XElement api = data[0];

            config.SessionId = api.Element("sessionid").Value;
            config.EndpointUrl = api.Element("endpoint").Value;

            config.CompanyId = authentication.CompanyId;
            config.UserId = authentication.UserId;

            config.Credentials = new SessionCredentials(config, new SenderCredentials(config));

            return config;
        }
    }

}