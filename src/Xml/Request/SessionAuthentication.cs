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

    public class SessionAuthentication : IAuthentication
    {

        private string sessionId;

        public string SessionId
        {
            get { return sessionId; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Session ID is required and cannot be blank");
                }
                this.sessionId = value;
            }
        }

        public SessionAuthentication(string sessionId)
        {
            this.SessionId = sessionId;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("authentication");
            xml.WriteElement("sessionid", this.SessionId, true);
            xml.WriteEndElement(); // authentication
        }

    }

}