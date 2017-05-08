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
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.OrderEntry
{

    [TestClass]
    public class OrderEntryTransactionCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_sotransaction>
        <transactiontype>Sales Order</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <customerid>2530</customerid>
        <sotransitems>
            <sotransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </sotransitem>
        </sotransitems>
    </create_sotransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionCreate record = new OrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                TransactionDate = new DateTime(2015, 06, 30),
                CustomerId = "2530",
            };

            OrderEntryTransactionLineCreate line1 = new OrderEntryTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };

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
        public void SubtotalEntryTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_sotransaction>
        <transactiontype>Sales Order</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <customerid>2530</customerid>
        <sotransitems>
            <sotransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </sotransitem>
        </sotransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal Description</description>
                <total>1200</total>
            </subtotal>
        </subtotals>
    </create_sotransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionCreate record = new OrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                TransactionDate = new DateTime(2015, 06, 30),
                CustomerId = "2530",
            };

            OrderEntryTransactionLineCreate line1 = new OrderEntryTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };
            record.Lines.Add(line1);

            TransactionSubtotalCreate subtotal1 = new TransactionSubtotalCreate()
            {
                Description = "Subtotal Description",
                Total = 1200,
            };
            record.Subtotals.Add(subtotal1);

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
    <create_sotransaction>
        <transactiontype>Sales Order</transactiontype>
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
        <createdfrom>Sales Quote-Q1002</createdfrom>
        <customerid>23530</customerid>
        <documentno>23430</documentno>
        <origdocdate>
            <year>2015</year>
            <month>06</month>
            <day>15</day>
        </origdocdate>
        <referenceno>234235</referenceno>
        <termname>N30</termname>
        <datedue>
            <year>2020</year>
            <month>09</month>
            <day>24</day>
        </datedue>
        <message>Submit</message>
        <shippingmethod>USPS</shippingmethod>
        <shipto>
            <contactname>28952</contactname>
        </shipto>
        <billto>
            <contactname>289533</contactname>
        </billto>
        <supdocid>6942</supdocid>
        <externalid>20394</externalid>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <exchratedate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <vsoepricelist>VSOEPricing</vsoepricelist>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <state>Pending</state>
        <projectid>P2904</projectid>
        <sotransitems>
            <sotransitem>
                <itemid>2390552</itemid>
                <quantity>223</quantity>
            </sotransitem>
        </sotransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal description</description>
                <total>223</total>
            </subtotal>
        </subtotals>
    </create_sotransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionCreate record = new OrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                CreatedFrom = "Sales Quote-Q1002",
                CustomerId = "23530",
                DocumentNumber = "23430",
                OriginalDocumentDate = new DateTime(2015, 06, 15),
                ReferenceNumber = "234235",
                PaymentTerm = "N30",
                DueDate = new DateTime(2020, 09, 24),
                Message = "Submit",
                ShippingMethod = "USPS",
                ShipToContactName = "28952",
                BillToContactName = "289533",
                AttachmentsId = "6942",
                ExternalId = "20394",
                BaseCurrency = "USD",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                VsoePriceList = "VSOEPricing",
                State = "Pending",
                ProjectId = "P2904",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            OrderEntryTransactionLineCreate line1 = new OrderEntryTransactionLineCreate()
            {
                ItemId = "2390552",
                Quantity = 223,
            };
            record.Lines.Add(line1);

            TransactionSubtotalCreate subtotal1 = new TransactionSubtotalCreate()
            {
                Description = "Subtotal description",
                Total = 223,
            };
            record.Subtotals.Add(subtotal1);

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
