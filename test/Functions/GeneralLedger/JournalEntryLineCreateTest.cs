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
using Intacct.Sdk.Functions.GeneralLedger;
using System.Collections.Generic;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.GeneralLedger
{

    [TestClass]
    public class JournalEntryLineCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<GLENTRY>
    <ACCOUNTNO />
    <TR_TYPE>1</TR_TYPE>
    <TRX_AMOUNT />
</GLENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            JournalEntryLineCreate record = new JournalEntryLineCreate();
            
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
        public void CreditAmountTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<GLENTRY>
    <ACCOUNTNO />
    <TR_TYPE>-1</TR_TYPE>
    <TRX_AMOUNT>400.23</TRX_AMOUNT>
</GLENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            JournalEntryLineCreate record = new JournalEntryLineCreate();
            record.TransactionAmount = -400.23M;

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
<GLENTRY>
    <DOCUMENT>212</DOCUMENT>
    <ACCOUNTNO>1010</ACCOUNTNO>
    <TR_TYPE>1</TR_TYPE>
    <TRX_AMOUNT>1456.54</TRX_AMOUNT>
    <CURRENCY>USD</CURRENCY>
    <EXCH_RATE_DATE>06/30/2016</EXCH_RATE_DATE>
    <EXCH_RATE_TYPE_ID>Intacct Daily Rate</EXCH_RATE_TYPE_ID>
    <LOCATION>100</LOCATION>
    <DEPARTMENT>ADM</DEPARTMENT>
    <PROJECTID>P100</PROJECTID>
    <CUSTOMERID>C100</CUSTOMERID>
    <VENDORID>V100</VENDORID>
    <EMPLOYEEID>E100</EMPLOYEEID>
    <ITEMID>I100</ITEMID>
    <CLASSID>C200</CLASSID>
    <CONTRACTID>C300</CONTRACTID>
    <WAREHOUSEID>W100</WAREHOUSEID>
    <DESCRIPTION>my memo</DESCRIPTION>
    <CUSTOM01>123</CUSTOM01>
</GLENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            JournalEntryLineCreate record = new JournalEntryLineCreate() {
                DocumentNumber = "212",
                GlAccountNumber = "1010",
                TransactionAmount = 1456.54M,
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2016, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                Memo = "my memo",
                LocationId = "100",
                DepartmentId = "ADM",
                ProjectId = "P100",
                CustomerId = "C100",
                VendorId = "V100",
                EmployeeId = "E100",
                ItemId = "I100",
                ClassId = "C200",
                ContractId = "C300",
                WarehouseId = "W100",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "CUSTOM01", "123" }
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

        [TestMethod()]
        public void AllocationTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<GLENTRY>
    <ACCOUNTNO>1010</ACCOUNTNO>
    <TR_TYPE>1</TR_TYPE>
    <TRX_AMOUNT>1456.54</TRX_AMOUNT>
    <ALLOCATION>60-40</ALLOCATION>
</GLENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            JournalEntryLineCreate record = new JournalEntryLineCreate()
            {
                GlAccountNumber = "1010",
                TransactionAmount = 1456.54M,
                AllocationId = "60-40",
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
        public void CustomAllocationTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<GLENTRY>
    <ACCOUNTNO>1010</ACCOUNTNO>
    <TR_TYPE>1</TR_TYPE>
    <TRX_AMOUNT>1000.00</TRX_AMOUNT>
    <ALLOCATION>Custom</ALLOCATION>
    <SPLIT>
        <AMOUNT>600.00</AMOUNT>
    </SPLIT>
    <SPLIT>
        <AMOUNT>400.00</AMOUNT>
    </SPLIT>
</GLENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            JournalEntryLineCreate record = new JournalEntryLineCreate()
            {
                GlAccountNumber = "1010",
                TransactionAmount = 1000.00M,
                AllocationId = "Custom",
            };

            CustomAllocationSplit split1 = new CustomAllocationSplit()
            {
                Amount = 600.00M,
            };

            CustomAllocationSplit split2 = new CustomAllocationSplit()
            {
                Amount = 400.00M,
            };

            record.CustomAllocationSplits.Add(split1);
            record.CustomAllocationSplits.Add(split2);

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
