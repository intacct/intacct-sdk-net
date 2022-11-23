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
    public class DepositCreateTest : XmlObjectTestHelper
    {
        [Fact]
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

            DepositCreate record = new DepositCreate("unittest")
            {
                BankAccountId = "BA1145",
                DepositDate = new DateTime(2015, 06, 30),
                DepositSlipId = "Deposit Slip 2015-06-30"
            };
            record.TransactionsKeysToDeposit.Add(1234);

            this.CompareXml(expected, record);
        }

        [Fact]
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

            DepositCreate record = new DepositCreate("unittest")
            {
                BankAccountId = "BA1145",
                DepositDate = new DateTime(2015, 06, 30),
                DepositSlipId = "Deposit Slip 2015-06-30",
                Description = "Desc",
                AttachmentsId = "AT111",
                TransactionsKeysToDeposit = new List<int>() {1234},
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            this.CompareXml(expected, record);
        }
    }
}