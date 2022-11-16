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

namespace Intacct.SDK.Functions.CashManagement
{
    public class OtherReceiptCreate : AbstractOtherReceipt
    {

        public OtherReceiptCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("record_otherreceipt");
                        
            xml.WriteStartElement("paymentdate");
            xml.WriteDateSplitElements(TransactionDate);
            xml.WriteEndElement(); //paymentdate

            xml.WriteElement("payee", Payer, true);

            xml.WriteStartElement("receiveddate");
            xml.WriteDateSplitElements(ReceiptDate);
            xml.WriteEndElement(); //receiveddate

            xml.WriteElement("paymentmethod", PaymentMethod, true);

            if(!string.IsNullOrWhiteSpace(BankAccountId) || DepositDate.HasValue)
            {
                xml.WriteElement("bankaccountid", BankAccountId, true);
                if (DepositDate.HasValue)
                {
                    xml.WriteStartElement("depositdate");
                    xml.WriteDateSplitElements(DepositDate.Value);
                    xml.WriteEndElement(); //depositdate
                }
            } else
            {
                xml.WriteElement("undepglaccountno", UndepositedFundsGlAccountNo, true);
            }

            xml.WriteElement("refid", TransactionNo);
            xml.WriteElement("description", Description);
            xml.WriteElement("supdocid", AttachmentsId);
            xml.WriteElement("currency", TransactionCurrency);
            
            if (ExchangeRateValue.HasValue)
            {
                xml.WriteElement("exchrate", ExchangeRateValue);
            } else if (ExchangeRateDate.HasValue || !string.IsNullOrWhiteSpace(ExchangeRateType))
            {
                if (ExchangeRateDate.HasValue)
                {
                    xml.WriteStartElement("exchratedate");
                    xml.WriteDateSplitElements(ExchangeRateDate.Value);
                    xml.WriteEndElement(); //exchratedate
                }
                xml.WriteElement("exchratetype", ExchangeRateType, true);
            }
            
            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteStartElement("receiptitems");
            if (Lines.Count > 0)
            {
                foreach (OtherReceiptLineCreate line in Lines)
                {
                   line.WriteXml(ref xml);
                }
            }

            xml.WriteEndElement(); //receiptitems
            xml.WriteEndElement(); //record_otherreceipt

            xml.WriteEndElement(); //function
        }

    }
}