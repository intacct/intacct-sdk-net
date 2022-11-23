/*
 * Copyright 2022 Sage Intacct, Inc.
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.CashManagement;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.CashManagement
{
    public class OtherReceiptCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <record_otherreceipt>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <payee>Costco</payee>
        <receiveddate>
            <year>2015</year>
            <month>07</month>
            <day>01</day>
        </receiveddate>
        <paymentmethod>Printed Check</paymentmethod>
        <undepglaccountno>1000</undepglaccountno>
        <receiptitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </receiptitems>
    </record_otherreceipt>
</function>";

            OtherReceiptCreate record =
                new OtherReceiptCreate("unittest")
                {
                    TransactionDate = new DateTime(2015, 06, 30),
                    Payer = "Costco",
                    ReceiptDate = new DateTime(2015, 07, 01),
                    PaymentMethod = "Printed Check",
                    UndepositedFundsGlAccountNo = "1000"
                };

            OtherReceiptLineCreate line = new OtherReceiptLineCreate
            {
                TransactionAmount = 76343.43M
            };

            record.Lines.Add(line);
            
            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <record_otherreceipt>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <payee>Costco</payee>
        <receiveddate>
            <year>2015</year>
            <month>07</month>
            <day>01</day>
        </receiveddate>
        <paymentmethod>Printed Check</paymentmethod>
        <bankaccountid>BA1234</bankaccountid>
        <depositdate>
            <year>2015</year>
            <month>07</month>
            <day>04</day>
        </depositdate>
        <refid>transno</refid>
        <description>my desc</description>
        <supdocid>A1234</supdocid>
        <currency>USD</currency>
        <exchratedate>
            <year>2015</year>
            <month>07</month>
            <day>04</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <receiptitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </receiptitems>
    </record_otherreceipt>
</function>";

            OtherReceiptCreate record = new OtherReceiptCreate("unittest")
            {
                TransactionDate = new DateTime(2015, 06, 30),
                Payer = "Costco",
                ReceiptDate = new DateTime(2015, 07, 01),
                PaymentMethod = "Printed Check",
                BankAccountId = "BA1234",
                DepositDate = new DateTime(2015, 07, 04),
                UndepositedFundsGlAccountNo = "1000",
                TransactionNo = "transno",
                Description = "my desc",
                AttachmentsId = "A1234",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 07, 04),
                ExchangeRateType = "Intacct Daily Rate",
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            OtherReceiptLineCreate line = new OtherReceiptLineCreate
            {
                TransactionAmount = 76343.43M
            };

            record.Lines.Add(line);
            
            this.CompareXml(expected, record);
        }
    }
}