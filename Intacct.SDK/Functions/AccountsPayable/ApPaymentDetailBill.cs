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

using System;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentDetailBill : AbstractApPaymentDetail
    {
       
        /// <summary>
        /// This is the record number of a Bill (APBILL).
        /// </summary>
        public int? BillRecordNo { get; set; }
        
        /// <summary>
        /// This is the record number of a Bill Line (APBILLITEM).  This must be an
        /// owned record of the BillRecordNo attribute.
        /// </summary>
        public int? BillLineRecordNo;

        /// <summary>
        /// Credit to apply.  Use this to have the system select available credits to use.  Do not use this if you are 
        /// applying an existing transaction.
        /// </summary>
        public decimal? CreditToApply;

        /// <summary>
        /// Discount to apply.  Do not use this if you use the ApplyToDiscountDate attribute or if you are applying an
        /// existing transaction.
        /// </summary>
        public decimal? DiscountToApply;

        /// <summary>
        /// Apply To Bill Discount Date.  Discount Date to use for a Bill (APBILL).
        /// </summary>
        public DateTime? ApplyToDiscountDate;

        /// <summary>
        /// Use existing transaction.  Specify an existing transaction to apply against the ApplyToRecordNo.
        /// </summary>
        public IApPaymentDetailTransaction DetailTransaction;

        public override void WriteXml(ref IaXmlWriter xml)
        {

            xml.WriteStartElement("APPYMTDETAIL");

            xml.WriteElement("RECORDKEY", BillRecordNo, true);
            xml.WriteElement("ENTRYKEY", BillLineRecordNo);

            if (ApplyToDiscountDate.HasValue)
            {
                xml.WriteElement("DISCOUNTDATE", ApplyToDiscountDate, IaXmlWriter.IntacctDateFormat);
            }
            else
            {
                xml.WriteElement("DISCOUNTTOAPPLY", DiscountToApply);
            }

            if (DetailTransaction != null)
            {
                DetailTransaction.WriteXml(ref xml);
            }
            else
            {
                xml.WriteElement("CREDITTOAPPLY", CreditToApply);
            }

            xml.WriteElement("TRX_PAYMENTAMOUNT", PaymentAmount);
            
            xml.WriteEndElement(); //APPYMTDETAIL
        }
    }
}