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

using System.Xml.Linq;
using Intacct.SDK.Exceptions;

namespace Intacct.SDK.Xml.Response
{
    public class Control
    {
        public string Status { get; }

        public string SenderId { get; }

        public string ControlId { get; }

        public string UniqueId { get; }

        public string DtdVersion { get; }

        public Control(XElement control)
        {
            var status = control.Element("status");
            if (status == null)
            {
                throw new IntacctException("Control block is missing status element");
            }
            this.Status = status.Value;

            var senderId = control.Element("senderid");
            if (senderId != null)
            {
                this.SenderId = senderId.Value;
            }
            

            var controlId = control.Element("controlid");
            if (controlId != null)
            {
                this.ControlId = controlId.Value;
            }

            var uniqueId = control.Element("uniqueid");
            if (uniqueId != null)
            {
                this.UniqueId = uniqueId.Value;
            }

            var dtdVersion = control.Element("dtdversion");
            if (dtdVersion != null)
            {
                this.DtdVersion = dtdVersion.Value;
            }
        }
    }
}