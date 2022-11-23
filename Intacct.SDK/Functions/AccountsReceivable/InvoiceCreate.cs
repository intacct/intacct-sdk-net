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
    public class InvoiceCreate : AbstractInvoice
    {

        public InvoiceCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_invoice");
            
            xml.WriteElement("customerid", CustomerId, true);

            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate.Value);
            xml.WriteEndElement(); //datecreated

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
                xml.WriteElement("termname", PaymentTerm, true);
            }

            xml.WriteElement("action", Action);
            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("invoiceno", InvoiceNumber);
            xml.WriteElement("ponumber", ReferenceNumber);
            xml.WriteElement("onhold", OnHold);
            xml.WriteElement("description", Description);
            xml.WriteElement("externalid", ExternalId);

            if (!string.IsNullOrWhiteSpace(BillToContactName))
            {
                xml.WriteStartElement("billto");
                xml.WriteElement("contactname", BillToContactName);
                xml.WriteEndElement(); //billto
            }
            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("shipto");
                xml.WriteElement("contactname", ShipToContactName);
                xml.WriteEndElement(); //shipto
            }

            WriteXmlMultiCurrencySection(ref xml);

            xml.WriteElement("nogl", DoNotPostToGl);
            xml.WriteElement("supdocid", AttachmentsId);

            xml.WriteElement("taxsolutionid", TaxSolutionId); 

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteStartElement("invoiceitems");
            if (Lines.Count > 0)
            {
                foreach (InvoiceLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //invoiceitems

            xml.WriteEndElement(); //create_invoice

            xml.WriteEndElement(); //function
        }

    }
}
