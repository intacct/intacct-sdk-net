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

namespace Intacct.SDK
{
    public class OfflineClient : AbstractClient
    {
        /// <summary>
        /// Construct an Intacct Offline Client
        /// </summary>
        /// <param name="config"></param>
        public OfflineClient(ClientConfig config = null) : base(config)
        {
        }

        /// <summary>
        /// Execute one function to the Intacct API
        /// </summary>
        /// <param name="apiFunction"></param>
        /// <param name="requestConfig"></param>
        /// <returns></returns>
        public async Task<OfflineResponse> Execute(IFunction apiFunction, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }

            List<IFunction> apiFunctions = new List<IFunction>
            {
                apiFunction
            };

            return await this.ExecuteOfflineRequest(apiFunctions, requestConfig).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute multiple functions to the Intacct API
        /// </summary>
        /// <param name="apiFunctions"></param>
        /// <param name="requestConfig"></param>
        /// <returns></returns>
        public async Task<OfflineResponse> ExecuteBatch(List<IFunction> apiFunctions, RequestConfig requestConfig = null)
        {
            if (requestConfig == null)
            {
                requestConfig = new RequestConfig();
            }
            
            return await this.ExecuteOfflineRequest(apiFunctions, requestConfig).ConfigureAwait(false);
        }
    }
}