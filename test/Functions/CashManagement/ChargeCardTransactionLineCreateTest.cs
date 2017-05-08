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
using Intacct.Sdk.Functions.CashManagement;
using System.Collections.Generic;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.CashManagement
{

    [TestClass]
    public class ChargeCardTransactionLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ccpayitem>
    <glaccountno />
    <paymentamount>76343.43</paymentamount>
</ccpayitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ChargeCardTransactionLineCreate record = new ChargeCardTransactionLineCreate();
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
<ccpayitem>
    <accountlabel>TestBill Account1</accountlabel>
    <description>Just another memo</description>
    <paymentamount>76343.43</paymentamount>
    <departmentid>Department1</departmentid>
    <locationid>Location1</locationid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <projectid>Project1</projectid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
</ccpayitem>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ChargeCardTransactionLineCreate record = new ChargeCardTransactionLineCreate();
            record.TransactionAmount = 76343.43M;
            record.AccountLabel = "TestBill Account1";
            record.Memo = "Just another memo";
            record.LocationId = "Location1";
            record.DepartmentId = "Department1";
            record.ProjectId = "Project1";
            record.CustomerId = "Customer1";
            record.VendorId = "Vendor1";
            record.EmployeeId = "Employee1";
            record.ItemId = "Item1";
            record.ClassId = "Class1";
            record.ContractId = "Contract1";
            record.WarehouseId = "Warehouse1";
            record.CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
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
