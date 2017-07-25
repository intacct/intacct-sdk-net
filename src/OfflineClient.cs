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

using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intacct.Sdk
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
        public async Task<OfflineResponse> execute(IFunction apiFunction, RequestConfig requestConfig = null)
        {
            List<IFunction> apiFunctions = new List<IFunction>();
            apiFunctions.Add(apiFunction);

            return await this.ExecuteOfflineRequest(apiFunctions, requestConfig);
        }

        /// <summary>
        /// Execute multiple functions to the Intacct API
        /// </summary>
        /// <param name="apiFunctions"></param>
        /// <param name="requestConfig"></param>
        /// <returns></returns>
        public async Task<OfflineResponse> executeBatch(List<IFunction> apiFunctions, RequestConfig requestConfig = null)
        {
            return await this.ExecuteOfflineRequest(apiFunctions, requestConfig);
        }
    }
}
