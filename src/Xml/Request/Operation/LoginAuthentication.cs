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

namespace Intacct.Sdk.Xml.Request.Operation
{

    public class LoginAuthentication : AbstractAuthentication
    {

        private string UserId;

        private string CompanyId;

        private string Password;

        public LoginAuthentication(SdkConfig config)
        {
            if (String.IsNullOrWhiteSpace(config.UserId))
            {
                throw new ArgumentException("Required UserId not supplied in params");
            }
            if (String.IsNullOrWhiteSpace(config.CompanyId))
            {
                throw new ArgumentException("Required CompanyId not supplied in params");
            }
            if (String.IsNullOrWhiteSpace(config.UserPassword))
            {
                throw new ArgumentException("Required UserPassword not supplied in params");
            }

            UserId = config.UserId;
            CompanyId = config.CompanyId;
            Password = config.UserPassword;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("authentication");
            xml.WriteStartElement("login");
            xml.WriteElement("userid", UserId, true);
            xml.WriteElement("companyid", CompanyId, true);
            xml.WriteElement("password", Password, true);
            xml.WriteEndElement(); // login
            xml.WriteEndElement(); // authentication
        }

    }

}