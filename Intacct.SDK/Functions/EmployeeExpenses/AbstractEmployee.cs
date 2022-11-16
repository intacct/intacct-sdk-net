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

namespace Intacct.SDK.Functions.EmployeeExpenses
{
    public abstract class AbstractEmployee : AbstractFunction
    {

        public string EmployeeId;

        public string ContactName;

        public DateTime? StartDate;

        public string Title;

        public string Ssn;

        public string EmployeeType;

        public bool? Active;

        public bool? PlaceholderResource;

        public DateTime? BirthDate;

        public DateTime? EndDate;

        public string TerminationType;

        public string ManagerEmployeeId;

        public string Gender;

        public string DepartmentId;

        public string LocationId;

        public string ClassId;

        public string DefaultCurrency;

        public string EarningTypeName;

        public bool? PostActualCost;

        public string Form1099Name;

        public string Form1099Type;

        public string Form1099Box;

        public string AttachmentFolderName;

        public string PreferredPaymentMethod;

        public string SendAutomaticPaymentNotification;

        public bool? MergePaymentRequests;

        public bool? AchEnabled;

        public string AchBankRoutingNo;

        public string AchBankAccountNo;

        public string AchBankAccountType;

        public string AchBankAccountClass;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractEmployee(string controlId = null) : base(controlId)
        {
        }
        
    }
}