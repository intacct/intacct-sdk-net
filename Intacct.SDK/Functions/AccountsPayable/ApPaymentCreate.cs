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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentCreate : AbstractFunction
    {
        private readonly ApPaymentInfo _apPaymentInfo;

        public ApPaymentCreate(ApPaymentInfo apPaymentInfo, string controlId = null) : base(controlId)
        {
            _apPaymentInfo = apPaymentInfo;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("APPYMT");

            xml.WriteElement("PAYMENTMETHOD", _apPaymentInfo.PaymentMethod, true);
            xml.WriteElement("FINANCIALENTITY", _apPaymentInfo.FinancialEntityId, true);
            xml.WriteElement("VENDORID", _apPaymentInfo.VendorId, true);

            xml.WriteElement("PAYMENTREQUESTMETHOD", _apPaymentInfo.MergeOption);
            xml.WriteElement("GROUPPAYMENTS", _apPaymentInfo.GroupPayments);

            if (_apPaymentInfo.ExchangeRateDate.HasValue)
            {
                xml.WriteElement("EXCH_RATE_DATE", _apPaymentInfo.ExchangeRateDate, IaXmlWriter.IntacctDateFormat);
            }

            xml.WriteElement("EXCH_RATE_TYPE_ID", _apPaymentInfo.ExchangeRateType);
            xml.WriteElement("DOCNUMBER", _apPaymentInfo.DocumentNo);
            xml.WriteElement("DESCRIPTION", _apPaymentInfo.Memo);
            xml.WriteElement("PAYMENTCONTACT", _apPaymentInfo.NotificationContactName);

            xml.WriteElement("PAYMENTDATE", _apPaymentInfo.PaymentDate, IaXmlWriter.IntacctDateFormat, true);

            xml.WriteElement("CURRENCY", _apPaymentInfo.TransactionCurrency);
            xml.WriteElement("BASECURR", _apPaymentInfo.BaseCurrency);
            xml.WriteElement("AMOUNTTOPAY", _apPaymentInfo.TransactionAmountPaid);
            xml.WriteElement("ACTION", _apPaymentInfo.Action);

            if (_apPaymentInfo.ApPaymentDetails.Count > 0)
            {
                xml.WriteStartElement("APPYMTDETAILS");
                foreach (IApPaymentDetail apPaymentDetail in _apPaymentInfo.ApPaymentDetails)
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