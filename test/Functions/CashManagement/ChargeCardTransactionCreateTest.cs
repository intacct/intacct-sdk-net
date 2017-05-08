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
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.CashManagement
{

    [TestClass]
    public class ChargeCardTransactionCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <record_cctransaction>
        <chargecardid>AMEX1234</chargecardid>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <ccpayitems>
            <ccpayitem>
                <glaccountno />
                <paymentamount>76343.43</paymentamount>
            </ccpayitem>
        </ccpayitems>
    </record_cctransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ChargeCardTransactionCreate record = new ChargeCardTransactionCreate("unittest");
            record.ChargeCardId = "AMEX1234";
            record.TransactionDate = new DateTime(2015, 06, 30);

            ChargeCardTransactionLineCreate line = new ChargeCardTransactionLineCreate();
            line.TransactionAmount = 76343.43M;

            record.Lines.Add(line);

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
    <record_cctransaction>
        <chargecardid>AMEX1234</chargecardid>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <referenceno>321</referenceno>
        <payee>Costco</payee>
        <description>Supplies</description>
        <supdocid>A1234</supdocid>
        <currency>USD</currency>
        <exchratedate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <ccpayitems>
            <ccpayitem>
                <glaccountno />
                <paymentamount>76343.43</paymentamount>
            </ccpayitem>
        </ccpayitems>
    </record_cctransaction>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ChargeCardTransactionCreate record = new ChargeCardTransactionCreate("unittest");
            record.ChargeCardId = "AMEX1234";
            record.TransactionDate = new DateTime(2015, 06, 30);
            record.ReferenceNumber = "321";
            record.Payee = "Costco";
            record.Description = "Supplies";
            record.AttachmentsId = "A1234";
            record.TransactionCurrency = "USD";
            record.ExchangeRateDate = new DateTime(2015, 06, 30);
            record.ExchangeRateType = "Intacct Daily Rate";
            record.CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                };

            ChargeCardTransactionLineCreate line = new ChargeCardTransactionLineCreate();
            line.TransactionAmount = 76343.43M;

            record.Lines.Add(line);

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
