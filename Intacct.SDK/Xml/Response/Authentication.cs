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

using System.Xml.Linq;
using Intacct.SDK.Exceptions;

namespace Intacct.SDK.Xml.Response
{
    public class Authentication
    {
        public string Status { get; }

        public string UserId { get; }

        public string CompanyId { get; }
        
        public string EntityId { get; }
        
        public string ClientStatus { get; }
        
        public string ClientId { get; }
        
        public string SessionTimeStamp { get; }
        
        public string SessionTimeOut { get; }

        public Authentication(XElement authentication)
        {
            var status = authentication.Element("status");
            if (status == null)
            {
                throw new IntacctException("Authentication block is missing status element");
            }
            this.Status = status.Value;

            var userId = authentication.Element("userid");
            if (userId == null)
            {
                throw new IntacctException("Authentication block is missing userid element");
            }
            this.UserId = userId.Value;

            var companyId = authentication.Element("companyid");
            if (companyId == null)
            {
                throw new IntacctException("Authentication block is missing companyid element");
            }
            this.CompanyId = companyId.Value;
            
            var entityId = authentication.Element("locationid");
            if (entityId != null)
            {
                this.EntityId = entityId.Value;
            }

            var clientStatus = authentication.Element("clientstatus");
            if (clientStatus != null)
            {
                this.ClientStatus = clientStatus.Value;
            }
            
            var clientId = authentication.Element("clientid");
            if (clientStatus != null)
            {
                this.ClientId = clientId.Value;
            }
            
            var sessionTimeStamp = authentication.Element("sessiontimestamp");
            if (sessionTimeStamp != null)
            {
                this.SessionTimeStamp = sessionTimeStamp.Value;
            }
            
            var sessionTimeOut = authentication.Element("sessiontimeout");
            if (sessionTimeOut != null)
            {
                this.SessionTimeOut = sessionTimeOut.Value;
            }
        }
    }
}