﻿/*
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

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.SDK.Credentials;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Functions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;

namespace Intacct.SDK
{
    public class SessionProvider
    {
        /// <summary>
        /// Create a new session-based ClientConfig, based on a different ClientConfig
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="IntacctException"></exception>
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

            ApiSessionCreate apiFunction = new ApiSessionCreate();

            if (!string.IsNullOrWhiteSpace(config.SessionId) && config.EntityId != null)
            {
                apiFunction.EntityId = config.EntityId;
            }

            OnlineClient client = new OnlineClient(config);
            OnlineResponse response = await client.Execute(apiFunction, requestConfig).ConfigureAwait(false);

            Authentication authentication = response.Authentication;
            Result result = response.Results[0];

            result.EnsureStatusSuccess(); // Throw any result errors

            List<XElement> data = result.Data;
            XElement api = data[0];

            config.SessionId = api.Element("sessionid")?.Value;
            config.EndpointUrl = api.Element("endpoint")?.Value;
            config.EntityId = api.Element("locationid")?.Value;

            config.CompanyId = authentication.CompanyId;
            config.UserId = authentication.UserId;

            config.Credentials = new SessionCredentials(config, new SenderCredentials(config));

            return config;
        }
    }
}