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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.CashManagement
{
    public class ChargeCardTransactionUpdate : AbstractChargeCardTransaction
    {

        public ChargeCardTransactionUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update_cctransaction");

            xml.WriteAttribute("key", RecordNo, true);
            
            if(TransactionDate.HasValue)
            {
                xml.WriteStartElement("paymentdate");
                xml.WriteDateSplitElements(TransactionDate.Value);
                xml.WriteEndElement(); //paymentdate
            }

            xml.WriteElement("referenceno", ReferenceNumber);
            xml.WriteElement("payee", Payee);
            xml.WriteElement("description", Description);
            xml.WriteElement("supdocid", AttachmentsId);
            xml.WriteElement("currency", TransactionCurrency);

            if (ExchangeRateValue.HasValue)
            {
                xml.WriteElement("exchrate", ExchangeRateValue);
            }
            else if (ExchangeRateDate.HasValue || !string.IsNullOrWhiteSpace(ExchangeRateType))
            {
                if (ExchangeRateDate.HasValue)
                {
                    xml.WriteStartElement("exchratedate");
                    xml.WriteDateSplitElements(ExchangeRateDate.Value);
                    xml.WriteEndElement();
                }
                xml.WriteElement("exchratetype", ExchangeRateType, true);
            }

            xml.WriteCustomFieldsExplicit(CustomFields);

            if (Lines.Count > 0)
            {
                xml.WriteStartElement("updateccpayitems");
                foreach (ChargeCardTransactionLineUpdate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //updateccpayitems
            }

            xml.WriteEndElement(); //update_cctransaction

            xml.WriteEndElement(); //function
        }
    }
}