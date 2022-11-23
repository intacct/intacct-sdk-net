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
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Functions.OrderEntry;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.OrderEntry
{
    public class RecurringOrderEntryTransactionCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_recursotransaction>
        <transactiontype>Sales Order</transactiontype>
        <customerid>2530</customerid>
        <startdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </startdate>
        <recursotransitems>
            <recursotransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </recursotransitem>
        </recursotransitems>
    </create_recursotransaction>
</function>";

            RecurringOrderEntryTransactionCreate record = new RecurringOrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                StartDate = new DateTime(2015, 06, 30),
                CustomerId = "2530",
            };

            RecurringOrderEntryTransactionLineCreate line1 = new RecurringOrderEntryTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void SubtotalEntryTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_recursotransaction>
        <transactiontype>Sales Order</transactiontype>
        <customerid>2530</customerid>
        <startdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </startdate>
        <recursotransitems>
            <recursotransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </recursotransitem>
        </recursotransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal Description</description>
                <total>1200</total>
            </subtotal>
        </subtotals>
    </create_recursotransaction>
</function>";

            RecurringOrderEntryTransactionCreate record = new RecurringOrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                StartDate = new DateTime(2015, 06, 30),
                CustomerId = "2530",
            };

            RecurringOrderEntryTransactionLineCreate line1 = new RecurringOrderEntryTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };
            record.Lines.Add(line1);

            TransactionSubtotalCreate subtotal1 = new TransactionSubtotalCreate()
            {
                Description = "Subtotal Description",
                Total = 1200,
            };
            record.Subtotals.Add(subtotal1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_recursotransaction>
        <transactiontype>Sales Order</transactiontype>
        <customerid>23530</customerid>
        <referenceno>234235</referenceno>
        <termname>N30</termname>
        <message>Submit</message>
        <contractid>25394</contractid>
        <contractdesc>Description of Contract</contractdesc>
        <customerponumber>123456</customerponumber>
        <shippingmethod>USPS</shippingmethod>
        <shipto>
            <contactname>28952</contactname>
        </shipto>
        <billto>
            <contactname>289533</contactname>
        </billto>
        <externalid>20394</externalid>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <exchratetype>Intacct Daily Rate</exchratetype>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <taxsolutionid>taxing</taxsolutionid>
        <paymethod>Credit Card</paymethod>
        <payinfull>true</payinfull>
        <paymentamount>100</paymentamount>
        <creditcardtype>VISA</creditcardtype>
        <customercreditcardkey>321654</customercreditcardkey>
        <customerbankaccountkey>64987</customerbankaccountkey>
        <accounttype>Bank</accounttype>
        <bankaccountid>321654987</bankaccountid>
        <startdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </startdate>
        <ending>
            <enddate>
                <year>2015</year>
                <month>06</month>
                <day>30</day>
            </enddate>
        </ending>
        <modenew>Months</modenew>
        <interval>12</interval>
        <eom>false</eom>
        <status>active</status>
        <docstatus>active</docstatus>
        <recursotransitems>
            <recursotransitem>
                <itemid>2390552</itemid>
                <quantity>223</quantity>
            </recursotransitem>
        </recursotransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal description</description>
                <total>223</total>
            </subtotal>
        </subtotals>
    </create_recursotransaction>
</function>";

            RecurringOrderEntryTransactionCreate record = new RecurringOrderEntryTransactionCreate("unittest")
            {
                TransactionDefinition = "Sales Order",
                CustomerId = "23530",
                ReferenceNumber = "234235",
                PaymentTerm = "N30",
                Message = "Submit",
                ContractId = "25394",
                ContractDescription = "Description of Contract",
                CustomerPoNumber = "123456",
                ShippingMethod = "USPS",
                ShipToContactName = "28952",
                BillToContactName = "289533",
                ExternalId = "20394",
                BaseCurrency = "USD",
                TransactionCurrency = "USD",
                ExchangeRateType = "Intacct Daily Rate",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                },
                TaxSolutionId = "taxing",
                PayMethod = "Credit Card",
                IsPayInFull = true,
                PaymentAmount = 100,
                CreditCardType = "VISA",
                CustomerCreditCardKey = 321654,
                CustomerBankAccountKey = 64987,
                AccountType = "Bank",
                BankAccountId = "321654987",
                GlAccountKey = "654321987",
                StartDate = new DateTime(2015, 06, 30),
                EndDate = new DateTime(2015, 06, 30),
                RepeatMode = "Months",
                RepeatInterval = 12,
                IsEndOfMonth = false,
                Status = "active",
                DocStatus = "active"
            };

            RecurringOrderEntryTransactionLineCreate line1 = new RecurringOrderEntryTransactionLineCreate()
            {
                ItemId = "2390552",
                Quantity = 223,
            };
            record.Lines.Add(line1);

            TransactionSubtotalCreate subtotal1 = new TransactionSubtotalCreate()
            {
                Description = "Subtotal description",
                Total = 223,
            };
            record.Subtotals.Add(subtotal1);
            
            this.CompareXml(expected, record);
        }
    }
}