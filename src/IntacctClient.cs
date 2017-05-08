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
using Intacct.Sdk.Xml;
using System;
using System.Threading.Tasks;

namespace Intacct.Sdk
{

    public class IntacctClient : AbstractClient
    {

        public SessionCredentials SessionCreds
        {
            get { return sessionCreds; }
            private set { }
        }

        public IntacctClient(SdkConfig config) : base(config)
        {
        }

        public new async Task<SynchronousResponse> Execute(
            Content contentBlock,
            bool transaction = false,
            string requestControlId = null,
            bool uniqueFunctionControlIds = false,
            SdkConfig config = null
        )
        {
            if (config == null)
            {
                config = new SdkConfig();
            }
            return await base.Execute(contentBlock, transaction, requestControlId, uniqueFunctionControlIds, config);
        }

        public new async Task<AsynchronousResponse> ExecuteAsync(
            Content contentBlock,
            string asyncPolicyId,
            bool transaction = false,
            string requestControlId = null,
            bool uniqueFunctionControlIds = false,
            SdkConfig config = null
        )
        {
            if (config == null)
            {
                config = new SdkConfig();
            }
            return await base.ExecuteAsync(contentBlock, asyncPolicyId, transaction, requestControlId, uniqueFunctionControlIds, config);
        }

    }
}
