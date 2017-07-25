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

using Intacct.Sdk.Exceptions;
using System.IO;
using System.Xml.Linq;

namespace Intacct.Sdk.Xml.Response
{

    public class Authentication
    {

        private string status;
        public string Status
        {
            get { return status; }
            private set
            {
                status = value;
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            private set
            {
                userId = value;
            }
        }

        private string companyId;
        public string CompanyId
        {
            get { return companyId; }
            private set
            {
                companyId = value;
            }
        }
        
        public Authentication(XElement authentication)
        {
            if (authentication.Element("status") == null)
            {
                throw new IntacctException("Authentication block is missing status element");
            }
            if (authentication.Element("userid") == null)
            {
                throw new IntacctException("Authentication block is missing userid element");
            }
            if (authentication.Element("companyid") == null)
            {
                throw new IntacctException("Authentication block is missing companyid element");
            }
            
            this.Status = authentication.Element("status").Value;
            this.UserId = authentication.Element("userid").Value;
            this.CompanyId = authentication.Element("companyid").Value;

            // TODO add getter/setter for elements: clientstatus, clientid, locationid, sessiontimestamp
        }

    }

}