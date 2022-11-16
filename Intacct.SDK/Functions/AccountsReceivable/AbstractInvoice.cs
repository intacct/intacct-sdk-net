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
using System.Collections.Generic;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public abstract class AbstractInvoice : AbstractFunction
    {

        public int? RecordNo;

        public string CustomerId;

        public DateTime? TransactionDate;

        public DateTime? GlPostingDate;

        public string Action;

        public int? SummaryRecordNo;

        public string InvoiceNumber;

        public string Description;

        public string ExternalId;

        public string BaseCurrency;

        public string TransactionCurrency;

        public DateTime? ExchangeRateDate;

        public decimal? ExchangeRateValue;

        public string ExchangeRateType;

        public bool? DoNotPostToGl;

        public DateTime? DueDate;

        public string PaymentTerm;

        public string ReferenceNumber;

        public bool? OnHold;

        public string BillToContactName;

        public string ShipToContactName;

        public string AttachmentsId;

        public string TaxSolutionId;

        public List<AbstractInvoiceLine> Lines = new List<AbstractInvoiceLine>();

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractInvoice(string controlId = null) : base(controlId)
        {
        }

        protected void WriteXmlMultiCurrencySection(ref IaXmlWriter xml)
        {
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
        }
        
    }
}
