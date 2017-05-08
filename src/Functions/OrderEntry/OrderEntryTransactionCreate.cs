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

using Intacct.Sdk.Functions.InventoryControl;
using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;

namespace Intacct.Sdk.Functions.OrderEntry
{

    public class OrderEntryTransactionCreate : AbstractOrderEntryTransaction
    {

        public OrderEntryTransactionCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_sotransaction");

            xml.WriteElement("transactiontype", TransactionDefinition, true);

            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate.Value, true);
            xml.WriteEndElement(); //datecreated

            if (GlPostingDate.HasValue)
            {
                xml.WriteStartElement("dateposted");
                xml.WriteDateSplitElements(GlPostingDate.Value);
                xml.WriteEndElement(); //dateposted
            }

            xml.WriteElement("createdfrom", CreatedFrom);
            xml.WriteElement("customerid", CustomerId, true);
            xml.WriteElement("documentno", DocumentNumber);

            if (OriginalDocumentDate.HasValue)
            {
                xml.WriteStartElement("origdocdate");
                xml.WriteDateSplitElements(OriginalDocumentDate.Value);
                xml.WriteEndElement(); //origdocdate
            }

            xml.WriteElement("referenceno", ReferenceNumber);
            xml.WriteElement("termname", PaymentTerm);

            if (DueDate.HasValue)
            {
                xml.WriteStartElement("datedue");
                xml.WriteDateSplitElements(DueDate.Value, true);
                xml.WriteEndElement(); //datedue
            }
            
            xml.WriteElement("message", Message);
            xml.WriteElement("shippingmethod", ShippingMethod);

            if (!String.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("shipto");
                xml.WriteElement("contactname", ShipToContactName, true);
                xml.WriteEndElement(); //shipto
            }

            if (!String.IsNullOrWhiteSpace(BillToContactName))
            {
                xml.WriteStartElement("billto");
                xml.WriteElement("contactname", BillToContactName, true);
                xml.WriteEndElement(); //billto
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
            if (!String.IsNullOrWhiteSpace(ExchangeRateType))
            {
                xml.WriteElement("exchratetype", ExchangeRateType);
            }
            else if (ExchangeRateValue.HasValue)
            {
                xml.WriteElement("exchrate", ExchangeRateValue);
            }
            else if (!String.IsNullOrWhiteSpace(BaseCurrency) || !String.IsNullOrWhiteSpace(TransactionCurrency))
            {
                xml.WriteElement("exchratetype", ExchangeRateType, true);
            }

            xml.WriteElement("vsoepricelist", VsoePriceList);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("state", State);
            xml.WriteElement("projectid", ProjectId);

            xml.WriteStartElement("sotransitems");
            if (Lines.Count > 0)
            {
                foreach (OrderEntryTransactionLineCreate Line in Lines)
                {
                    Line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //sotransitems

            if (Subtotals.Count > 0)
            {
                xml.WriteStartElement("subtotals");
                foreach (TransactionSubtotalCreate Subtotal in Subtotals)
                {
                    Subtotal.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //subtotals
            }

            xml.WriteEndElement(); //create_sotransaction

            xml.WriteEndElement(); //function
        }

    }

}