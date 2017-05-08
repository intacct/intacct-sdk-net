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
using Intacct.Sdk.Functions.AccountsPayable;
using System.Collections.Generic;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.AccountsPayable
{

    [TestClass]
    public class BillCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_bill>
        <vendorid>VENDOR1</vendorid>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <termname>N30</termname>
        <billitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </billitems>
    </create_bill>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            BillCreate record = new BillCreate("unittest");
            record.VendorId = "VENDOR1";
            record.TransactionDate = new DateTime(2015, 06, 30);
            record.PaymentTerm = "N30";

            BillLineCreate line1 = new BillLineCreate();
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
    <create_bill>
        <vendorid>VENDOR1</vendorid>
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
        <datedue>
            <year>2020</year>
            <month>09</month>
            <day>24</day>
        </datedue>
        <termname>N30</termname>
        <action>Submit</action>
        <batchkey>20323</batchkey>
        <billno>234</billno>
        <ponumber>234235</ponumber>
        <onhold>true</onhold>
        <description>Some description</description>
        <externalid>20394</externalid>
        <payto>
            <contactname>28952</contactname>
        </payto>
        <returnto>
            <contactname>289533</contactname>
        </returnto>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <exchratedate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <nogl>false</nogl>
        <supdocid>6942</supdocid>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <billitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </billitems>
    </create_bill>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            BillCreate record = new BillCreate("unittest")
            {
                VendorId = "VENDOR1",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                DueDate = new DateTime(2020, 09, 24),
                PaymentTerm = "N30",
                Action = "Submit",
                SummaryRecordNo = 20323,
                BillNumber = "234",
                ReferenceNumber = "234235",
                OnHold = true,
                Description = "Some description",
                ExternalId = "20394",
                PayToContactName = "28952",
                ReturnToContactName = "289533",
                BaseCurrency = "USD",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                DoNotPostToGL = false,
                AttachmentsId = "6942",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                },
            };
            

            BillLineCreate line1 = new BillLineCreate();
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
