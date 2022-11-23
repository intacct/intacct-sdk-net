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

namespace Intacct.SDK.Functions.GeneralLedger
{
    public class AccountUpdate : AbstractAccount
    {

        public AccountUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("GLACCOUNT");

            xml.WriteElement("ACCOUNTNO", AccountNo, true);

            xml.WriteElement("TITLE", Title);
            xml.WriteElement("ACCOUNTTYPE", AccountType);
            xml.WriteElement("NORMALBALANCE", NormalBalance);
            xml.WriteElement("CLOSINGTYPE", ClosingType);
            xml.WriteElement("CLOSINGACCOUNTNO", CloseIntoGlAccountNo);
            xml.WriteElement("CATEGORY", SystemCategory);
            xml.WriteElement("TAXCODE", TaxReturnCode);
            xml.WriteElement("MRCCODE", M3ReturnCode);
            
            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteElement("TAXABLE", Taxable);
            xml.WriteElement("REQUIREDEPT", RequireDepartment);
            xml.WriteElement("REQUIRELOC", RequireLocation);
            xml.WriteElement("REQUIREPROJECT", RequireProject);
            xml.WriteElement("REQUIRECUSTOMER", RequireCustomer);
            xml.WriteElement("REQUIREVENDOR", RequireVendor);
            xml.WriteElement("REQUIREEMPLOYEE", RequireEmployee);
            xml.WriteElement("REQUIREITEM", RequireItem);
            xml.WriteElement("REQUIRECLASS", RequireClass);
            xml.WriteElement("REQUIRECONTRACT", RequireContract);
            xml.WriteElement("REQUIREWAREHOUSE", RequireWarehouse);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //GLACCOUNT
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}