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
using System.Collections.Generic;
using System;

namespace Intacct.Sdk.Tests.Functions.EmployeeExpenses
{

    [TestClass]
    public class ExpenseReportSummaryCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_expensereportbatch>
        <batchtitle>unit test</batchtitle>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
    </create_expensereportbatch>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ExpenseReportSummaryCreate record = new ExpenseReportSummaryCreate("unittest");
            record.Title = "unit test";
            record.GlPostingDate = new DateTime(2015, 06, 30);
            
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
<function controlid=""unittest"">
    <create_expensereport>
        <employeeid>E0001</employeeid>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <dateposted>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </dateposted>
        <batchkey>123</batchkey>
        <expensereportno>ER001</expensereportno>
        <state>Submitted</state>
        <description>For hotel</description>
        <memo>Memo</memo>
        <externalid>122</externalid>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <supdocid>AT122</supdocid>
        <expenses>
            <expense>
                <glaccountno />
            </expense>
        </expenses>
    </create_expensereport>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ExpenseReportCreate record = new ExpenseReportCreate("unittest")
            {
                EmployeeId = "E0001",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                SummaryRecordNo = 123,
                ExpenseReportNumber = "ER001",
                Action = "Submitted",
                ReasonForExpense = "For hotel",
                Memo = "Memo",
                ExternalId = "122",
                BaseCurrency = "USD",
                ReimbursementCurrency = "USD",
                AttachmentsId = "AT122",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                },
            };
            

            ExpenseReportLineCreate line1 = new ExpenseReportLineCreate();

            record.Lines.Add(line1);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

    }

}
