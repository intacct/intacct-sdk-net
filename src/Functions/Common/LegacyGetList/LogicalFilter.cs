/*
 * Copyright 2017 Intacct Corporation.
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

using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;

namespace Intacct.Sdk.Functions.Common.LegacyGetList
{

    public class LogicalFilter : FilterInterface
    {
        public const string OperatorAnd = "and";

        public const string OperatorOr = "or";

        public List<FilterInterface> Filters = new List<FilterInterface>();
        
        public string Operator;

        public string ObjectName;

        public LogicalFilter()
        {
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("logical");

            xml.WriteAttribute("logical_operator", Operator);

            if (!String.IsNullOrWhiteSpace(ObjectName))
            {
                xml.WriteElement("object", ObjectName);
            }
            
            if (Filters.Count > 0)
            {
                foreach (FilterInterface Filter in Filters)
                {
                    Filter.WriteXml(ref xml);
                }
            }
            
            xml.WriteEndElement(); //logical
        }

    }

}