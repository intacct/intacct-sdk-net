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

    public class Control
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

        private string senderId;
        public string SenderId
        {
            get { return senderId; }
            private set
            {
                senderId = value;
            }
        }

        private string controlId;
        public string ControlId
        {
            get { return controlId; }
            private set
            {
                controlId = value;
            }
        }

        private string uniqueId;
        public string UniqueId
        {
            get { return uniqueId; }
            private set
            {
                uniqueId = value;
            }
        }

        private string dtdVersion;
        public string DtdVersion
        {
            get { return dtdVersion; }
            private set
            {
                dtdVersion = value;
            }
        }

        public Control(XElement control)
        {
            if (control.Element("status") == null)
            {
                throw new IntacctException("Control block is missing status element");
            }
            if (control.Element("senderid") == null)
            {
                throw new IntacctException("Control block is missing senderid element");
            }
            if (control.Element("controlid") == null)
            {
                throw new IntacctException("Control block is missing controlid element");
            }
            if (control.Element("uniqueid") == null)
            {
                throw new IntacctException("Control block is missing uniqueid element");
            }
            if (control.Element("dtdversion") == null)
            {
                throw new IntacctException("Control block is missing dtdversion element");
            }

            this.Status = control.Element("status").Value;
            this.SenderId = control.Element("senderid").Value;
            this.ControlId = control.Element("controlid").Value;
            this.UniqueId = control.Element("uniqueid").Value;
            this.DtdVersion = control.Element("dtdversion").Value;

        }

    }

}