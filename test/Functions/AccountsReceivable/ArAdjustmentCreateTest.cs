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
    public class ArAdjustmentCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_aradjustment>
        <customerid>CUSTOMER1</customerid>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <aradjustmentitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </aradjustmentitems>
    </create_aradjustment>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ArAdjustmentCreate record = new ArAdjustmentCreate("unittest")
            {
                CustomerId = "CUSTOMER1",
                TransactionDate = new DateTime(2015, 06, 30),
            };

            ArAdjustmentLineCreate line1 = new ArAdjustmentLineCreate();
            line1.TransactionAmount = 76343.43M;

            record.Lines.Add(line1);

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
<function controlid=""unittest"">
    <create_aradjustment>
        <customerid>CUSTOMER1</customerid>
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
        <batchkey>20323</batchkey>
        <adjustmentno>234</adjustmentno>
        <action>Submit</action>
        <invoiceno>234235</invoiceno>
        <description>Some description</description>
        <externalid>20394</externalid>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <exchratedate>
            <year>2016</year>
            <month>06</month>
            <day>30</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <nogl>false</nogl>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <aradjustmentitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </aradjustmentitems>
    </create_aradjustment>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ArAdjustmentCreate record = new ArAdjustmentCreate("unittest")
            {
                CustomerId = "CUSTOMER1",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                SummaryRecordNo = "20323",
                AdjustmentNumber = "234",
                Action = "Submit",
                InvoiceNumber = "234235",
                Description = "Some description",
                ExternalId = "20394",
                BaseCurrency = "USD",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2016, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                DoNotPostToGL = false,
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                },
            };            

            ArAdjustmentLineCreate line1 = new ArAdjustmentLineCreate();
            line1.TransactionAmount = 76343.43M;

            record.Lines.Add(line1);
            
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
