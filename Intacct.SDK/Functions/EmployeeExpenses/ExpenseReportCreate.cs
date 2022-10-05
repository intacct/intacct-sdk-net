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

namespace Intacct.SDK.Functions.EmployeeExpenses
{
    public class ExpenseReportCreate : AbstractExpenseReport
    {

        public ExpenseReportCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_expensereport");
            
            xml.WriteElement("employeeid", EmployeeId, true);

            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate.Value);
            xml.WriteEndElement(); //datecreated

            if (GlPostingDate.HasValue)
            {
                xml.WriteStartElement("dateposted");
                xml.WriteDateSplitElements(GlPostingDate.Value);
                xml.WriteEndElement(); //dateposted
            }

            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("expensereportno", ExpenseReportNumber);
            xml.WriteElement("state", Action);
            xml.WriteElement("description", ReasonForExpense);
            xml.WriteElement("memo", Memo);
            xml.WriteElement("externalid", ExternalId);
            xml.WriteElement("basecurr", BaseCurrency);
            xml.WriteElement("currency", ReimbursementCurrency);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("supdocid", AttachmentsId);
            
            xml.WriteStartElement("expenses");
            if (Lines.Count > 0)
            {
                foreach (ExpenseReportLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //expenses

            xml.WriteEndElement(); //create_expensereport

            xml.WriteEndElement(); //function
        }

    }
}