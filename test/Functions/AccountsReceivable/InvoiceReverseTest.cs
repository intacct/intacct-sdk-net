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
using Intacct.Sdk.Functions.AccountsReceivable;
using System.Collections.Generic;
using System;

namespace Intacct.Sdk.Tests.Functions.AccountsReceivable
{

    [TestClass]
    public class InvoiceReverseTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <reverse_invoice key=""1234"">
        <datereversed>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datereversed>
    </reverse_invoice>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            InvoiceReverse record = new InvoiceReverse("unittest");
            record.RecordNo = 1234;
            record.ReverseDate = new DateTime(2015, 06, 30);
            
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
    <create_invoice>
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
        <datedue>
            <year>2020</year>
            <month>09</month>
            <day>24</day>
        </datedue>
        <termname>N30</termname>
        <action>Submit</action>
        <batchkey>20323</batchkey>
        <invoiceno>234</invoiceno>
        <ponumber>234235</ponumber>
        <onhold>true</onhold>
        <description>Some description</description>
        <externalid>20394</externalid>
        <billto>
            <contactname>28952</contactname>
        </billto>
        <shipto>
            <contactname>289533</contactname>
        </shipto>
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
        <invoiceitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </invoiceitems>
    </create_invoice>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            InvoiceCreate record = new InvoiceCreate("unittest")
            {
                CustomerId = "CUSTOMER1",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                DueDate = new DateTime(2020, 09, 24),
                PaymentTerm = "N30",
                Action = "Submit",
                SummaryRecordNo = 20323,
                InvoiceNumber = "234",
                ReferenceNumber = "234235",
                OnHold = true,
                Description = "Some description",
                ExternalId = "20394",
                BillToContactName = "28952",
                ShipToContactName = "289533",
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
            

            InvoiceLineCreate line1 = new InvoiceLineCreate();
            line1.TransactionAmount = 76343.43M;

            record.Lines.Add(line1);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

    }

}
