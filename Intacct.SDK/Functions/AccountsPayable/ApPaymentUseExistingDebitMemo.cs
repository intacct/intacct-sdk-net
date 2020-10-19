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
    public class ApPaymentUseExistingDebitMemo : IApPaymentUseExistingTransaction
    {
        /// <summary>
        /// Use existing Record ID.  This is the record number of a Debit Memo (APADJUSTMENT).
        /// </summary>
        public int? ExistingRecordId { get; set; }
        
        /// <summary>
        /// Use existing Record Line ID.  This is the record number of a Debit Memo Line (APADJUSTMENTITEM).
        /// This must be an owned record of the ExistingRecordId attribute.
        /// </summary>
        public int? ExistingRecordLineId;

        /// <summary>
        /// Existing amount to use.  Amount you want to use to apply against the related Bill/Credit Memo.
        /// </summary>
        public decimal? ExistingAmount;
        
        
        public ApPaymentUseExistingDebitMemo()
        {
        }
        
        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteElement("ADJUSTMENTKEY", ExistingRecordId, true);
            xml.WriteElement("ADJUSTMENTENTRYKEY", ExistingRecordLineId);
            xml.WriteElement("TRX_ADJUSTMENTAMOUNT", ExistingAmount);
        }
    }
}