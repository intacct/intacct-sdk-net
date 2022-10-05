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

namespace Intacct.SDK.Functions.Common.Query.Comparison
{
    public abstract class AbstractDateTimeClass : AbstractComparison
    {
        public const string IaDateFormat = "MM/dd/yyyy";
        
        public const string IaDateTimeFormat = "MM/dd/yyyy HH:mm:ss";
        
        public DateTime Value;

        public string Format;

        protected AbstractDateTimeClass(string format)
        {
            Format = format;
        }
    }
}