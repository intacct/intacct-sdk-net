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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class InvoiceReverse : AbstractInvoice
    {
        public DateTime ReverseDate;
        
        public string Memo;

        public InvoiceReverse(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("reverse_invoice");
            
            xml.WriteAttribute("key", RecordNo);

            xml.WriteStartElement("datereversed");
            xml.WriteDateSplitElements(ReverseDate);
            xml.WriteEndElement(); //datereversed

            xml.WriteElement("description", Memo);

            xml.WriteEndElement(); //reverse_invoice

            xml.WriteEndElement(); //function
        }

    }
}