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

namespace Intacct.SDK.Functions.ContractsRevMgmt
{
    public abstract class AbstractContractLine : AbstractFunction
    {
        public int? RecordNo;

        public string ContractId;
        
        public string ItemId;
        
        public DateTime? BeginDate;
        
        public DateTime? EndDate;
        
        public string ItemDescription;
        
        public bool? Renewal;
        
        public DateTime? ExchangeRateDate;
        
        public decimal? ExchangeRateValue;
        
        public string BillingMethod;
        
        public string FixedPriceFrequency;
        
        public string BillingTemplate;
        
        public DateTime? BillingStartDate;
        
        public DateTime? BillingEndDate;
        
        public decimal? FixedPriceQuantity;
        
        public decimal? FixedPriceRate;
        
        public decimal? FixedPriceMultiplier;
        
        public decimal? FixedPriceDiscountPercent;
        
        public decimal? FlatFixedAmount;

        public string Revenue1Template;

        public DateTime? Revenue1StartDate;
        
        public DateTime? Revenue1EndDate;

        public string Revenue2Template;
        
        public DateTime? Revenue2StartDate;
        
        public DateTime? Revenue2EndDate;

        public string ShipToContactName;

        public string LocationId;

        public string DepartmentId;

        public string ProjectId;

        public string VendorId;

        public string EmployeeId;

        public string ClassId;
        
        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractContractLine(string controlId = null) : base(controlId)
        {
        }
    }
}