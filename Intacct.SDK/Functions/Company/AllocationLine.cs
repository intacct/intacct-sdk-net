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
    public class AllocationLine : AbstractAllocationLine
    {

        public AllocationLine()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("ALLOCATIONENTRY");

            xml.WriteElement("VALUE", Amount, true);

            xml.WriteElement("LOCATIONID", LocationId, true);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("PROJECTID", ProjectId);
            xml.WriteElement("CUSTOMERID", CustomerId);
            xml.WriteElement("VENDORID", VendorId);
            xml.WriteElement("EMPLOYEEID", EmployeeId);
            xml.WriteElement("ITEMID", ItemId);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteElement("CONTRACTID", ContractId);
            xml.WriteElement("WAREHOUSEID", WarehouseId);

            xml.WriteEndElement(); //ALLOCATIONENTRY
        }

    }
}