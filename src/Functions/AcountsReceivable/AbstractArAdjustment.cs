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

using System;
using System.Collections.Generic;
using Intacct.Sdk.Xml;

namespace Intacct.Sdk.Functions.AccountsReceivable
{

    abstract public class AbstractArAdjustment : AbstractFunction
    {
        
        public int? RecordNo;

        public string CustomerId;

        public DateTime TransactionDate;
        
        public DateTime? GlPostingDate;

        public string Action;

        public string SummaryRecordNo;

        public string InvoiceNumber;

        public string Description;

        public string ExternalId;

        public string BaseCurrency;

        public string TransactionCurrency;

        public DateTime? ExchangeRateDate;

        public decimal? ExchangeRateValue;

        public string ExchangeRateType;

        public bool? DoNotPostToGL;

        public string AdjustmentNumber;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        public List<AbstractArAdjustmentLine> Lines = new List<AbstractArAdjustmentLine>();

        public AbstractArAdjustment(string controlId = null) : base(controlId)
        {
        }

        public void WriteXmlMultiCurrencySection(ref IaXmlWriter xml)
        {
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
        }
    }
}