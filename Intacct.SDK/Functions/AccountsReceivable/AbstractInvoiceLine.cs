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
using System.Collections.Generic;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public abstract class AbstractInvoiceLine : IXmlObject
    {

        public string AccountLabel;

        public string GlAccountNumber;

        public string OffsetGlAccountNumber;

        public decimal? TransactionAmount;

        public string AllocationId;

        public string Memo;

        public int? Key;

        public decimal? TotalPaid;

        public decimal? TotalDue;

        public string RevRecTemplateId;

        public string DeferredRevGlAccountNo;

        public DateTime? RevRecStartDate;

        public DateTime? RevRecEndDate;

        public string DepartmentId;

        public string LocationId;

        public string ProjectId;

        public string CustomerId;

        public string VendorId;

        public string EmployeeId;

        public string ItemId;

        public string ClassId;

        public string ContractId;

        public string WarehouseId;
        //public AbstractInvoiceLineTaxEntries TaxEntries ;
        //Made tax entries it as list because the API documentation showed multiple entries can be allowed for a line item  please see below comment
        /*taxentries	Optional	taxentry[1...n]	Tax entries for the line. Required for VAT enabled transactions. Providing multiple entries is allowed if your tax solution supports it (AU and GB only). For ZA, only one tax entry is allowed.
        */
        public List<AbstractInvoiceLineTaxEntries> Taxentry = new List<AbstractInvoiceLineTaxEntries>();
        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractInvoiceLine()
        {
        }
        
        public abstract void WriteXml(ref IaXmlWriter xml);
        
    }
}
