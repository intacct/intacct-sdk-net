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

namespace Intacct.SDK.Functions.Company
{
    public class ContactCreate : AbstractContact
    {

        public ContactCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("CONTACT");

            xml.WriteElement("CONTACTNAME", ContactName, true);
            xml.WriteElement("PRINTAS", PrintAs, true);

            xml.WriteElement("COMPANYNAME", CompanyName);
            xml.WriteElement("TAXABLE", Taxable);
            xml.WriteElement("TAXID", TaxId);
            xml.WriteElement("TAXGROUP", ContactTaxGroupName);
            xml.WriteElement("PREFIX", Prefix);
            xml.WriteElement("FIRSTNAME", FirstName);
            xml.WriteElement("LASTNAME", LastName);
            xml.WriteElement("INITIAL", MiddleName);
            xml.WriteElement("PHONE1", PrimaryPhoneNo);
            xml.WriteElement("PHONE2", SecondaryPhoneNo);
            xml.WriteElement("CELLPHONE", CellularPhoneNo);
            xml.WriteElement("PAGER", PagerNo);
            xml.WriteElement("FAX", FaxNo);
            xml.WriteElement("EMAIL1", PrimaryEmailAddress);
            xml.WriteElement("EMAIL2", SecondaryEmailAddress);
            xml.WriteElement("URL1", PrimaryUrl);
            xml.WriteElement("URL2", SecondaryUrl);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            WriteXmlMailAddress(ref xml);
            
            xml.WriteEndElement(); //CONTACT
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}