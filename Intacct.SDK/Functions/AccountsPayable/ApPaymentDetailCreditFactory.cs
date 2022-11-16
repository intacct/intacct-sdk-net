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
    public class ApPaymentDetailCreditFactory
    {
        public AbstractApPaymentDetailCredit Create(string type, int? recordNo, int? lineRecordNo,
            decimal? transactionAmount)
        {
            AbstractApPaymentDetailCredit detailCredit;

            switch (type)
            {
                case AbstractApPaymentDetailCredit.NegativeBill:
                    detailCredit = new ApPaymentDetailNegativeBill();
                    break;
                case AbstractApPaymentDetailCredit.DebitMemo:
                    detailCredit = new ApPaymentDetailDebitMemo();
                    break;
                case AbstractApPaymentDetailCredit.Advance:
                    detailCredit = new ApPaymentDetailAdvance();
                    break;
                default:
                    throw new ArgumentException("Credit " + type + " doesn't exist.");
            }

            detailCredit.RecordNo = recordNo;
            detailCredit.LineRecordNo = lineRecordNo;
            detailCredit.TransactionAmount = transactionAmount;

            return detailCredit;
        }
    }
}