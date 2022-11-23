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
    public class ExpenseAdjustmentCreate : AbstractExpenseAdjustment
    {

        public ExpenseAdjustmentCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_expenseadjustmentreport");
            
            xml.WriteElement("employeeid", EmployeeId, true);

            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate);
            xml.WriteEndElement(); //datecreated

            if (GlPostingDate.HasValue)
            {
                xml.WriteStartElement("dateposted");
                xml.WriteDateSplitElements(GlPostingDate.Value);
                xml.WriteEndElement(); //dateposted
            }

            xml.WriteElement("batchkey", SummaryRecordNo);
            xml.WriteElement("adjustmentno", ExpenseAdjustmentNumber);
            xml.WriteElement("docnumber", ExpenseReportNumber);
            xml.WriteElement("description", Description);
            xml.WriteElement("basecurr", BaseCurrency);
            xml.WriteElement("currency", ReimbursementCurrency);

            // Current schema does not allow custom fields
            //xml.WriteCustomFieldsExplicit(CustomFields);
            
            xml.WriteStartElement("expenseadjustments");
            if (Lines.Count > 0)
            {
                foreach (ExpenseAdjustmentLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //expenseadjustments

            xml.WriteElement("supdocid", AttachmentsId);

            xml.WriteEndElement(); //create_expenseadjustmentreport

            xml.WriteEndElement(); //function
        }

    }
}