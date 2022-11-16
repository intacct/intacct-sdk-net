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

using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Purchasing
{
    public class PurchasingTransactionUpdate : AbstractPurchasingTransaction
    {

        public PurchasingTransactionUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update_potransaction");
            if (!string.IsNullOrWhiteSpace(ExternalId))
            {
                xml.WriteAttribute("key", ExternalId);
                xml.WriteAttribute("externalkey", true);
            }
            else
            {
                xml.WriteAttribute("key", DocumentId);
            }

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

            xml.WriteElement("referenceno", ReferenceNumber);
            xml.WriteElement("vendordocno", VendorDocNumber);
            xml.WriteElement("termname", PaymentTerm);

            if (DueDate.HasValue)
            {
                xml.WriteStartElement("datedue");
                xml.WriteDateSplitElements(DueDate.Value);
                xml.WriteEndElement(); //datedue   
            }
            
            xml.WriteElement("message", Message);
            xml.WriteElement("shippingmethod", ShippingMethod);

            if (!string.IsNullOrWhiteSpace(ReturnToContactName))
            {
                xml.WriteStartElement("returnto");
                xml.WriteElement("contactname", ReturnToContactName);
                xml.WriteEndElement(); //returnto                
            }

            if (!string.IsNullOrWhiteSpace(PayToContactName))
            {
                xml.WriteStartElement("payto");
                xml.WriteElement("contactname", PayToContactName);
                xml.WriteEndElement(); //payto                
            }
            
            if (!string.IsNullOrWhiteSpace(DeliverToContactName))
            {
                xml.WriteStartElement("deliverto");
                xml.WriteElement("contactname", DeliverToContactName, true);
                xml.WriteEndElement(); //deliverto
            }

            xml.WriteElement("supdocid", AttachmentsId);
            xml.WriteElement("externalid", ExternalId);
            
            xml.WriteElement("basecurr", BaseCurrency);
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
            
            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("state", State);
            xml.WriteElement("projectid", ProjectId);

            if (Lines.Count > 0)
            {
                xml.WriteStartElement("updatepotransitems");
                foreach (AbstractPurchasingTransactionLine line in Lines)
                {
                    line.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //updatepotransitems
            }

            if (Subtotals.Count > 0)
            {
                xml.WriteStartElement("updatesubtotals");
                foreach (TransactionSubtotalUpdate subtotal in Subtotals)
                {
                    subtotal.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //updatesubtotals
            }

            xml.WriteEndElement(); //update_potransaction

            xml.WriteEndElement(); //function
        }

        
    }
}