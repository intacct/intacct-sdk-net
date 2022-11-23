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
    public abstract class AbstractContract : AbstractFunction
    {

        public int? RecordNo;

        public string ContractId;

        public string CustomerId;
        
        public string ContractName;
        
        public string BillToContactName;

        public string Description;
        
        public string ShipToContactName;
        
        public DateTime? BeginDate;
        
        public DateTime? EndDate;
        
        public string BillingFrequency;
        
        public string PaymentTerm;
        
        public string BillingPriceList;
        
        public string FairValuePriceList;
        
        public string AttachmentsId;
        
        public string LocationId;
        
        public string DepartmentId;
        
        public string ProjectId;
        
        public string VendorId;
        
        public string EmployeeId;
        
        public string ClassId;
        
        public string TransactionCurrency;
        
        public string ExchangeRateType;
        
        public bool? Renewal;
        
        public string RenewalTemplate;
        
        public string RenewalTermLength;
        
        public string RenewalTermPeriod;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractContract(string controlId = null) : base(controlId)
        {
        }

    }
}