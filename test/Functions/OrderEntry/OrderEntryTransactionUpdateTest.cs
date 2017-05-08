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
    public class OrderEntryTransactionUpdateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_sotransaction key=""Sales Order-SO0001"" />
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionUpdate record = new OrderEntryTransactionUpdate("unittest")
            {
                TransactionId = "Sales Order-SO0001",
            };

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void SubtotalEntryTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_sotransaction key=""Sales Order-SO0001"">
        <updatesotransitems>
            <updatesotransitem line_num=""1"">
                <itemid>02354032</itemid>
                <quantity>12</quantity>
            </updatesotransitem>
            <sotransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </sotransitem>
        </updatesotransitems>
        <updatesubtotals>
            <updatesubtotal>
                <description>Subtotal Description</description>
                <total>1200</total>
            </updatesubtotal>
        </updatesubtotals>
    </update_sotransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionUpdate record = new OrderEntryTransactionUpdate("unittest")
            {
                TransactionId = "Sales Order-SO0001",
            };

            OrderEntryTransactionLineUpdate line1 = new OrderEntryTransactionLineUpdate()
            {
                LineNo = 1,
                ItemId = "02354032",
                Quantity = 12,
            };
            record.Lines.Add(line1);

            OrderEntryTransactionLineCreate line2 = new OrderEntryTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };
            record.Lines.Add(line2);

            TransactionSubtotalUpdate subtotal1 = new TransactionSubtotalUpdate()
            {
                Description = "Subtotal Description",
                Total = 1200,
            };
            record.Subtotals.Add(subtotal1);

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
    <update_sotransaction key=""Sales Order-SO0001"">
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
        <referenceno>234235</referenceno>
        <termname>N30</termname>
        <datedue>
            <year>2020</year>
            <month>09</month>
            <day>24</day>
        </datedue>
        <origdocdate>
            <year>2015</year>
            <month>06</month>
            <day>15</day>
        </origdocdate>
        <message>Submit</message>
        <shippingmethod>USPS</shippingmethod>
        <shipto>
            <contactname>28952</contactname>
        </shipto>
        <billto>
            <contactname>289533</contactname>
        </billto>
        <supdocid>6942</supdocid>
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
    </update_sotransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OrderEntryTransactionUpdate record = new OrderEntryTransactionUpdate("unittest")
            {
                TransactionId = "Sales Order-SO0001",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                OriginalDocumentDate = new DateTime(2015, 06, 15),
                ReferenceNumber = "234235",
                PaymentTerm = "N30",
                DueDate = new DateTime(2020, 09, 24),
                Message = "Submit",
                ShippingMethod = "USPS",
                ShipToContactName = "28952",
                BillToContactName = "289533",
                AttachmentsId = "6942",
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

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

    }

}
