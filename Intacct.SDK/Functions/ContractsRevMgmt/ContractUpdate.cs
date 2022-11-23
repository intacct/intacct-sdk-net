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
    public class ContractUpdate : AbstractContract
    {

        public ContractUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("CONTRACT");

            xml.WriteElement("RECORDNO", RecordNo);
            xml.WriteElement("CONTRACTID", ContractId);

            xml.WriteElement("CUSTOMERID", CustomerId);
            xml.WriteElement("NAME", ContractName);

            xml.WriteElement("BILLTOCONTACTNAME", BillToContactName);
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("SHIPTOCONTACTNAME", ShipToContactName);
            
            xml.WriteElement("BEGINDATE", BeginDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat);
            
            xml.WriteElement("BILLINGFREQUENCY", BillingFrequency);
            xml.WriteElement("TERMNAME", PaymentTerm);
            
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
            xml.WriteElement("RENEWALMACRO", RenewalTemplate);
            xml.WriteElement("RENEWTERMLENGTH", RenewalTermLength);
            xml.WriteElement("RENEWTERMPERIOD", RenewalTermPeriod);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //CONTRACT
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}