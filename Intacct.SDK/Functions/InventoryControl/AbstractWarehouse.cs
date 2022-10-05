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

using System.Collections.Generic;

namespace Intacct.SDK.Functions.InventoryControl
{
    public abstract class AbstractWarehouse : AbstractFunction
    {
        
        public int? RecordNo;

        public string WarehouseId;

        public string WarehouseName;

        public string LocationId;

        public string ManagerEmployeeId;

        public string ParentWarehouseId;

        public string WarehouseContactName;

        public string ShipToContactName;

        public bool? UsedInGeneralLedger;

        public bool? Active;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractWarehouse(string controlId = null) : base(controlId)
        {
        }

    }
}