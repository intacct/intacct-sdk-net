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
    public abstract class AbstractWarehouseTransfer : AbstractFunction
    {
        public int? RecordNumber;
        
        public DateTime TransactionDate;

        public string ReferenceNumber;
        
        public string Description;

        public string TransferType;

        public string Action;

        public DateTime? OutDate;

        public DateTime? InDate;

        public decimal? ExchangeRate;

        public string ExchangeRateTypeId;

        public DateTime? ExchangeRateDate;

        public List<AbstractWarehouseTransferLine> Lines = new List<AbstractWarehouseTransferLine>();
        
        protected AbstractWarehouseTransfer(string controlId = null) : base(controlId)
        {
        }
    }
}