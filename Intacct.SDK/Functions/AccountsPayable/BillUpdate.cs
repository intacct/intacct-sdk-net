/*
 * Copyright 2018 Sage Intacct, Inc.
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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class BillUpdate : AbstractBill
    {
        public BillUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update_bill");
            xml.WriteAttribute("key", RecordNo, true);
            
            xml.WriteElement("vendorid", VendorId);

            if (TransactionDate.HasValue)
            {
                xml.WriteStartElement("datecreated");
                xml.WriteDateSplitElements(TransactionDate.Value);
                xml.WriteEndElement(); //datecreated   
            }

            if (GlPostingDate.HasValue)
            {
                xml.WriteStartElement("dateposted");
                xml.WriteDateSplitElements(GlPostingDate.Value);
                xml.WriteEndElement(); //dateposted
            }

            if (DueDate.HasValue)
            {
                xml.WriteStartElement("datedue");
                xml.WriteDateSplitElements(DueDate.Value);
                xml.WriteEndElement(); //datedue

                xml.WriteElement("termname", PaymentTerm);
            } else
            {
                xml.WriteElement("termname", PaymentTerm);
            }

            xml.WriteElement("action", Action);
            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("billno", BillNumber);
            xml.WriteElement("ponumber", ReferenceNumber);
            xml.WriteElement("onhold", OnHold);
            xml.WriteElement("description", Description);
            xml.WriteElement("externalid", ExternalId);

            if (!string.IsNullOrWhiteSpace(PayToContactName))
            {
                xml.WriteStartElement("payto");
                xml.WriteElement("contactname", PayToContactName);
                xml.WriteEndElement(); //payto
            }
            if (!string.IsNullOrWhiteSpace(ReturnToContactName))
            {
                xml.WriteStartElement("returnto");
                xml.WriteElement("contactname", ReturnToContactName);
                xml.WriteEndElement(); //returnto
            }

            WriteXmlMultiCurrencySection(ref xml);

            xml.WriteElement("nogl", DoNotPostToGL);
            xml.WriteElement("supdocid", AttachmentsId);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteStartElement("updatebillitems");
            if (Lines.Count > 0)
            {
                foreach (AbstractBillLine line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //updatebillitems

            xml.WriteEndElement(); //update_bill

            xml.WriteEndElement(); //function
        }
    }
}