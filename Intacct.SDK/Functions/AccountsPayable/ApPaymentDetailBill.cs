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
    public class ApPaymentDetailBill : IApPaymentDetail
    {
        private readonly ApPaymentDetailBillInfo _info;

        public ApPaymentDetailBill(ApPaymentDetailBillInfo info)
        {
            _info = info;
        }
        
        public void WriteXml(ref IaXmlWriter xml)
        {

            xml.WriteStartElement("APPYMTDETAIL");

            xml.WriteElement("RECORDKEY", _info.RecordNo, true);
            xml.WriteElement("ENTRYKEY", _info.LineRecordNo);

            if (_info.ApplyToDiscountDate.HasValue)
            {
                xml.WriteElement("DISCOUNTDATE", _info.ApplyToDiscountDate, IaXmlWriter.IntacctDateFormat);
            }
            else
            {
                xml.WriteElement("DISCOUNTTOAPPLY", _info.DiscountToApply);
            }

            if (_info.DetailTransaction != null)
            {
                _info.DetailTransaction.WriteXml(ref xml);
            }
            else
            {
                xml.WriteElement("CREDITTOAPPLY", _info.CreditToApply);
            }

            xml.WriteElement("TRX_PAYMENTAMOUNT", _info.PaymentAmount);
            
            xml.WriteEndElement(); //APPYMTDETAIL
        }
    }
}