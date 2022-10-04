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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public abstract class AbstractArAdvance : AbstractFunction
    {
        public int? RecordNo;
        
        public string CustomerId;

        public string PaymentMethod;

        public DateTime? PaymentDate;

        public DateTime? ReceiptDate;

        public string FinancialEntity;

        public string UndepositedAccountNo;

        public string PrBatch;

        public string DocNumber;

        public string Description;

        public string Currency;

        public string BaseCurrency;

        public DateTime? ExchangeRateDate;

        public string ExchangeRateTypeId;

        public decimal? ExchangeRate;

        public string AttachmentId;

        public string Action;

        public List<AbstractArAdvanceLine> Lines = new List<AbstractArAdvanceLine>();
        
        protected AbstractArAdvance(string controlId = null) : base(controlId)
        {
        }
    }
}