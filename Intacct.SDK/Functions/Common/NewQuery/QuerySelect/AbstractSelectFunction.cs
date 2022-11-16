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

using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery.QuerySelect
{
    public abstract class AbstractSelectFunction : ISelect
    {
        public const string Average = "avg";

        public const string Count = "count";

        public const string Minimum = "min";

        public const string Maximum = "max";

        public const string Sum = "sum";
        
        private readonly string _fieldName;

        protected AbstractSelectFunction(string fieldName)
        {
            _fieldName = fieldName;
        }

        protected abstract string GetFunctionName();
    
        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteElement(GetFunctionName(), _fieldName);
        }
    }
}