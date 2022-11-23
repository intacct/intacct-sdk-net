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
    public class ControlBlock : IXmlObject
    {
        
        private string _senderId;

        public string SenderId
        {
            get => _senderId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sender ID is required and cannot be blank");
                }
                _senderId = value;
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sender Password is required and cannot be blank");
                }
                _password = value;
            }
        }

        private string _controlId;

        public string ControlId
        {
            get => _controlId;
            set
            {
                if (value.Length < 1 || value.Length > 256)
                {
                    throw new ArgumentException("Request Control ID must be between 1 and 256 characters in length");
                }

                _controlId = value;
            }
        }

        public bool UniqueId;

        public string DtdVersion { get; private set; }

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
            xml.WriteElementString("senderid", this.SenderId);
            xml.WriteElementString("password", this.Password);
            xml.WriteElementString("controlid", this.ControlId);
            xml.WriteElementString("uniqueid", this.UniqueId == true ? "true" : "false");
            xml.WriteElementString("dtdversion", this.DtdVersion);
            if (!string.IsNullOrEmpty(this.PolicyId))
            {
                xml.WriteElementString("policyid", this.PolicyId);
            }
            xml.WriteElementString("includewhitespace", this.IncludeWhitespace == true ? "true" : "false");
            xml.WriteEndElement(); //control
        }
    }
}