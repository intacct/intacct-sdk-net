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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.OrderEntry
{
    public abstract class AbstractOrderEntryTransactionLine : IXmlObject
    {

        public string BundleNumber;

        public string ItemId;

        public string ItemDescription;

        public bool? Taxable;

        public string WarehouseId;

        public decimal? Quantity;

        public string Unit;
        
        public string LineLevelSimpleTaxType;

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
        
        public bool? DropShip;

        public string LineShipToContactName;

        public List<AbstractTransactionItemDetail> ItemDetails = new List<AbstractTransactionItemDetail>();

        public string DepartmentId;

        public string LocationId;

        public string ProjectId;

        public string CustomerId;

        public string VendorId;

        public string EmployeeId;

        public string ClassId;

        public string ContractId;

        public List<AbstractLineSubtotal> LineSubtotals = new List<AbstractLineSubtotal>();

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractOrderEntryTransactionLine()
        {
        }
        
        public abstract void WriteXml(ref IaXmlWriter xml);
        
    }
}