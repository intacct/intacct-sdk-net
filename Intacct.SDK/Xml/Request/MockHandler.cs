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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Intacct.SDK.Xml.Request
{
    public class MockHandler : DelegatingHandler
    {
        public List<HttpResponseMessage> Queue;

        protected HttpResponseMessage LastResponse;

        public MockHandler() : base()
        {
        }

        public MockHandler(List<HttpResponseMessage> queue) : base()
        {
            Queue = queue;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Queue.Count > 0)
            {
                LastResponse = Queue[0];

                // Remove this response from the queue
                Queue.RemoveAt(0);
            }
            else
            {
                throw new InvalidOperationException("Mock queue is empty");
            }

            return Task.FromResult(LastResponse);
        }
    }
}