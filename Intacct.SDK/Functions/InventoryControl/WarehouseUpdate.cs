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

namespace Intacct.SDK.Functions.InventoryControl
{
    public class WarehouseUpdate : AbstractWarehouse
    {

        public WarehouseUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("WAREHOUSE");

            xml.WriteElement("RECORDNO", RecordNo);
            xml.WriteElement("WAREHOUSEID", WarehouseId);

            xml.WriteElement("NAME", WarehouseName);

            if (!string.IsNullOrWhiteSpace(LocationId))
            {
                xml.WriteStartElement("LOC");
                xml.WriteElement("LOCATIONID", LocationId);
                xml.WriteEndElement(); // LOC
            }

            xml.WriteElement("MANAGERID", ManagerEmployeeId);
            xml.WriteElement("PARENTID", ParentWarehouseId);

            if (!string.IsNullOrWhiteSpace(WarehouseContactName))
            {
                xml.WriteStartElement("CONTACTINFO");
                xml.WriteElement("CONTACTNAME", WarehouseContactName);
                xml.WriteEndElement(); //CONTACTINFO
            }
            if (!string.IsNullOrWhiteSpace(ShipToContactName))
            {
                xml.WriteStartElement("SHIPTO");
                xml.WriteElement("CONTACTNAME", ShipToContactName);
                xml.WriteEndElement(); //SHIPTO
            }

            xml.WriteElement("USEDINGL", UsedInGeneralLedger);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //WAREHOUSE
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}