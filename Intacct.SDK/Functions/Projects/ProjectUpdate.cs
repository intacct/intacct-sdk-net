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

namespace Intacct.SDK.Functions.Projects
{
    public class ProjectUpdate : AbstractProject
    {

        public ProjectUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("PROJECT");

            xml.WriteElement("PROJECTID", ProjectId, true);

            xml.WriteElement("NAME", ProjectName);
            xml.WriteElement("PROJECTCATEGORY", ProjectCategory);
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("PARENTID", ParentProjectId);
            xml.WriteElement("INVOICEWITHPARENT", InvoiceWithParent);
            xml.WriteElement("PROJECTTYPE", ProjectType);
            xml.WriteElement("PROJECTSTATUS", ProjectStatus);
            xml.WriteElement("CUSTOMERID", CustomerId);
            xml.WriteElement("MANAGERID", ProjectManagerEmployeeId);
            xml.WriteElement("CUSTUSERID", ExternalUserId);
            xml.WriteElement("SALESCONTACTID", SalesContactEmployeeId);
            xml.WriteElement("DOCNUMBER", ReferenceNo);
            xml.WriteElement("USERRESTRICTIONS", UserRestrictions);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            if (!string.IsNullOrWhiteSpace(PrimaryContactName))
            {
                xml.WriteStartElement("CONTACTINFO");
                xml.WriteElement("CONTACTNAME", PrimaryContactName);
                xml.WriteEndElement(); //CONTACTINFO
            }
            if (!string.IsNullOrWhiteSpace(BillToContactName))
            {
                xml.WriteStartElement("BILLTO");
                xml.WriteElement("CONTACTNAME", BillToContactName);
                xml.WriteEndElement(); //BILLTO
            }
            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("SHIPTO");
                xml.WriteElement("CONTACTNAME", ShipToContactName);
                xml.WriteEndElement(); //SHIPTO
            }

            xml.WriteElement("TERMNAME", PaymentTerms);
            xml.WriteElement("BILLINGTYPE", BillingType);
            xml.WriteElement("BEGINDATE", BeginDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("LOCATIONID", LocationId);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            xml.WriteElement("BILLABLEEXPDEFAULT", BillableEmployeeExpense);
            xml.WriteElement("BILLABLEAPPODEFAULT", BillableApPurchasing);
            // xml.WriteElement("OBSPERCENTCOMPLETE", ObservedPercentComplete);
            xml.WriteElement("CURRENCY", Currency);
            xml.WriteElement("SONUMBER", SalesOrderNo);
            xml.WriteElement("PONUMBER", PurchaseOrderNo);
            xml.WriteElement("POAMOUNT", PurchaseOrderAmount);
            xml.WriteElement("PQNUMBER", PurchaseQuoteNo);
            xml.WriteElement("CONTRACTAMOUNT", ContractAmount);
            xml.WriteElement("BILLINGPRICING", LaborPricingOption);
            xml.WriteElement("BILLINGRATE", LaborPricingDefaultRate);
            xml.WriteElement("EXPENSEPRICING", ExpensePricingOption);
            xml.WriteElement("EXPENSERATE", ExpensePricingDefaultRate);
            xml.WriteElement("POAPPRICING", ApPurchasingPricingOption);
            xml.WriteElement("POAPRATE", ApPurchasingPricingDefaultRate);
            xml.WriteElement("BUDGETAMOUNT", BudgetedBillingAmount);
            xml.WriteElement("BUDGETEDCOST", BudgetedCost);
            xml.WriteElement("BUDGETQTY", BudgetedDuration);
            xml.WriteElement("BUDGETID", GlBudgetId);
            xml.WriteElement("INVOICEMESSAGE", InvoiceMessage);
            xml.WriteElement("INVOICECURRENCY", InvoiceCurrency);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //PROJECT
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}