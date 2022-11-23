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
using Intacct.SDK.Functions.Purchasing;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Purchasing
{
    public class PurchasingTransactionLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<potransitem>
    <itemid>26323</itemid>
    <quantity>2340</quantity>
</potransitem>";

            PurchasingTransactionLineCreate record = new PurchasingTransactionLineCreate()
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
<potransitem>
    <itemid>26323</itemid>
    <itemdesc>Item Description</itemdesc>
    <taxable>true</taxable>
    <warehouseid>93294</warehouseid>
    <quantity>2340</quantity>
    <unit>593</unit>
    <linelevelsimpletaxtype>Test</linelevelsimpletaxtype>
    <price>32.35</price>
    <overridetaxamount>1.23</overridetaxamount>
    <tax>9.23</tax>
    <locationid>SF</locationid>
    <departmentid>Purchasing</departmentid>
    <memo>Memo</memo>
    <itemdetails>
        <itemdetail>
            <quantity>52</quantity>
            <lotno>3243</lotno>
        </itemdetail>
    </itemdetails>
    <form1099>true</form1099>
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
    <billable>true</billable>
</potransitem>";

            PurchasingTransactionLineCreate record = new PurchasingTransactionLineCreate() {
                ItemId = "26323",
                ItemDescription = "Item Description",
                Taxable = true,
                WarehouseId = "93294",
                Quantity = 2340,
                Unit = "593",
                LineLevelSimpleTaxType = "Test",
                Price = 32.35M,
                OverrideTaxAmount = 1.23M,
                Tax = 9.23M,
                Memo = "Memo",
                Billable = true,
                Form1099 = true,
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
            };
            record.ItemDetails.Add(detail1);
            
            this.CompareXml(expected, record);
        }
    }
}