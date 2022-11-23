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
    public class ChargeCardTransactionUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_cctransaction key=""1234"" />
</function>";

            ChargeCardTransactionUpdate record = new ChargeCardTransactionUpdate("unittest")
            {
                RecordNo = 1234
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_cctransaction key=""1234"">
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
        <updateccpayitems>
            <updateccpayitem line_num=""1"">
                <paymentamount>76343.43</paymentamount>
            </updateccpayitem>
        </updateccpayitems>
    </update_cctransaction>
</function>";

            ChargeCardTransactionUpdate record = new ChargeCardTransactionUpdate("unittest")
            {
                RecordNo = 1234,
                TransactionDate = new DateTime(2015, 06, 30),
                ReferenceNumber = "321",
                Payee = "Costco",
                Description = "Supplies",
                AttachmentsId = "A1234",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            ChargeCardTransactionLineUpdate line = new ChargeCardTransactionLineUpdate
            {
                LineNo = 1,
                TransactionAmount = 76343.43M
            };

            record.Lines.Add(line);
            
            this.CompareXml(expected, record);
        }
    }
}