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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.InventoryControl
{
    public class InventoryTransactionLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ictransitem>
    <itemid>26323</itemid>
    <warehouseid>W1234</warehouseid>
    <quantity>2340</quantity>
</ictransitem>";

            InventoryTransactionLineCreate record = new InventoryTransactionLineCreate()
            {
                ItemId = "26323",
                WarehouseId = "W1234",
                Quantity = 2340,
            };
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ictransitem>
    <itemid>26323</itemid>
    <itemdesc>Item Description</itemdesc>
    <warehouseid>93294</warehouseid>
    <quantity>2340</quantity>
    <unit>593</unit>
    <cost>32.35</cost>
    <locationid>SF</locationid>
    <departmentid>Purchasing</departmentid>
    <itemdetails>
        <itemdetail>
            <quantity>52</quantity>
            <serialno>S111</serialno>
            <lotno>3243</lotno>
        </itemdetail>
    </itemdetails>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <projectid>235</projectid>
    <customerid>23423434</customerid>
    <vendorid>797656</vendorid>
    <employeeid>90295</employeeid>
    <classid>243609</classid>
    <contractid>9062</contractid>
</ictransitem>";

            InventoryTransactionLineCreate record = new InventoryTransactionLineCreate() {
                ItemId = "26323",
                ItemDescription = "Item Description",
                WarehouseId = "93294",
                Quantity = 2340,
                Unit = "593",
                Cost = 32.35M,
                LocationId = "SF",
                DepartmentId = "Purchasing",
                ProjectId = "235",
                CustomerId = "23423434",
                VendorId = "797656",
                EmployeeId = "90295",
                ClassId = "243609",
                ContractId = "9062",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            TransactionItemDetail detail1 = new TransactionItemDetail()
            {
                Quantity = 52,
                LotNumber = "3243",
                SerialNumber = "S111"
            };
            record.ItemDetails.Add(detail1);
            
            this.CompareXml(expected, record);
        }
    }
}