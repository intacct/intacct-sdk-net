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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.InventoryControl
{
    public abstract class AbstractInventoryTransactionLine : IXmlObject
    {

        public string ItemId;

        public string ItemDescription;

        public string WarehouseId;

        public decimal? Quantity;

        public string Unit;

        public decimal? Cost;

        public List<AbstractTransactionItemDetail> ItemDetails = new List<AbstractTransactionItemDetail>();

        public string DepartmentId;

        public string LocationId;

        public string ProjectId;

        public string CustomerId;

        public string VendorId;

        public string EmployeeId;

        public string ClassId;

        public string ContractId;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractInventoryTransactionLine()
        {
        }
        
        public abstract void WriteXml(ref IaXmlWriter xml);
        
    }
}