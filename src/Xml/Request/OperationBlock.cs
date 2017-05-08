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
using System;
using Intacct.Sdk.Xml.Request.Operation;

namespace Intacct.Sdk.Xml.Request
{
    public class OperationBlock
    {

        private bool Transaction;

        private AbstractAuthentication Authentication;

        private Content Content;

        public OperationBlock(SdkConfig config, Content content)
        {
            Transaction = config.Transaction;

            if (!String.IsNullOrWhiteSpace(config.SessionId))
            {
                Authentication = new SessionAuthentication(config);
            }
            else if (
                !String.IsNullOrWhiteSpace(config.CompanyId)
                && !String.IsNullOrWhiteSpace(config.UserId)
                && !String.IsNullOrWhiteSpace(config.UserPassword)
            )
            {
                Authentication = new LoginAuthentication(config);
            }
            else
            {
                throw new ArgumentException("Required CompanyId, UserId, and UserPassword, or SessionId, not supplied in params");
            }

            Content = content;
        }

        // TODO Module Preferences

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("operation");
            xml.WriteAttribute("transaction", Transaction);

            Authentication.WriteXml(ref xml);

            xml.WriteStartElement("content");
            foreach (AbstractFunction function in Content)
            {
                function.WriteXml(ref xml);
            }
            xml.WriteEndElement(); // content

            xml.WriteEndElement(); // operation
        }
    }
}