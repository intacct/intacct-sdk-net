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
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Purchasing
{
    public abstract class AbstractPurchasingTransactionLine : IXmlObject
    {

        public string ItemId;

        public string ItemDescription;

        public bool? Taxable;

        public string WarehouseId;

        public decimal? Quantity;

        public string Unit;
        
        public string LineLevelSimpleTaxType;

        public decimal? Price;

        public decimal? OverrideTaxAmount;

        public decimal? Tax;

        public string Memo;

        public bool? Form1099;

        public string Form1099Type;

        public string Form1099Box;

        public bool? Billable;
        
        public string LineDeliverToContactName;

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

        protected AbstractPurchasingTransactionLine()
        {
        }
        
        public abstract void WriteXml(ref IaXmlWriter xml);
        
    }
}