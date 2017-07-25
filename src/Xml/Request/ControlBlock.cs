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
    public class ControlBlock
    {

        private string senderId;

        public string SenderId
        {
            get { return senderId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sender ID is required and cannot be blank");
                }
                senderId = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sender Password is required and cannot be blank");
                }
                password = value;
            }
        }

        private string controlId;

        public string ControlId
        {
            get { return controlId; }
            set
            {
                if (value.Length < 1 || value.Length > 256)
                {
                    throw new ArgumentException("Request Control ID must be between 1 and 256 characters in length");
                }

                controlId = value;
            }
        }

        private bool UniqueId;

        private string dtdVersion;

        public string DtdVersion
        {
            get { return dtdVersion; }
            private set
            {
                this.dtdVersion = value;
            }
        }

        public string PolicyId;

        public bool IncludeWhitespace;

        public ControlBlock(ClientConfig clientConfig, RequestConfig requestConfig)
        {
            this.SenderId = clientConfig.SenderId;
            this.Password = clientConfig.SenderPassword;
            this.ControlId = requestConfig.ControlId;
            this.UniqueId = requestConfig.UniqueId;
            this.PolicyId = requestConfig.PolicyId;
            this.IncludeWhitespace = false;
            this.DtdVersion = "3.0";
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("control");
            xml.WriteElement("senderid", this.SenderId, true);
            xml.WriteElement("password", this.Password, true);
            xml.WriteElement("controlid", this.ControlId, true);
            if (this.UniqueId == true)
            {
                xml.WriteElement("uniqueid", "true");
            }
            else
            {
                xml.WriteElement("uniqueid", "false");
            }
            xml.WriteElement("dtdversion", this.DtdVersion, true);
            if (!string.IsNullOrEmpty(this.PolicyId))
            {
                xml.WriteElement("policyid", this.PolicyId);
            }
            xml.WriteElement("includewhitespace", this.IncludeWhitespace);
            xml.WriteEndElement(); //control
        }

    }
}