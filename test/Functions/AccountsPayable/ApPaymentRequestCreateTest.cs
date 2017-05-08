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
using System;

namespace Intacct.Sdk.Tests.Functions.AccountsPayable
{

    [TestClass]
    public class ApPaymentRequestCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_paymentrequest>
        <bankaccountid>BA1143</bankaccountid>
        <vendorid>V0001</vendorid>
        <paymentmethod>Printed Check</paymentmethod>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <paymentrequestitems>
            <paymentrequestitem>
                <key>123</key>
                <paymentamount>100.12</paymentamount>
            </paymentrequestitem>
        </paymentrequestitems>
    </create_paymentrequest>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ApPaymentRequestCreate record = new ApPaymentRequestCreate("unittest")
            {
                BankAccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = AbstractApPaymentRequest.PaymentMethodCheck,
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentRequestItem line1 = new ApPaymentRequestItem()
            {
                ApplyToRecordId = 123,
                AmountToApply = 100.12M,
            };

            record.ApplyToTransactions.Add(line1);

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
    <create_paymentrequest>
        <bankaccountid>BA1143</bankaccountid>
        <vendorid>V0001</vendorid>
        <memo>Memo</memo>
        <paymentmethod>Printed Check</paymentmethod>
        <grouppayments>true</grouppayments>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <paymentoption>vendorpref</paymentoption>
        <paymentrequestitems>
            <paymentrequestitem>
                <key>123</key>
                <paymentamount>100.12</paymentamount>
                <credittoapply>8.12</credittoapply>
                <discounttoapply>1.21</discounttoapply>
            </paymentrequestitem>
        </paymentrequestitems>
        <documentnumber>10000</documentnumber>
        <paymentdescription>Memo</paymentdescription>
        <paymentcontact>Jim Smith</paymentcontact>
    </create_paymentrequest>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ApPaymentRequestCreate record = new ApPaymentRequestCreate("unittest")
            {
                BankAccountId = "BA1143",
                VendorId = "V0001",
                Memo = "Memo",
                PaymentMethod = AbstractApPaymentRequest.PaymentMethodCheck,
                GroupPayments = true,
                PaymentDate = new DateTime(2015, 06, 30),
                MergeOption = "vendorpref",
                DocumentNo = "10000",
                NotificationContactName = "Jim Smith",
            };

            ApPaymentRequestItem line1 = new ApPaymentRequestItem()
            {
                ApplyToRecordId = 123,
                AmountToApply = 100.12M,
                CreditToApply = 8.12M,
                DiscountToApply = 1.21M,
            };

            record.ApplyToTransactions.Add(line1);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
        
    }

}
