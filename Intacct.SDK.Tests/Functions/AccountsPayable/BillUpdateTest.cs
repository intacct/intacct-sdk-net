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
using Intacct.SDK.Functions.AccountsPayable;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsPayable
{
    public class BillUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_bill key=""1234"" />
</function>";

            BillUpdate record = new BillUpdate("unittest")
            {
                RecordNo = 1234,
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void BillLineUpdateTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_bill key=""1234"">
        <updatebillitems>
            <updatelineitem line_num=""1"">
                <glaccountno>7940</glaccountno>
            </updatelineitem>
            <lineitem>
                <glaccountno>4554</glaccountno>
                <amount>76343.43</amount>
            </lineitem>
        </updatebillitems>
    </update_bill>
</function>";

            BillUpdate record = new BillUpdate("unittest")
            {
                RecordNo= 1234,
            };
            
            BillLineUpdate line1 = new BillLineUpdate()
            {
                LineNo = 1,
                GlAccountNumber = "7940",
            };
             
            record.Lines.Add(line1);
                 
            BillLineCreate line2 = new BillLineCreate()
            {
                GlAccountNumber = "4554",
                TransactionAmount = 76343.43M
            };
                 
            record.Lines.Add(line2);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_bill key=""20394"" externalkey=""true"">
        <vendorid>VENDOR1</vendorid>
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
        <billno>234</billno>
        <ponumber>234235</ponumber>
        <onhold>true</onhold>
        <description>Some description</description>
        <payto>
            <contactname>28952</contactname>
        </payto>
        <returnto>
            <contactname>289533</contactname>
        </returnto>
        <currency>USD</currency>
        <exchratedate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </exchratedate>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <supdocid>6942</supdocid>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <updatebillitems>
            <lineitem>
                <glaccountno />
                <amount>76343.43</amount>
            </lineitem>
        </updatebillitems>
    </update_bill>
</function>";

            BillUpdate record = new BillUpdate("unittest")
            {
                RecordNo = 1234,
                VendorId = "VENDOR1",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                DueDate = new DateTime(2020, 09, 24),
                PaymentTerm = "N30",
                Action = "Submit",
                BillNumber = "234",
                ReferenceNumber = "234235",
                OnHold = true,
                Description = "Some description",
                ExternalId = "20394",
                PayToContactName = "28952",
                ReturnToContactName = "289533",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                AttachmentsId = "6942",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                },
            };


            BillLineCreate line1 = new BillLineCreate
            {
                TransactionAmount = 76343.43M
            };

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
} 

