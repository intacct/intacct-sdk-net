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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery.QuerySelect
{
    public class Field : ISelect 
    {
        private string FieldName { get; set; }

        public Field(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException(
                    "Field name cannot be empty or null. Provide a field name for the builder.");
            }

            this.FieldName = fieldName;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteElement("field", FieldName, false);
        }
    }
}