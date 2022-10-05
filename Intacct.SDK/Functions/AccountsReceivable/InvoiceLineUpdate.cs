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

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class InvoiceLineUpdate : AbstractInvoiceLine
    {
        
        private int _lineNo;
        
        public int LineNo
        {
            get => _lineNo;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Line No must be greater than zero");
                }

                _lineNo = value;
            }
        }


        public InvoiceLineUpdate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("updatelineitem");
            xml.WriteAttribute("line_num", LineNo);

            if (!string.IsNullOrWhiteSpace(AccountLabel))
            {
                xml.WriteElement("accountlabel", AccountLabel, true);
            }
            else if (!string.IsNullOrWhiteSpace(GlAccountNumber))
            {
                xml.WriteElement("glaccountno", GlAccountNumber, true);
            }

            xml.WriteElement("offsetglaccountno", OffsetGlAccountNumber);

            if (TransactionAmount.HasValue)
            {
                xml.WriteElement("amount", TransactionAmount);
            }
            xml.WriteElement("allocationid", AllocationId);
            xml.WriteElement("memo", Memo);
            xml.WriteElement("locationid", LocationId);
            xml.WriteElement("departmentid", DepartmentId);
            xml.WriteElement("key", Key);
            xml.WriteElement("totalpaid", TotalPaid);
            xml.WriteElement("totaldue", TotalDue);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("revrectemplate", RevRecTemplateId);
            xml.WriteElement("defrevaccount", DeferredRevGlAccountNo);
            if (RevRecStartDate.HasValue)
            {
                xml.WriteStartElement("revrecstartdate");
                xml.WriteDateSplitElements(RevRecStartDate.Value);
                xml.WriteEndElement(); //revrecstartdate
            }
            if (RevRecEndDate.HasValue)
            {
                xml.WriteStartElement("revrecenddate");
                xml.WriteDateSplitElements(RevRecEndDate.Value);
                xml.WriteEndElement(); //revrecenddate
            }
            xml.WriteElement("projectid", ProjectId);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("vendorid", VendorId);
            xml.WriteElement("employeeid", EmployeeId);
            xml.WriteElement("itemid", ItemId);
            xml.WriteElement("classid", ClassId);
            xml.WriteElement("contractid", ContractId);
            xml.WriteElement("warehouseid", WarehouseId);

            xml.WriteEndElement(); //updatelineitem
        }
    }
}