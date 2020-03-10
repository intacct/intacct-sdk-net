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
    public class ApPaymentRequestApplyToCreditMemo : IApPaymentRequestApplyToTransaction
    {
       
        /// <summary>
        /// Apply to Record ID.  This is the record number of a Credit Memo (APADJUSTMENT).
        /// </summary>
        public int? ApplyToRecordId { get; set; }
        
        /// <summary>
        /// Apply to Record Line ID.  This is the record number of a Credit Memo Line (APADJUSTMENTITEM).  This must be
        /// an owned record of the ApplyToRecordId attribute.
        /// </summary>
        public int? ApplyToRecordLineId;

        /// <summary>
        /// Payment amount.  Enter an amount you want to pay.  Not required if applying an existing transaction.
        /// </summary>
        public decimal? PaymentAmount;
        
        /// <summary>
        /// Use existing transaction.  Specify an existing transaction to apply against the ApplyToRecordNo.
        /// </summary>
        public IApPaymentRequestUseExistingTransaction UseExistingTransaction;

        public ApPaymentRequestApplyToCreditMemo()
        {
        }

        public void WriteXml(ref IaXmlWriter xml)
        {

            xml.WriteStartElement("APPYMTDETAIL");

            xml.WriteElement("POSADJKEY", ApplyToRecordId, true);
            xml.WriteElement("POSADJENTRYKEY", ApplyToRecordLineId);

            if (UseExistingTransaction != null)
            {
                UseExistingTransaction.WriteXml(ref xml);
            }

            xml.WriteElement("TRX_PAYMENTAMOUNT", PaymentAmount);
            
            xml.WriteEndElement(); //APPYMTDETAIL
        }
    }
}