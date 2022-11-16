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
    public class TransactionSubtotalCreate : AbstractTransactionSubtotal
    {

        public TransactionSubtotalCreate() : base()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("subtotal");

            xml.WriteElement("description", Description, true);
            xml.WriteElement("total", Total);
            xml.WriteElement("absval", AbsoluteValue);
            xml.WriteElement("percentval", PercentageValue);
            xml.WriteElement("locationid", LocationId);
            xml.WriteElement("departmentid", DepartmentId);
            xml.WriteElement("projectid", ProjectId);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("vendorid", VendorId);
            xml.WriteElement("employeeid", EmployeeId);
            xml.WriteElement("classid", ClassId);
            xml.WriteElement("itemid", ItemId);
            xml.WriteElement("contractid", ContractId);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteEndElement(); //subtotal
        }

    }
}