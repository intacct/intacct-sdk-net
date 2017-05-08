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
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.EmployeeExpenses;
using System;
using System.Collections.Generic;

namespace Intacct.Sdk.Tests.Functions.EmployeeExpenses
{

    [TestClass]
    public class ExpenseAdjustmentLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<expenseadjustment>
    <glaccountno />
    <amount />
</expenseadjustment>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ExpenseAdjustmentLineCreate line = new ExpenseAdjustmentLineCreate();

            line.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<expenseadjustment>
    <glaccountno>7000</glaccountno>
    <amount>1025.99</amount>
    <currency>USD</currency>
    <trx_amount>76343.43</trx_amount>
    <exchratedate>
        <year>2016</year>
        <month>06</month>
        <day>30</day>
    </exchratedate>
    <exchratetype>Intacct Daily Rate</exchratetype>
    <expensedate>
        <year>2016</year>
        <month>06</month>
        <day>30</day>
    </expensedate>
    <memo>Marriott</memo>
    <locationid>Location1</locationid>
    <departmentid>Department1</departmentid>
    <projectid>Project1</projectid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
    <billable>true</billable>
    <exppmttype>AMEX</exppmttype>
    <quantity>10</quantity>
    <rate>12.34</rate>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
</expenseadjustment>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ExpenseAdjustmentLineCreate record = new ExpenseAdjustmentLineCreate()
            {
                GlAccountNumber = "7000",
                ReimbursementAmount = 1025.99M,
                TransactionCurrency = "USD",
                TransactionAmount = 76343.43M,
                ExchangeRateDate = new DateTime(2016, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                ExpenseDate = new DateTime(2016, 06, 30),
                Memo = "Marriott",
                Billable = true,
                PaymentTypeName = "AMEX",
                Quantity = 10,
                UnitRate = 12.34M,
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

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

    }

}
