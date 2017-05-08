/*
 * Copyright 2017 Intacct Corporation.
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

using Intacct.Sdk.Xml;
using System;

namespace Intacct.Sdk.Functions.AccountsReceivable.PaymentServices
{

    public class CustomerChargeCardUpdate : AbstractCustomerChargeCard
    {

        public CustomerChargeCardUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update_customerchargecard");

            xml.WriteAttribute("recordno", RecordNo, true);
            
            xml.WriteElement("exp_month", ExpirationMonth);
            xml.WriteElement("exp_year", ExpirationYear);

            xml.WriteElement("description", Description);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            // Are any of these set with data?
            if (
                !String.IsNullOrWhiteSpace(AddressLine1)
                || !String.IsNullOrWhiteSpace(AddressLine2)
                || !String.IsNullOrWhiteSpace(City)
                || !String.IsNullOrWhiteSpace(StateProvince)
                || !String.IsNullOrWhiteSpace(ZipPostalCode)
                || !String.IsNullOrWhiteSpace(Country)
            ) {
                xml.WriteStartElement("mailaddress");
                xml.WriteElement("address1", AddressLine1);
                xml.WriteElement("address2", AddressLine2);
                xml.WriteElement("city", City);
                xml.WriteElement("state", StateProvince);
                xml.WriteElement("zip", ZipPostalCode);
                xml.WriteElement("country", Country);
                xml.WriteEndElement(); //mailaddress
            }

            xml.WriteElement("defaultcard", DefaultCard);
            xml.WriteElement("usebilltoaddr", BillToContactAddressUsedForVerification);
            
            xml.WriteEndElement(); //update_customerchargecard

            xml.WriteEndElement(); //function
        }

    }

}