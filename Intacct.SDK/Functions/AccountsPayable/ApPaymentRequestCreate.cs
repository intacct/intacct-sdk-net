/*
 * Copyright 2019 Sage Intacct, Inc.
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
    public class ApPaymentRequestCreate : AbstractApPaymentRequest
    {

        public ApPaymentRequestCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);
           
            xml.WriteStartElement("create_paymentrequest");

            if (!string.IsNullOrWhiteSpace(ChargeCardId))
            {
                xml.WriteElement("chargecardid", ChargeCardId);
            }
            else
            {
                xml.WriteElement("bankaccountid", BankAccountId);
            }

            xml.WriteElement("vendorid", VendorId, true);

            xml.WriteElement("memo", Memo);

            xml.WriteElement("paymentmethod", PaymentMethod, true);

            xml.WriteElement("grouppayments", GroupPayments);

            xml.WriteStartElement("paymentdate");
            xml.WriteDateSplitElements(PaymentDate, true);
            xml.WriteEndElement(); //paymentdate

            xml.WriteElement("paymentoption", MergeOption);

            if (ApplyToTransactions.Count > 0)
            {
                xml.WriteStartElement("paymentrequestitems");
                foreach (ApPaymentRequestItem applyToTransaction in ApplyToTransactions)
                {
                    applyToTransaction.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //paymentrequestitems
            }

            xml.WriteElement("documentnumber", DocumentNo);
             
            // TODO: review paymentdescription vs memo
            xml.WriteElement("paymentdescription", Memo);

            xml.WriteElement("paymentcontact", NotificationContactName);

            xml.WriteEndElement(); //create_paymentrequest

            xml.WriteEndElement(); //function
        }

    }
}