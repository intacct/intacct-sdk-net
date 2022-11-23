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
    public class LocationCreate : AbstractLocation
    {

        public LocationCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("LOCATION");

            xml.WriteElement("LOCATIONID", LocationId, true);
            xml.WriteElement("NAME", LocationName, true);

            xml.WriteElement("PARENTID", ParentLocationId);
            xml.WriteElement("SUPERVISORID", ManagerEmployeeId);

            if (!string.IsNullOrWhiteSpace(LocationContactName))
            {
                xml.WriteStartElement("CONTACTINFO");
                xml.WriteElement("CONTACTNAME", LocationContactName);
                xml.WriteEndElement(); //CONTACTINFO
            }

            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("SHIPTO");
                xml.WriteElement("CONTACTNAME", ShipToContactName);
                xml.WriteEndElement(); //SHIPTO
            }

            xml.WriteElement("STARTDATE", StartDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("ENDDATE", EndDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("CUSTTITLE", LocationTitle);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //LOCATION
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}