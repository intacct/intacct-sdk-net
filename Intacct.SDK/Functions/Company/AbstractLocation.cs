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

namespace Intacct.SDK.Functions.Company
{
    public abstract class AbstractLocation : AbstractFunction
    {

        public string LocationId;

        public string LocationName;

        public string ParentLocationId;

        public string ManagerEmployeeId;

        public string LocationContactName;

        public string ShipToContactName;

        public DateTime? StartDate;

        public DateTime? EndDate;

        public string LocationTitle;

        public bool? Active;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractLocation(string controlId = null) : base(controlId)
        {
        }
        
    }
}