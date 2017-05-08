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

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.OrderEntry;
using System.Collections.Generic;
using System;
using Intacct.Sdk.Functions.InventoryControl;

namespace Intacct.Sdk.Tests.Functions.OrderEntry
{

    [TestClass]
    public class OrderEntryTransactionLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<sotransitem>
    <itemid>26323</itemid>
    <quantity>2340</quantity>
</sotransitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionLineCreate record = new OrderEntryTransactionLineCreate()
            {
                ItemId = "26323",
                Quantity = 2340,
            };
            
            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<sotransitem>
    <bundlenumber>092304</bundlenumber>
    <itemid>26323</itemid>
    <itemdesc>Item Description</itemdesc>
    <taxable>true</taxable>
    <warehouseid>93294</warehouseid>
    <quantity>2340</quantity>
    <unit>593</unit>
    <discountpercent>10.00</discountpercent>
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
    <renewalmacro>Quarterly</renewalmacro>
    <projectid>235</projectid>
    <customerid>23423434</customerid>
    <vendorid>797656</vendorid>
    <employeeid>90295</employeeid>
    <classid>243609</classid>
    <contractid>9062</contractid>
    <fulfillmentstatus>Complete</fulfillmentstatus>
    <taskno>9850</taskno>
    <billingtemplate>3525</billingtemplate>
</sotransitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionLineCreate record = new OrderEntryTransactionLineCreate() {
                BundleNumber = "092304",
                ItemId = "26323",
                ItemDescription = "Item Description",
                Taxable = true,
                WarehouseId = "93294",
                Quantity = 2340,
                Unit = "593",
                DiscountPercent = 10.00M,
                Price = 32.35M,
                DiscountSurchargeMemo = "None",
                Memo = "Memo",
                RevRecTemplate = "template",
                RevRecStartDate = new DateTime(2015, 06, 30),
                RevRecEndDate = new DateTime(2015, 07, 31),
                RenewalMacro = "Quarterly",
                FulfillmentStatus = "Complete",
                TaskNumber = "9850",
                BillingTemplate = "3525",
                LocationId = "SF",
                DepartmentId = "Receiving",
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

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
        
    }

}
