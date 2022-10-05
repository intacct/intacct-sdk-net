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

namespace Intacct.SDK.Functions.ContractsRevMgmt
{
    public class ContractCreate : AbstractContract
    {

        public ContractCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("CONTRACT");

            xml.WriteElement("CONTRACTID", ContractId);

            xml.WriteElement("CUSTOMERID", CustomerId, true);
            xml.WriteElement("NAME", ContractName, true);

            xml.WriteElement("BILLTOCONTACTNAME", BillToContactName);
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("SHIPTOCONTACTNAME", ShipToContactName);
            
            xml.WriteElement("BEGINDATE", BeginDate, IaXmlWriter.IntacctDateFormat, true);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat, true);
            
            xml.WriteElement("BILLINGFREQUENCY", BillingFrequency, true);
            xml.WriteElement("TERMNAME", PaymentTerm, true);
            
            xml.WriteElement("PRCLIST", BillingPriceList);
            xml.WriteElement("MEAPRCLIST", FairValuePriceList);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            xml.WriteElement("LOCATIONID", LocationId);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("PROJECTID", ProjectId);
            xml.WriteElement("VENDORID", VendorId);
            xml.WriteElement("EMPLOYEEID", EmployeeId);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteElement("CURRENCY", TransactionCurrency);
            xml.WriteElement("EXCHRATETYPE", ExchangeRateType);
            
            xml.WriteElement("RENEWAL", Renewal);
            if (Renewal == true)
            {
                xml.WriteElement("RENEWALMACRO", RenewalTemplate);
                xml.WriteElement("RENEWTERMLENGTH", RenewalTermLength);
                xml.WriteElement("RENEWTERMPERIOD", RenewalTermPeriod);
            }

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //CONTRACT
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}