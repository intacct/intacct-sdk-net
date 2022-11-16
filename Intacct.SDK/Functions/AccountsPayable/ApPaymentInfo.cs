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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentInfo
    {
        public string PaymentMethod;

        public string FinancialEntityId;

        public string VendorId;

        public string MergeOption;

        public bool? GroupPayments;

        public DateTime PaymentDate;
        
        public string BaseCurrency;
        
        public string TransactionCurrency;
        
        public decimal? TransactionAmountPaid;

        public DateTime? ExchangeRateDate;
        
        public string ExchangeRateType;
        
        public string DocumentNo;

        public string Memo;

        public string NotificationContactName;
        
        public string Action;

        public List<IApPaymentDetail> ApPaymentDetails = new List<IApPaymentDetail>();
    }
}