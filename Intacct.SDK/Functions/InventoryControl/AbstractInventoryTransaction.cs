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

namespace Intacct.SDK.Functions.InventoryControl
{
    public abstract class AbstractInventoryTransaction : AbstractFunction
    {

        public string DocumentId;

        public string TransactionDefinition;

        public DateTime? TransactionDate;

        public string CreatedFrom;
        
        public string DocumentNumber;

        public string ReferenceNumber;

        public string Message;

        public string ExternalId;
        
        public string BaseCurrency;

        public string State;

        public List<AbstractTransactionSubtotal> Subtotals = new List<AbstractTransactionSubtotal>();

        public List<AbstractInventoryTransactionLine> Lines = new List<AbstractInventoryTransactionLine>();

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractInventoryTransaction(string controlId = null) : base(controlId)
        {
        }
        
    }
}