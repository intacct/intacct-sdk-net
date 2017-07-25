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

using System;

namespace Intacct.Sdk.Xml.Request
{

    public class LoginAuthentication : IAuthentication
    {

        private string userId;

        public string UserId
        {
            get { return this.userId; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("User ID is required and cannot be blank");
                }
                this.userId = value;
            }
        }

        private string companyId;

        public string CompanyId
        {
            get { return this.companyId; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Company ID is required and cannot be blank");
                }
                this.companyId = value;
            }
        }

        private string password;

        public string Password
        {
            get { return this.password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("User Password is required and cannot be blank");
                }
                this.password = value;
            }
        }

        public LoginAuthentication(string userId, string companyId, string password)
        {
            this.UserId = userId;
            this.CompanyId = companyId;
            this.Password = password;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("authentication");
            xml.WriteStartElement("login");
            xml.WriteElement("userid", this.UserId, true);
            xml.WriteElement("companyid", this.CompanyId, true);
            xml.WriteElement("password", this.Password, true);
            xml.WriteEndElement(); // login
            xml.WriteEndElement(); // authentication
        }

    }

}