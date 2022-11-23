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

using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class ArPaymentApply : AbstractArPayment
    {

        public string Memo;

        public ArPaymentApply(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("apply_arpayment");
            
            xml.WriteElement("arpaymentkey", RecordNo, true);
            
            xml.WriteStartElement("paymentdate");
            xml.WriteDateSplitElements(ReceivedDate.Value, true);
            xml.WriteEndElement(); //paymentdate

            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("memo", Memo);
            xml.WriteElement("overpaylocid", OverpaymentLocationId);
            xml.WriteElement("overpaydeptid", OverpaymentDepartmentId);

            if (ApplyToTransactions.Count > 0)
            {
                xml.WriteStartElement("arpaymentitems");
                foreach (ArPaymentItem applyToTransaction in ApplyToTransactions)
                {
                    applyToTransaction.WriteXml(ref xml);
                }
                
                xml.WriteEndElement(); //arpaymentitems
            }

            xml.WriteEndElement(); //apply_arpayment

            xml.WriteEndElement(); //function
        }

    }
}