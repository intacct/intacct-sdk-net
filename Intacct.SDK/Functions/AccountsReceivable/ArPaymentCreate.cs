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

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class ArPaymentCreate : AbstractArPayment
    {

        public ArPaymentCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_arpayment");
            
            xml.WriteElement("customerid", CustomerId, true);
            xml.WriteElement("paymentamount", TransactionPaymentAmount, true);
            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("translatedamount", BasePaymentAmount);

            if (!string.IsNullOrWhiteSpace(UndepositedFundsGlAccountNo))
            {
                xml.WriteElement("undepfundsacct", UndepositedFundsGlAccountNo);
            }
            else
            {
                xml.WriteElement("bankaccountid", BankAccountId);
            }

            xml.WriteElement("refid", ReferenceNumber);
            
            xml.WriteElement("overpaylocid", OverpaymentLocationId);
            xml.WriteElement("overpaydeptid", OverpaymentDepartmentId);
            
            xml.WriteStartElement("datereceived");
            xml.WriteDateSplitElements(ReceivedDate.Value, true);
            xml.WriteEndElement(); //datereceived

            xml.WriteElement("paymentmethod", PaymentMethod, true);

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
            
            xml.WriteElement("cctype", CreditCardType);
            xml.WriteElement("authcode", AuthorizationCode);
            
            if (ApplyToTransactions.Count > 0)
            {
                foreach (ArPaymentItem applyToTransaction in ApplyToTransactions)
                {
                    applyToTransaction.WriteXml(ref xml);
                }
            }

            // TODO online payment methods

            xml.WriteEndElement(); //create_arpayment

            xml.WriteEndElement(); //function
        }

    }
}