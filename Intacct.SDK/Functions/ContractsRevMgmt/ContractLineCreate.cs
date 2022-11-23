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
    public class ContractLineCreate : AbstractContractLine
    {
        public ContractLineCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("CONTRACTDETAIL");

            xml.WriteElement("CONTRACTID", ContractId, true);
            xml.WriteElement("ITEMID", ItemId, true);
            
            xml.WriteElement("BEGINDATE", BeginDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat);
            
            xml.WriteElement("ITEMDESC", ItemDescription);
            xml.WriteElement("RENEWAL", Renewal);
            
            xml.WriteElement("EXCH_RATE_DATE", ExchangeRateDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("EXCHANGE_RATE", ExchangeRateValue);
            
            xml.WriteElement("BILLINGMETHOD", BillingMethod, true);

            if (BillingMethod == "Fixed price")
            {
                xml.WriteElement("BILLINGOPTIONS", FixedPriceFrequency);
                
                xml.WriteElement("QUANTITY", FixedPriceQuantity);
                xml.WriteElement("PRICE", FixedPriceRate);
                xml.WriteElement("MULTIPLIER", FixedPriceMultiplier);
                xml.WriteElement("DISCOUNTPERCENT", FixedPriceDiscountPercent);
                
                if (FixedPriceFrequency == "Use billing template")
                {
                    xml.WriteElement("BILLINGTEMPLATE", BillingTemplate);
                    xml.WriteElement("BILLINGSTARTDATE", BillingStartDate, IaXmlWriter.IntacctDateFormat);
                    xml.WriteElement("BILLINGENDDATE", BillingEndDate, IaXmlWriter.IntacctDateFormat);
                }
            }
            
            xml.WriteElement("FLATAMOUNT", FlatFixedAmount);
            
            xml.WriteElement("REVENUETEMPLATENAME", Revenue1Template);
            xml.WriteElement("REVENUESTARTDATE", Revenue1StartDate);
            xml.WriteElement("REVENUEENDDATE", Revenue1EndDate);
            
            xml.WriteElement("REVENUE2TEMPLATENAME", Revenue2Template);
            xml.WriteElement("REVENUE2STARTDATE", Revenue2StartDate);
            xml.WriteElement("REVENUE2ENDDATE", Revenue2EndDate);
            
            xml.WriteElement("SHIPTOCONTACTNAME", ShipToContactName);
            
            xml.WriteElement("LOCATIONID", LocationId);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("PROJECTID", ProjectId);
            xml.WriteElement("VENDORID", VendorId);
            xml.WriteElement("EMPLOYEEID", EmployeeId);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //CONTRACTDETAIL
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }
    }
}