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
using Intacct.Sdk.Functions.InventoryControl;
using System.Collections.Generic;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.InventoryControl
{

    [TestClass]
    public class InventoryTransactionLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ictransitem>
    <itemid>26323</itemid>
    <warehouseid>W1234</warehouseid>
    <quantity>2340</quantity>
</ictransitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            InventoryTransactionLineCreate record = new InventoryTransactionLineCreate()
            {
                ItemId = "26323",
                WarehouseId = "W1234",
                Quantity = 2340,
            };
            
            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

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
            };
            record.ItemDetails.Add(detail1);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }
        
    }

}
