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
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.OrderEntry
{
    public class RecurringOrderEntryTransactionCreate : AbstractRecurringOrderEntryTransaction
    {
        
        public RecurringOrderEntryTransactionCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);
            
            xml.WriteStartElement("create_recursotransaction");
            
            xml.WriteElement("transactiontype", TransactionDefinition);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("referenceno", ReferenceNumber);
            xml.WriteElement("termname", PaymentTerm);
            xml.WriteElement("message", Message);
            xml.WriteElement("contractid", ContractId);
            xml.WriteElement("contractdesc", ContractDescription);
            xml.WriteElement("customerponumber", CustomerPoNumber);
            xml.WriteElement("shippingmethod", ShippingMethod);
            
            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("shipto");
                xml.WriteElement("contactname", ShipToContactName, true);
                xml.WriteEndElement(); //shipto
            }

            if (!string.IsNullOrWhiteSpace(BillToContactName))
            {
                xml.WriteStartElement("billto");
                xml.WriteElement("contactname", BillToContactName, true);
                xml.WriteEndElement(); //billto
            }
            
            xml.WriteElement("externalid", ExternalId);
            xml.WriteElement("basecurr", BaseCurrency);
            xml.WriteElement("currency", TransactionCurrency);
            
            if (!string.IsNullOrWhiteSpace(ExchangeRateType))
            {
                xml.WriteElement("exchratetype", ExchangeRateType);
            }
            
            xml.WriteCustomFieldsExplicit(CustomFields);
            
            xml.WriteElement("taxsolutionid", TaxSolutionId);

            xml.WriteElement("paymethod", PayMethod);
            xml.WriteElement("payinfull", IsPayInFull);
            xml.WriteElement("paymentamount", PaymentAmount);
            xml.WriteElement("creditcardtype", CreditCardType);
            xml.WriteElement("customercreditcardkey", CustomerCreditCardKey);
            xml.WriteElement("customerbankaccountkey", CustomerBankAccountKey);
            xml.WriteElement("accounttype", AccountType);

            if (!string.IsNullOrWhiteSpace(AccountType))
            {
                if (AccountType.Equals("Bank"))
                {
                    xml.WriteElement("bankaccountid", BankAccountId);
                }
                else if (AccountType.Equals("Undeposited Funds Account"))
                {
                    xml.WriteElement("glaccountkey", GlAccountKey);
                }
            }
            
            if (StartDate.HasValue)
            {
                xml.WriteStartElement("startdate");
                xml.WriteDateSplitElements(StartDate.Value);
                xml.WriteEndElement(); // revrecstartdate
            }
            
            // if either enddate or occur have values, create the ending object
            if (EndDate.HasValue || NumberOfOccurrences.HasValue)
            {
                xml.WriteStartElement("ending");
                if (!EndDate.HasValue)
                {
                    xml.WriteElement("occur", NumberOfOccurrences);
                }
                else
                {
                    xml.WriteStartElement("enddate");
                    xml.WriteDateSplitElements(EndDate.Value);
                    xml.WriteEndElement(); // enddate
                }
                xml.WriteEndElement(); //ending
            }
            
            xml.WriteElement("modenew", RepeatMode);
            xml.WriteElement("interval", RepeatInterval);

            if (!string.IsNullOrWhiteSpace(RepeatMode))
            {
                if (RepeatMode.Equals("Months"))
                {
                    xml.WriteElement("eom", IsEndOfMonth); 
                }
            }
            
            xml.WriteElement("status", Status);
            xml.WriteElement("docstatus", DocStatus);
            
            xml.WriteStartElement("recursotransitems");
            if (Lines.Count > 0)
            {
                foreach (RecurringOrderEntryTransactionLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //recursotransitems
            
            if (Subtotals.Count > 0)
            {
                xml.WriteStartElement("subtotals");
                foreach (TransactionSubtotalCreate subtotal in Subtotals)
                {
                    subtotal.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //subtotals
            }
            
            xml.WriteEndElement(); //create_recursotransaction
            
            xml.WriteEndElement(); //function
        }
    }
}