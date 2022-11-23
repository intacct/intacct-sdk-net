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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentDetailInfo
    {
        /// <summary>
        /// This is the record number of a Bill (APBILL) or Credit Memo.
        /// </summary>
        public int? RecordNo { get; set; }
        
        /// <summary>
        /// This is the record number of a Bill or Credit Memo Line (APBILLITEM).  This must be an
        /// owned record of the BillRecordNo attribute.
        /// </summary>
        public int? LineRecordNo;

        /// <summary>
        /// Payment amount.  Enter an amount you want to pay.  Not required if applying an existing transaction.
        /// </summary>
        public decimal? PaymentAmount { get; set; }
        
        /// <summary>
        /// Use existing transaction.  Specify an existing transaction to apply against the ApplyToRecordNo.
        /// </summary>
        public AbstractApPaymentDetailCredit DetailTransaction;
    }
}