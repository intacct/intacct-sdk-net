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

namespace Intacct.Sdk.Functions.Common.LegacyGetList
{

    public class ExpressionFilter : FilterInterface
    {
        public const string OperatorEqualTo = "=";
        
        public const string OperatorNotEqualTo = "!=";
        
        public const string OperatorLessThan = "<";
        
        public const string OperatorLessThanOrEqualTo = "<=";
        
        public const string OperatorGreaterThan = ">";
        
        public const string OperatorGreaterThanOrEqualTo = ">=";
        
        public const string OperatorLike = "like";
        
        public const string OperatorNotLike = "not like";
        
        public const string OperatorIsNull = "is null";
        
        public string FieldName;
        
        public string Operator;
        
        public string Value;
        
        public string ObjectName;
            
        public ExpressionFilter()
        {
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("expression");
            
            if (!String.IsNullOrWhiteSpace(ObjectName))
            {
                xml.WriteAttribute("object", ObjectName);
            }

            xml.WriteElement("field", FieldName, true);
            xml.WriteElement("operator", Operator, true);
            xml.WriteElement("value", Value, true);

            xml.WriteEndElement(); //expression
        }

    }

}