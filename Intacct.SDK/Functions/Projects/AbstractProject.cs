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

namespace Intacct.SDK.Functions.Projects
{
    public abstract class AbstractProject : AbstractFunction
    {

        public string ProjectId;

        public string ProjectName;

        public string ProjectCategory;

        public string Description;

        public string ParentProjectId;

        public bool? InvoiceWithParent;

        public string ProjectType;

        public string ProjectStatus;

        public string CustomerId;

        public string ProjectManagerEmployeeId;

        public string ExternalUserId;

        public string SalesContactEmployeeId;

        public string ReferenceNo;

        public string UserRestrictions;

        public bool? Active;

        public string PrimaryContactName;

        public string BillToContactName;

        public string ShipToContactName;

        public string PaymentTerms;

        public string BillingType;

        public DateTime? BeginDate;

        public DateTime? EndDate;

        public string DepartmentId;

        public string LocationId;

        public string ClassId;

        public string AttachmentsId;

        public bool? BillableEmployeeExpense;

        public bool? BillableApPurchasing;

        [ObsoleteAttribute("The ObservedPercentComplete property is deprecated in the API. Use ProjectObservedPercentCompleted classes instead.", false)]
        public decimal? ObservedPercentComplete;

        public string Currency;

        public string SalesOrderNo;

        public string PurchaseOrderNo;

        public decimal? PurchaseOrderAmount;

        public string PurchaseQuoteNo;

        public decimal? ContractAmount;

        public string LaborPricingOption;

        public decimal? LaborPricingDefaultRate;

        public string ExpensePricingOption;

        public decimal? ExpensePricingDefaultRate;

        public string ApPurchasingPricingOption;

        public decimal? ApPurchasingPricingDefaultRate;

        public decimal? BudgetedBillingAmount;

        public decimal? BudgetedCost;

        public decimal? BudgetedDuration;

        public string GlBudgetId;

        public string InvoiceMessage;

        public string InvoiceCurrency;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractProject(string controlId = null) : base(controlId)
        {
        }
        
    }
}