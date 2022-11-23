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
    public abstract class AbstractItem : AbstractFunction
    {

        public string ItemId;

        public bool? Active;

        public string ItemName;

        public string ProductLineId;

        public string CostMethod;

        public string ExtendedDescription;

        public string SalesDescription;

        public string PurchasingDescription;

        public string UnitOfMeasure;

        public string Note;

        public string ExpenseGlAccountNo;

        public string ArGlAccountNo;

        public string ApGlAccountNo;

        public string InventoryGlAccountNo;

        public decimal? ShippingWeight;

        public string ItemGlGroupName;

        public decimal? StandardCost;

        public string CogsGlAccountNo;

        public decimal? BasePrice;

        public string RevenueGlAccountNo;

        public bool? Taxable;

        public string ItemTaxGroupName;

        public string DeferredRevGlAccountNo;

        public string DefaultRevRecTemplateId;

        public string VsoeCategory;

        public string VsoeDefaultDeliveryStatus;

        public string VsoeDefaultDeferralStatus;

        public string KitRevenuePosting;

        public string KitPrintFormat;

        public string SubstituteItemId;

        public bool? SerialTrackingEnabled;

        public string SerialNumberMask;

        public bool? LotTrackingEnabled;

        public string LotCategory;

        public bool? BinTrackingEnabled;

        public bool? ExpTrackingEnabled;

        public string Upc;

        public int? UnitCostPrecisionInventory;

        public int? UnitCostPrecisionSales;

        public int? UnitCostPrecisionPurchasing;

        public bool? ItemStartEndDateEnabled;

        public string PeriodsMeasuredIn;

        public int? NumberOfPeriods;

        public bool? ProratePriceAllowed;

        public string DefaultRenewalMacroId;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractItem(string controlId = null) : base(controlId)
        {
        }

    }
}