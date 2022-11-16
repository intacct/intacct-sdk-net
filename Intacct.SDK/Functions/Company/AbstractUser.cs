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

namespace Intacct.SDK.Functions.Company
{
    public abstract class AbstractUser : AbstractFunction
    {

        public string UserId;

        public string UserName;

        public string UserType;
        
        public bool? Active;

        public bool? WebServicesOnly;

        public List<string> RestrictedEntities = new List<string>();

        public List<string> RestrictedDepartments = new List<string>();

        public bool? SsoEnabled;

        public string SsoFederatedId;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractUser(string controlId = null) : base(controlId)
        {
        }
        
    }
}