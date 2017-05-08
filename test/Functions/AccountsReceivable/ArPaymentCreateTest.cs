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

namespace Intacct.Sdk.Tests.Functions.AccountsReceivable
{

    [TestClass]
    public class ArPaymentCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_arpayment>
        <customerid>C0020</customerid>
        <paymentamount>1922.12</paymentamount>
        <datereceived>
            <year>2016</year>
            <month>06</month>
            <day>30</day>
        </datereceived>
        <paymentmethod>Printed Check</paymentmethod>
    </create_arpayment>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ArPaymentCreate record = new ArPaymentCreate("unittest");
            record.CustomerId = "C0020";
            record.TransactionPaymentAmount = 1922.12M;
            record.ReceivedDate = new DateTime(2016, 06, 30);
            record.PaymentMethod = ArPaymentCreate.PaymentMethodCheck;

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
        
    }

}
