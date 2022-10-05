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

using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsPayable
{
    public abstract class AbstractApPaymentFunction : AbstractFunction
    {
        private readonly int _recordNo;

        public const string Delete = "delete";

        public const string Decline = "decline_appaymentrequest";

        public const string Confirm = "confirm_appaymentrequest";
        
        public const string Approve = "approve_appaymentrequest";

        public const string Send = "send_appaymentrequest";
        
        public const string Void = "void_appaymentrequest";
        
        protected AbstractApPaymentFunction(int recordNo, string controlId = null) : base(controlId)
        {
            this._recordNo = recordNo;
        }

        protected abstract string GetFunction();

        public override void WriteXml(ref IaXmlWriter xml)
        {
            switch (GetFunction())
            {
                case Delete:
                    WriteCrudXml(ref xml);
                    break;
                case Decline:
                case Confirm:
                case Approve:
                case Send:
                case Void:
                    WriteLegacyXml(ref xml);
                    break;
                default:
                    throw new IntacctException("Cannot write XML for ApPaymentFunction " + GetFunction());
            }
        }
        
        private void WriteCrudXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);
           
            xml.WriteStartElement(GetFunction());

            xml.WriteElement("object", "APPYMT");
            xml.WriteElement("keys", _recordNo);
            
            xml.WriteEndElement(); //delete

            xml.WriteEndElement(); //function
        }
        
        private void WriteLegacyXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);
           
            xml.WriteStartElement(GetFunction());

            xml.WriteStartElement("appaymentkeys");

            xml.WriteElement("appaymentkey", _recordNo, true);

            xml.WriteEndElement(); //appaymentkeys

            xml.WriteEndElement(); // GetFunction

            xml.WriteEndElement(); //function
        }
    }
}