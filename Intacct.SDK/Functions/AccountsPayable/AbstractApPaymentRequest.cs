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

using System;
using System.Collections.Generic;

namespace Intacct.SDK.Functions.AccountsPayable
{
    public abstract class AbstractApPaymentRequest : AbstractFunction
    {

        public int? RecordNo;

        public string PaymentMethod;

        public string BankAccountId;

        public string ChargeCardId;

        public string VendorId;

        public string MergeOption;

        public bool? GroupPayments;

        public DateTime PaymentDate;

        public decimal TransactionAmount;

        public string DocumentNo;

        public string Memo;

        public string NotificationContactName;

        public List<ApPaymentRequestItem> ApplyToTransactions = new List<ApPaymentRequestItem>();

        protected AbstractApPaymentRequest(string controlId = null) : base(controlId)
        {
        }
    }
}