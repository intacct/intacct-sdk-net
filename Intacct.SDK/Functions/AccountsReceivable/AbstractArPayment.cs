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
using System.Collections.Generic;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public abstract class AbstractArPayment : AbstractFunction
    {
        public int? RecordNo;

        public string PaymentMethod;

        public int? SummaryRecordNo;
        
        public string BankAccountId;

        public string UndepositedFundsGlAccountNo;

        public string TransactionCurrency;

        public string BaseCurrency;

        public string CustomerId;

        public DateTime? ReceivedDate;

        public decimal? TransactionPaymentAmount;

        public decimal? BasePaymentAmount;

        public DateTime? ExchangeRateDate;

        public decimal? ExchangeRateValue;

        public string ExchangeRateType;

        public string CreditCardType;

        public string AuthorizationCode;

        public string OverpaymentLocationId;

        public string OverpaymentDepartmentId;

        public string ReferenceNumber;

        public List<ArPaymentItem> ApplyToTransactions = new List<ArPaymentItem>();

        protected AbstractArPayment(string controlId = null) : base(controlId)
        {
        }
    }
}