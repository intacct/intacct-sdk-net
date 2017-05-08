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

using Intacct.Sdk.Functions.InventoryControl;
using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Intacct.Sdk.Functions.OrderEntry
{

    abstract public class AbstractOrderEntryTransactionLine
    {

        public string BundleNumber;

        public string ItemId;

        public string ItemDescription;

        public bool? Taxable;

        public string WarehouseId;

        public decimal? Quantity;

        public string Unit;

        public decimal? DiscountPercent;

        public decimal? Price;

        public string DiscountSurchargeMemo;

        public string Memo;

        public string RevRecTemplate;

        public DateTime? RevRecStartDate;

        public DateTime? RevRecEndDate;

        public string RenewalMacro;

        public string FulfillmentStatus;

        public string TaskNumber;

        public string BillingTemplate;

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

        public AbstractOrderEntryTransactionLine()
        {
        }
        
        abstract public void WriteXml(ref IaXmlWriter xml);
        
    }
}