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
using Intacct.Sdk.Functions;
using System;
using System.Collections.Generic;

namespace Intacct.Sdk.Xml.Request
{
    public class OperationBlock
    {

        public bool Transaction;

        public IAuthentication Authentication;

        public List<IFunction> Content;

        public OperationBlock(ClientConfig clientConfig, RequestConfig requestConfig, List<IFunction> content)
        {
            Transaction = requestConfig.Transaction;

            ICredentials credentials = clientConfig.Credentials;
            if (credentials != null && credentials.GetType() == typeof(SessionCredentials))
            {
                SessionCredentials sessionCreds = credentials as SessionCredentials;
                this.Authentication = new SessionAuthentication(sessionCreds.SessionId);
            }
            else if (credentials != null && credentials.GetType() == typeof(LoginCredentials))
            {
                LoginCredentials loginCreds = credentials as LoginCredentials;
                this.Authentication = new LoginAuthentication(loginCreds.UserId, loginCreds.CompanyId, loginCreds.Password);
            }
            else if (!string.IsNullOrEmpty(clientConfig.SessionId))
            {
                this.Authentication = new SessionAuthentication(clientConfig.SessionId);
            }
            else if (
                !string.IsNullOrEmpty(clientConfig.CompanyId)
                && !string.IsNullOrEmpty(clientConfig.UserId)
                && !string.IsNullOrEmpty(clientConfig.UserPassword)
            )
            {
                Authentication = new LoginAuthentication(clientConfig.UserId, clientConfig.CompanyId, clientConfig.UserPassword);
            }
            else
            {
                throw new ArgumentException("Authentication credentials [Company ID, User ID, and User Password] or [Session ID] are required and cannot be blank");
            }

            this.Content = content;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("operation");
            xml.WriteAttribute("transaction", (this.Transaction == true) ? "true" : "false");

            Authentication.WriteXml(ref xml);

            xml.WriteStartElement("content");
            foreach (IFunction func in Content)
            {
                func.WriteXml(ref xml);
            }
            xml.WriteEndElement(); // content

            xml.WriteEndElement(); // operation
        }
    }
}