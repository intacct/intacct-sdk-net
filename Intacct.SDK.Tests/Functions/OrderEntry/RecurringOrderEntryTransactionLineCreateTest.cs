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
using Intacct.SDK.Functions.OrderEntry;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.OrderEntry
{
    public class RecurringOrderEntryTransactionLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<recursotransitem>
    <itemid>26323</itemid>
    <quantity>2340</quantity>
</recursotransitem>";

            RecurringOrderEntryTransactionLineCreate record = new RecurringOrderEntryTransactionLineCreate()
            {
                ItemId = "26323",
                Quantity = 2340,
            };

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<recursotransitem>
    <itemid>26323</itemid>
    <itemaliasid>987654</itemaliasid>
    <itemdesc>Item Description</itemdesc>
    <taxable>true</taxable>
    <warehouseid>93294</warehouseid>
    <quantity>2340</quantity>
    <unit>593</unit>
    <price>32.35</price>
    <discsurchargememo>None</discsurchargememo>
    <locationid>SF</locationid>
    <departmentid>Receiving</departmentid>
    <memo>Memo</memo>
    <itemdetails>
        <itemdetail>
            <quantity>52</quantity>
            <lotno>3243</lotno>
        </itemdetail>
    </itemdetails>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <revrectemplate>template</revrectemplate>
    <revrecstartdate>
        <year>2015</year>
        <month>06</month>
        <day>30</day>
    </revrecstartdate>
    <revrecenddate>
        <year>2015</year>
        <month>07</month>
        <day>31</day>
    </revrecenddate>
    <status>active</status>
    <projectid>235</projectid>
    <taskid>654321</taskid>
    <costtypeid>321654</costtypeid>
    <customerid>23423434</customerid>
    <vendorid>797656</vendorid>
    <employeeid>90295</employeeid>
    <classid>243609</classid>
    <contractid>9062</contractid>
    <shipto>
        <contactname>Unknown</contactname>
    </shipto>
</recursotransitem>";

            RecurringOrderEntryTransactionLineCreate record = new RecurringOrderEntryTransactionLineCreate() {
                ItemId = "26323",
                ItemAliasId = "987654",
                ItemDescription = "Item Description",
                IsTaxable = true,
                WarehouseId = "93294",
                Quantity = 2340,
                Unit = "593",
                Price = 32.35M,
                DiscountSurchargeMemo = "None",
                Memo = "Memo",
                RevRecTemplate = "template",
                RevRecStartDate = new DateTime(2015, 06, 30),
                RevRecEndDate = new DateTime(2015, 07, 31),
                LocationId = "SF",
                DepartmentId = "Receiving",
                Status = "active",
                ProjectId = "235",
                TaskId = "654321",
                CostTypeId = "321654",
                CustomerId = "23423434",
                VendorId = "797656",
                EmployeeId = "90295",
                ClassId = "243609",
                ContractId = "9062",
                LineShipToContactName = "Unknown",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            TransactionItemDetail detail1 = new TransactionItemDetail()
            {
                Quantity = 52,
                LotNumber = "3243",
            };
            record.ItemDetails.Add(detail1);

            this.CompareXml(expected, record);
        }
    }
}