﻿/*
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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class ArPaymentReverse : AbstractArPayment
    {

        public DateTime ReverseDate;
        
        public string Memo;
        
        public ArPaymentReverse(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("reverse_arpayment");
            
            xml.WriteAttribute("key", RecordNo);
            
            xml.WriteStartElement("datereversed");
            xml.WriteDateSplitElements(ReverseDate, true);
            xml.WriteEndElement(); //datereversed

            xml.WriteElement("description", Memo);
             
            xml.WriteEndElement(); //reverse_arpayment

            xml.WriteEndElement(); //function
        }

    }
}