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

namespace Intacct.SDK.Functions.CashManagement
{
    public abstract class AbstractOtherReceipt : AbstractFunction
    {

        public DateTime ReceiptDate;
        
        public string Payer;
        
        public string PaymentMethod;

        public DateTime TransactionDate;
        
        public string TransactionNo;
        
        public string Description;

        public string AttachmentsId;

        public string BankAccountId;
        
        public DateTime? DepositDate;

        public string UndepositedFundsGlAccountNo;

        public string TransactionCurrency;

        public DateTime? ExchangeRateDate;

        public decimal? ExchangeRateValue;

        public string ExchangeRateType;

        public List<AbstractOtherReceiptLine> Lines = new List<AbstractOtherReceiptLine>();

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractOtherReceipt(string controlId = null) : base(controlId)
        {
        }
    }
}