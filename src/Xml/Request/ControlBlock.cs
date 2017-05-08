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
        private string SenderId
        {
            get { return senderId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Required SenderId not supplied in params");
                }
                senderId = value;
            }
        }

        private string password;
        private string Password
        {
            get { return password; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Required SenderPassword not supplied in params");
                }
                password = value;
            }
        }

        private string controlId;
        private string ControlId
        {
            get { return controlId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = Guid.NewGuid().ToString();
                }

                if (value.Length < 1 || value.Length > 256)
                {
                    throw new ArgumentException("ControlId must be between 1 and 256 characters in length");
                }

                controlId = value;
            }
        }

        private bool uniqueId;
        private bool UniqueId
        {
            get { return uniqueId; }
            set
            {
                if (value)
                {
                    uniqueId = value;
                }
                else
                {
                    uniqueId = false;
                }
            }
        }

        private string dtdVersion;
        private string DtdVersion
        {
            get { return dtdVersion; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = "3.0";
                }

                if (value == "2.1" || value == "3.0")
                {
                    dtdVersion = value;
                }
                else
                {
                    throw new ArgumentException("DtdVersion is not a valid version");
                }
            }
        }

        private string policyId;
        private string PolicyId
        {
            get { return policyId; }
            set
            {
                policyId = value;
            }
        }

        private bool includeWhitespace = false;

        private bool debug;
        private bool Debug
        {
            get { return debug; }
            set
            {
                debug = value;
            }
        }

        public ControlBlock(SdkConfig config)
        {
            SenderId = config.SenderId;
            Password = config.SenderPassword;
            ControlId = config.ControlId;
            UniqueId = config.UniqueId;
            DtdVersion = config.DtdVersion;
            PolicyId = config.PolicyId;
            Debug = config.Debug;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("control");
            xml.WriteElement("senderid", SenderId, true);
            xml.WriteElement("password", Password, true);
            xml.WriteElement("controlid", ControlId, true);
            xml.WriteElement("uniqueid", UniqueId);
            xml.WriteElement("dtdversion", DtdVersion, true);
            xml.WriteElement("policyid", PolicyId);
            xml.WriteElement("includewhitespace", includeWhitespace);
            if (DtdVersion == "2.1")
            {
                xml.WriteElement("debug", Debug);
            }
            xml.WriteEndElement(); //control
        }

    }
}