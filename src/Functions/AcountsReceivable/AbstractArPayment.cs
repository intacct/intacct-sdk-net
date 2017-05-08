/*
 * Copyright 2017 Intacct Corporation.
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

using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Intacct.Sdk.Functions.AccountsReceivable
{

    abstract public class AbstractArPayment : AbstractFunction
    {

        public static string PaymentMethodCheck = "Printed Check";

        public static string PaymentMethodCash = "Cash";

        public static string PaymentMethodRecordTransfer = "EFT";

        public static string PaymentMethodCreditCard = "Credit Card";

        public static string PaymentMethodOnline = "Online";

        // TODO: public static string PaymentMethodOnlineCreditCard = "Online Charge Card";

        // TODO: public static string PaymentMethodOnlineAchDebit = "Online ACH Debit";

        public int? RecordNo;

        public string PaymentMethod;

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

        public AbstractArPayment(string controlId = null) : base(controlId)
        {
        }
    }
}