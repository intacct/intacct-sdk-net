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
    public abstract class AbstractChargeCardTransaction : AbstractFunction
    {

        public int? RecordNo;

        public string ChargeCardId;
            
        public DateTime? TransactionDate;
        
        public string ReferenceNumber;
        
        public string Payee;
        
        public string Description;
        
        public string AttachmentsId;
        
        public string TransactionCurrency;
        
        public DateTime? ExchangeRateDate;
        
        public decimal? ExchangeRateValue;
        
        public string ExchangeRateType;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();
        
        public List<AbstractChargeCardTransactionLine> Lines = new List<AbstractChargeCardTransactionLine>();

        protected AbstractChargeCardTransaction(string controlId = null) : base(controlId)
        {
        }
    }
}