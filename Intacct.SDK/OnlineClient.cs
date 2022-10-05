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

using System.Collections.Generic;
using System.Threading.Tasks;
using Intacct.SDK.Functions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;

namespace Intacct.SDK
{
    public class OnlineClient : AbstractClient
    {
        /// <summary>
        /// Construct an Intacct Online Client
        /// </summary>
        /// <param name="config"></param>
        public OnlineClient(ClientConfig config = null) : base(config)
        {
        }

        /// <summary>
        /// Execute one Intacct API function
        /// </summary>
        /// <param name="apiFunction"></param>
        /// <param name="requestConfig"></param>
        /// <returns></returns>
        public async Task<OnlineResponse> Execute(IFunction apiFunction, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }

            List<IFunction> apiFunctions = new List<IFunction>
            {
                apiFunction
            };

            OnlineResponse response = await this.ExecuteOnlineRequest(apiFunctions, requestConfig).ConfigureAwait(false);

            response.Results[0].EnsureStatusSuccess();

            return response;
        }

        /// <summary>
        /// Execute multiple Intacct API functions
        /// </summary>
        /// <param name="apiFunctions"></param>
        /// <param name="requestConfig"></param>
        /// <returns></returns>
        public async Task<OnlineResponse> ExecuteBatch(List<IFunction> apiFunctions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }
            
            OnlineResponse response = await this.ExecuteOnlineRequest(apiFunctions, requestConfig).ConfigureAwait(false);

            if (requestConfig.Transaction == true)
            {
                foreach (Result result in response.Results)
                {
                    result.EnsureStatusNotFailure();
                }
            }

            return response;
        }
    }
}