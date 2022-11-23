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
using System;
using System.Collections.Generic;

namespace Intacct.SDK.Functions.Common.List
{
    public class GetList : AbstractFunction
    {
        public int start;
        public int maxitems;
        public bool showprivate;
        public string ObjectName;

        public List<string> Fields = new List<string>();
        public List<SortedField> SortedFields = new List<SortedField>();

        public ExpressionFilter Expression;
        public LogicalFilter Logical;

        public GetList(string objectName)
        {
            base.ControlId = Guid.NewGuid().ToString();
            this.start = 0;
            this.maxitems = 100;
            this.showprivate = true;
            this.ObjectName = objectName;
        }

        public GetList(string objectName, string controlId)
        {
            base.ControlId = controlId;

            this.start = 0;
            this.maxitems = 100;
            this.showprivate = true;
            this.ObjectName = objectName;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {

            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("get_list");
            xml.WriteAttributeString("object", this.ObjectName);
            xml.WriteAttributeString("start", this.start.ToString());
            xml.WriteAttributeString("maxitems", this.maxitems.ToString());
            xml.WriteAttributeString("showprivate", this.showprivate.ToString().ToLower());


            if (Expression != null || Logical != null)
            {
                xml.WriteStartElement("filter");

                if (this.Expression != null)
                {
                    WriteExpressions(Expression, ref xml);
                }
                else // If Expression is null, then Logical is required
                {
                    xml.WriteStartElement("logical");
                    xml.WriteAttribute("logical_operator", Logical.LogicalOperator);
                
                    if (Logical.ExpressionFilterList.Count > 0)
                    {
                        WriteExpressionLists(Logical.ExpressionFilterList, ref xml);
                    }

                    if (Logical.LogicalFilterList.Count > 0)
                    {
                        WriteLogicalLists(Logical.LogicalFilterList, ref xml);
                    }
                    xml.WriteEndElement(); // logical
                }

                xml.WriteEndElement(); // </filter>
            }

            if (this.SortedFields.Count > 0)
            {
                xml.WriteStartElement("sorts");

                foreach (SortedField SortedField in this.SortedFields)
                {
                    xml.WriteStartElement("sortfield");
                    xml.WriteAttributeString("order", SortedField.Order);
                    xml.WriteString(SortedField.Name);
                    xml.WriteEndElement(); // </sortfield>
                }

                xml.WriteEndElement(); // </sorts>
            }

            if (this.Fields.Count > 0)
            {
                xml.WriteStartElement("fields");

                foreach (string Field in this.Fields)
                {
                    xml.WriteElementString("field", Field);
                }

                xml.WriteEndElement(); // </fields>
            }

            xml.WriteEndElement(); // </get_list> 

            xml.WriteEndElement(); // </function> 

        }
        
        /*
         * This function takes a LogicalFilter list and writes out the Expressions associated with it 
         */
        public void WriteLogicalLists(List<LogicalFilter> list, ref IaXmlWriter xml) {
            foreach (var logicalFilter in list)
            {
                xml.WriteStartElement("logical");
                xml.WriteAttribute("logical_operator", logicalFilter.LogicalOperator);
                if (logicalFilter.ExpressionFilterList.Count > 0)
                {
                    WriteExpressionLists(logicalFilter.ExpressionFilterList, ref xml);
                }

                xml.WriteEndElement(); // logical
            }
        }

        /*
         * This function takes one or more expressions from a list and sends them to be written as xml
         */
        public void WriteExpressionLists(List<ExpressionFilter> expressionList, ref IaXmlWriter xml)
        {
            foreach (var expression in expressionList)
            {
                WriteExpressions(expression, ref xml);
            }
        }
        
        /*
         * This function writes expressions as xml
         */
        public void WriteExpressions(ExpressionFilter expression, ref IaXmlWriter xml)
        {
            xml.WriteStartElement("expression");
            xml.WriteElementString("field", expression.Field);
            xml.WriteElementString("operator", expression.Operator);
            xml.WriteElementString("value", expression.Value);
            xml.WriteEndElement(); // </expression>
        }
    }
}
