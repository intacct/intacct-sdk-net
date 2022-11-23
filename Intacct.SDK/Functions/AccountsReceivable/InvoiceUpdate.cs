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
    public class InvoiceUpdate : AbstractInvoice
    {

        public InvoiceUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update_invoice");
            if (!string.IsNullOrWhiteSpace(ExternalId))
            {
                xml.WriteAttribute("key", ExternalId);
                xml.WriteAttribute("externalkey", true);
            }
            else
            {
                xml.WriteAttribute("key", RecordNo);
            }
            
            xml.WriteElement("customerid", CustomerId);

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
            } else if (!string.IsNullOrWhiteSpace(PaymentTerm))
            {
                xml.WriteElement("termname", PaymentTerm, true);
            }

            xml.WriteElement("action", Action);
            xml.WriteElement("invoiceno", InvoiceNumber);
            xml.WriteElement("ponumber", ReferenceNumber);
            xml.WriteElement("description", Description);

            if (!string.IsNullOrWhiteSpace(BillToContactName))
            {
                xml.WriteStartElement("payto");
                xml.WriteElement("contactname", BillToContactName);
                xml.WriteEndElement(); //payto
            }
            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("returnto");
                xml.WriteElement("contactname", ShipToContactName);
                xml.WriteEndElement(); //returnto
            }

            WriteXmlMultiCurrencySection(ref xml);

            xml.WriteElement("supdocid", AttachmentsId);

            xml.WriteCustomFieldsExplicit(CustomFields);

            if (Lines.Count > 0)
            {
                xml.WriteStartElement("updateinvoiceitems");
                foreach (AbstractInvoiceLine line in Lines)
                {
                    line.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //updateinvoiceitems
            }

            xml.WriteEndElement(); //update_invoice

            xml.WriteEndElement(); //function
        }

        private new void WriteXmlMultiCurrencySection(ref IaXmlWriter xml)
        {
            xml.WriteElement("currency", TransactionCurrency);

            if (ExchangeRateDate.HasValue)
            {
                xml.WriteStartElement("exchratedate");
                xml.WriteDateSplitElements(ExchangeRateDate.Value);
                xml.WriteEndElement(); //exchratedate
            }
            if (!string.IsNullOrWhiteSpace(ExchangeRateType))
            {
                xml.WriteElement("exchratetype", ExchangeRateType);
            }
            else if (ExchangeRateValue.HasValue)
            {
                xml.WriteElement("exchrate", ExchangeRateValue);
            }
            else if (!string.IsNullOrWhiteSpace(BaseCurrency) || !string.IsNullOrWhiteSpace(TransactionCurrency))
            {
                xml.WriteElement("exchratetype", ExchangeRateType, true);
            }
        }
    }
}