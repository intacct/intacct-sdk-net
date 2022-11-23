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

namespace Intacct.SDK.Functions.GeneralLedger
{
    public abstract class AbstractGlAccount : AbstractFunction
    {

        public string AccountNo;

        public string Title;

        public string SystemCategory;

        public bool? Active;

        public bool? RequireDepartment;

        public bool? RequireLocation;

        public bool? RequireProject;

        public bool? RequireCustomer;

        public bool? RequireVendor;

        public bool? RequireEmployee;

        public bool? RequireItem;

        public bool? RequireClass;

        public bool? RequireContract;

        public bool? RequireWarehouse;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractGlAccount(string controlId = null) : base(controlId)
        {
        }

    }
}