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

namespace Intacct.Sdk.Tests.Functions.CashManagement
{

    [TestClass]
    public class DepositCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <record_deposit>
        <bankaccountid>BA1145</bankaccountid>
        <depositdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </depositdate>
        <depositid>Deposit Slip 2015-06-30</depositid>
        <receiptkeys>
            <receiptkey>1234</receiptkey>
        </receiptkeys>
    </record_deposit>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            DepositCreate record = new DepositCreate("unittest");
            record.BankAccountId = "BA1145";
            record.DepositDate = new DateTime(2015, 06, 30);
            record.DepositSlipId = "Deposit Slip 2015-06-30";
            
            int key = 1234;
            record.TransactionsKeysToDeposit.Add(key);

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
    <record_deposit>
        <bankaccountid>BA1145</bankaccountid>
        <depositdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </depositdate>
        <depositid>Deposit Slip 2015-06-30</depositid>
        <receiptkeys>
            <receiptkey>1234</receiptkey>
        </receiptkeys>
        <description>Desc</description>
        <supdocid>AT111</supdocid>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
    </record_deposit>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            DepositCreate record = new DepositCreate("unittest");
            record.BankAccountId = "BA1145";
            record.DepositDate = new DateTime(2015, 06, 30);
            record.DepositSlipId = "Deposit Slip 2015-06-30";
            record.Description = "Desc";
            record.AttachmentsId = "AT111";
            record.TransactionsKeysToDeposit = new List<int>() {1234};
            record.CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                };

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
    }
}
