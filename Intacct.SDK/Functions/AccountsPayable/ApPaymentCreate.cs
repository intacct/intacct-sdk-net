/*
 * Copyright 2020 Sage Intacct, Inc.
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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentCreate : AbstractApPayment
    {
        public ApPaymentCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("APPYMT");

            xml.WriteElement("PAYMENTMETHOD", PaymentMethod, true);
            xml.WriteElement("FINANCIALENTITY", AccountId, true);
            xml.WriteElement("VENDORID", VendorId, true);

            xml.WriteElement("PAYMENTREQUESTMETHOD", MergeOption);
            xml.WriteElement("GROUPPAYMENTS", GroupPayments);

            if (ExchangeRateDate.HasValue)
            {
                xml.WriteElement("EXCH_RATE_DATE", ExchangeRateDate, IaXmlWriter.IntacctDateFormat);
            }

            xml.WriteElement("EXCH_RATE_TYPE_ID", ExchangeRateType);
            xml.WriteElement("DOCNUMBER", DocumentNo);
            xml.WriteElement("DESCRIPTION", Memo);
            xml.WriteElement("PAYMENTCONTACT", NotificationContactName);

            xml.WriteElement("PAYMENTDATE", PaymentDate, IaXmlWriter.IntacctDateFormat, true);

            xml.WriteElement("CURRENCY", TransactionCurrency);
            xml.WriteElement("BASECURR", BaseCurrency);
            xml.WriteElement("AMOUNTTOPAY", TransactionAmountPaid);
            xml.WriteElement("ACTION", Action);

            if (ApPaymentDetails.Count > 0)
            {
                xml.WriteStartElement("APPYMTDETAILS");
                foreach (IApPaymentDetail apPaymentDetail in ApPaymentDetails)
                {
                    apPaymentDetail.WriteXml(ref xml);
                }

                xml.WriteEndElement(); //APPYMTDETAILS
            }

            xml.WriteEndElement(); //APPYMT
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }
    }
}