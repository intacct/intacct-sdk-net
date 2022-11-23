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
using Intacct.SDK.Functions.InventoryControl;

namespace Intacct.SDK.Functions.OrderEntry
{
    public abstract class AbstractRecurringOrderEntryTransaction : AbstractFunction
    {

        public string DocumentId;

        public string TransactionDefinition;

        public string CustomerId;

        public string ReferenceNumber;

        public string PaymentTerm;

        public string Message;

        public string ContractId;

        public string ContractDescription;

        public string CustomerPoNumber;

        public string ShippingMethod;
        
        public string ShipToContactName;

        public string BillToContactName;

        public string ExternalId;
        
        public string BaseCurrency;

        public string TransactionCurrency;
        
        public string ExchangeRateType;

        public string TaxSolutionId;

        public string PayMethod;

        public bool? IsPayInFull;

        public decimal? PaymentAmount;

        public string CreditCardType;

        public int? CustomerCreditCardKey;

        public int? CustomerBankAccountKey;

        public string AccountType;

        public string BankAccountId;

        public string GlAccountKey;

        public DateTime? StartDate;

        public int? NumberOfOccurrences;

        public DateTime? EndDate;

        public string RepeatMode;

        public int? RepeatInterval;

        public bool? IsEndOfMonth;

        public string Status;

        public string DocStatus;

        public List<AbstractTransactionSubtotal> Subtotals = new List<AbstractTransactionSubtotal>();

        public List<AbstractRecurringOrderEntryTransactionLine> Lines = new List<AbstractRecurringOrderEntryTransactionLine>();
        
        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();
        
        protected AbstractRecurringOrderEntryTransaction(string controlId = null) : base(controlId)
        {
        }

    }
}