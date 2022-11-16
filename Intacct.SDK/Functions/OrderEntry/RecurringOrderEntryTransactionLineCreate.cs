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

using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.OrderEntry
{
    public class RecurringOrderEntryTransactionLineCreate : AbstractRecurringOrderEntryTransactionLine
    {

        public RecurringOrderEntryTransactionLineCreate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("recursotransitem");
            
            xml.WriteElement("itemid", ItemId);
            xml.WriteElement("itemaliasid", ItemAliasId);
            xml.WriteElement("itemdesc", ItemDescription);
            xml.WriteElement("taxable", IsTaxable);
            xml.WriteElement("warehouseid", WarehouseId);
            xml.WriteElement("quantity", Quantity);
            xml.WriteElement("unit", Unit);
            xml.WriteElement("price", Price);
            xml.WriteElement("discsurchargememo", DiscountSurchargeMemo);
            xml.WriteElement("locationid", LocationId);
            xml.WriteElement("departmentid", DepartmentId);
            xml.WriteElement("memo", Memo);

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
            
            xml.WriteElement("revrectemplate", RevRecTemplate);

            if (RevRecStartDate.HasValue)
            {
                xml.WriteStartElement("revrecstartdate");
                xml.WriteDateSplitElements(RevRecStartDate.Value);
                xml.WriteEndElement(); // revrecstartdate
            }
            
            if (RevRecEndDate.HasValue)
            {
                xml.WriteStartElement("revrecenddate");
                xml.WriteDateSplitElements(RevRecEndDate.Value);
                xml.WriteEndElement(); //revrecenddate
            }
            
            xml.WriteElement("status", Status);
            xml.WriteElement("projectid", ProjectId);
            xml.WriteElement("taskid", TaskId);
            xml.WriteElement("costtypeid", CostTypeId);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("vendorid", VendorId);
            xml.WriteElement("employeeid", EmployeeId);
            xml.WriteElement("classid", ClassId);
            xml.WriteElement("contractid", ContractId);
            
            if (!string.IsNullOrWhiteSpace(LineShipToContactName))
            {
                xml.WriteStartElement("shipto");
                xml.WriteElement("contactname", LineShipToContactName, true);
                xml.WriteEndElement(); //shipto
            }
            
            xml.WriteEndElement(); //recursotransitem
        }
    }
}