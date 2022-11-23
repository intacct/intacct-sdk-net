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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentDetailBillInfo : ApPaymentDetailInfo
    {
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
        /// Apply To Bill Discount Date.  Discount Date to use for a Bill (APBILL) or Credit Memo.
        /// </summary>
        public DateTime? ApplyToDiscountDate;
    }
}