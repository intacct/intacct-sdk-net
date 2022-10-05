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

using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsPayable
{
    public abstract class AbstractApPaymentDetailCredit
    {
        public const string DebitMemo = "debit memo";

        public const string NegativeBill = "negative bill";

        public const string Advance = "advance";
        
        public int? RecordNo{ get; set; }
        
        public int? LineRecordNo;

        public decimal? TransactionAmount;

        protected abstract string GetKeyType();

        protected abstract string GetEntryKeyType();

        protected abstract string GetTransactionType();
        
        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteElement(GetKeyType(), RecordNo, true);
            xml.WriteElement(GetEntryKeyType(), LineRecordNo);
            xml.WriteElement(GetTransactionType(), TransactionAmount);
        }
    }
}