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

using System.IO;
using Intacct.SDK.Exceptions;

namespace Intacct.SDK.Xml
{
    public class OfflineResponse: AbstractResponse
    {
        public string Status { get; }

        public OfflineResponse(Stream body) : base(body)
        {
            var acknowledgement = Response.Element("acknowledgement");
            if (acknowledgement == null)
            {
                throw new IntacctException("Response is missing acknowledgement block");
            }

            var status = acknowledgement.Element("status");
            if (status == null)
            {
                throw new IntacctException("Acknowledgement block is missing status element");
            }
            this.Status = status.Value;
        }
    }
}