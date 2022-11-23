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
    public class InventoryTransactionLineCreate : AbstractInventoryTransactionLine
    {
        
        public int? SourceLineRecordNo;

        public InventoryTransactionLineCreate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("ictransitem");

            xml.WriteElement("itemid", ItemId, true);
            xml.WriteElement("itemdesc", ItemDescription);
            xml.WriteElement("warehouseid", WarehouseId);
            xml.WriteElement("quantity", Quantity, true);
            xml.WriteElement("unit", Unit);
            xml.WriteElement("cost", Cost);
            xml.WriteElement("sourcelinekey", SourceLineRecordNo);
            xml.WriteElement("locationid", LocationId);
            xml.WriteElement("departmentid", DepartmentId);

            if (ItemDetails.Count > 0)
            {
                xml.WriteStartElement("itemdetails");
                foreach (AbstractTransactionItemDetail itemDetail in ItemDetails)
                {
                    itemDetail.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //itemdetails
            }

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("projectid", ProjectId);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("vendorid", VendorId);
            xml.WriteElement("employeeid", EmployeeId);
            xml.WriteElement("classid", ClassId);
            xml.WriteElement("contractid", ContractId);

            xml.WriteEndElement(); //ictransitem
        }

    }
}