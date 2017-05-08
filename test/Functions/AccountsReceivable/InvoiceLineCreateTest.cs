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
using Intacct.Sdk.Functions.AccountsReceivable;
using System.Collections.Generic;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.AccountsReceivable
{

    [TestClass]
    public class InvoiceLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lineitem>
    <glaccountno />
    <amount>76343.43</amount>
</lineitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            InvoiceLineCreate record = new InvoiceLineCreate();
            record.TransactionAmount = 76343.43M;

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
<lineitem>
    <accountlabel>TestBill Account1</accountlabel>
    <offsetglaccountno>93590253</offsetglaccountno>
    <amount>76343.43</amount>
    <memo>Just another memo</memo>
    <locationid>Location1</locationid>
    <departmentid>Department1</departmentid>
    <key>1234</key>
    <totalpaid>23484.93</totalpaid>
    <totaldue>0.00</totaldue>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <revrectemplate>RevRec1</revrectemplate>
    <defrevaccount>2100</defrevaccount>
    <revrecstartdate>
        <year>2016</year>
        <month>06</month>
        <day>01</day>
    </revrecstartdate>
    <revrecenddate>
        <year>2017</year>
        <month>05</month>
        <day>31</day>
    </revrecenddate>
    <projectid>Project1</projectid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
</lineitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            InvoiceLineCreate record = new InvoiceLineCreate() {
                AccountLabel = "TestBill Account1",
                OffsetGlAccountNumber = "93590253",
                TransactionAmount = 76343.43M,
                Memo = "Just another memo",
                Key = 1234,
                TotalPaid = 23484.93M,
                TotalDue = 0.00M,
                RevRecTemplateId = "RevRec1",
                DeferredRevGlAccountNo = "2100",
                RevRecStartDate = new DateTime(2016, 06, 01),
                RevRecEndDate = new DateTime(2017, 05, 31),
                LocationId = "Location1",
                DepartmentId = "Department1",
                ProjectId = "Project1",
                CustomerId = "Customer1",
                VendorId = "Vendor1",
                EmployeeId = "Employee1",
                ItemId = "Item1",
                ClassId = "Class1",
                ContractId = "Contract1",
                WarehouseId = "Warehouse1",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
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

    }

}
