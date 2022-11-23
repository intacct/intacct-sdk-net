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
using System.Xml;

namespace Intacct.SDK.Xml.Request
{
    public class LoginAuthentication: IAuthentication
    {
        private string _userId;

        public string UserId
        {
            get => this._userId;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("User ID is required and cannot be blank");
                }
                this._userId = value;
            }
        }

        private string _companyId;

        public string CompanyId
        {
            get => this._companyId;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Company ID is required and cannot be blank");
                }
                this._companyId = value;
            }
        }

        private string _entityId;

        public string EntityId
        {
            get => this._entityId;
            set => _entityId = value;
        }

        private string _password;

        public string Password
        {
            get => this._password;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("User Password is required and cannot be blank");
                }
                this._password = value;
            }
        }

        public LoginAuthentication(string userId, string companyId, string password, string entityId = null)
        {
            this.UserId = userId;
            this.CompanyId = companyId;
            this.Password = password;
            this.EntityId = entityId;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("authentication");
            xml.WriteStartElement("login");
            xml.WriteElementString("userid", this.UserId);
            xml.WriteElementString("companyid", this.CompanyId);
            xml.WriteElementString("password", this.Password);
            if (this.EntityId != null)
            {
                xml.WriteElementString("locationid", this.EntityId);
            }
            xml.WriteEndElement(); // login
            xml.WriteEndElement(); // authentication
        }
    }
}