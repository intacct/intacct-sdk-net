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
    public class EmployeeUpdate : AbstractEmployee
    {

        public EmployeeUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("EMPLOYEE");
            
            xml.WriteElement("EMPLOYEEID", EmployeeId, true);

            if (!string.IsNullOrWhiteSpace(ContactName))
            {
                xml.WriteStartElement("PERSONALINFO");
                xml.WriteElement("CONTACTNAME", ContactName, true);
                xml.WriteEndElement(); //PERSONALINFO
            }

            xml.WriteElement("STARTDATE", StartDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("TITLE", Title);
            xml.WriteElement("SSN", Ssn);
            xml.WriteElement("EMPLOYEETYPE", EmployeeType);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteElement("BIRTHDATE", BirthDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat);

            xml.WriteElement("TERMINATIONTYPE", TerminationType);
            xml.WriteElement("SUPERVISORID", ManagerEmployeeId);
            xml.WriteElement("GENDER", Gender);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("LOCATIONID", LocationId);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteElement("CURRENCY", DefaultCurrency);
            xml.WriteElement("EARNINGTYPENAME", EarningTypeName);
            xml.WriteElement("POSTACTUALCOST", PostActualCost);
            xml.WriteElement("NAME1099", Form1099Name);
            xml.WriteElement("FORM1099TYPE", Form1099Type);
            xml.WriteElement("FORM1099BOX", Form1099Box);
            xml.WriteElement("SUPDOCFOLDERNAME", AttachmentFolderName);
            xml.WriteElement("PAYMETHODKEY", PreferredPaymentMethod);
            xml.WriteElement("MERGEPAYMENTREQ", MergePaymentRequests);
            xml.WriteElement("PAYMENTNOTIFY", SendAutomaticPaymentNotification);
            xml.WriteElement("ACHENABLED", AchEnabled);
            xml.WriteElement("ACHBANKROUTINGNUMBER", AchBankRoutingNo);
            xml.WriteElement("ACHACCOUNTNUMBER", AchBankAccountNo);
            xml.WriteElement("ACHACCOUNTTYPE", AchBankAccountType);
            xml.WriteElement("ACHREMITTANCETYPE", AchBankAccountClass);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //EMPLOYEE
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}