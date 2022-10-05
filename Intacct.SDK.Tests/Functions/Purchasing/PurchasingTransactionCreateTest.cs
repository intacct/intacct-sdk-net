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
using Intacct.SDK.Functions.Purchasing;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Purchasing
{
    public class PurchasingTransactionCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_potransaction>
        <transactiontype>Purchase Order</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <vendorid>2530</vendorid>
        <datedue>
            <year>2019</year>
            <month>09</month>
            <day>15</day>
        </datedue>
        <returnto>
            <contactname />
        </returnto>
        <payto>
            <contactname />
        </payto>
        <potransitems>
            <potransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </potransitem>
        </potransitems>
    </create_potransaction>
</function>";

            PurchasingTransactionCreate record = new PurchasingTransactionCreate("unittest")
            {
                TransactionDefinition = "Purchase Order",
                TransactionDate = new DateTime(2015, 06, 30),
                VendorId = "2530",
                DueDate = new DateTime(2019, 09, 15),
            };

            PurchasingTransactionLineCreate line1 = new PurchasingTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_potransaction>
        <transactiontype>Purchase Order</transactiontype>
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
        <createdfrom>Purchase Order-P1002</createdfrom>
        <vendorid>23530</vendorid>
        <documentno>23430</documentno>
        <referenceno>234235</referenceno>
        <termname>N30</termname>
        <datedue>
            <year>2020</year>
            <month>09</month>
            <day>24</day>
        </datedue>
        <message>Submit</message>
        <shippingmethod>USPS</shippingmethod>
        <returnto>
            <contactname>Bobbi Reese</contactname>
        </returnto>
        <payto>
            <contactname>Henry Jones</contactname>
        </payto>
        <supdocid>6942</supdocid>
        <externalid>20394</externalid>
        <basecurr>USD</basecurr>
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
        <state>Pending</state>
        <projectid>Project1</projectid>
        <potransitems>
            <potransitem>
                <itemid>2390552</itemid>
                <quantity>223</quantity>
            </potransitem>
        </potransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal description</description>
                <total>223</total>
            </subtotal>
        </subtotals>
    </create_potransaction>
</function>";

            PurchasingTransactionCreate record = new PurchasingTransactionCreate("unittest")
            {
                TransactionDefinition = "Purchase Order",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                CreatedFrom = "Purchase Order-P1002",
                VendorId = "23530",
                DocumentNumber = "23430",
                ReferenceNumber = "234235",
                PaymentTerm = "N30",
                DueDate = new DateTime(2020, 09, 24),
                Message = "Submit",
                ShippingMethod = "USPS",
                ReturnToContactName = "Bobbi Reese",
                PayToContactName = "Henry Jones",
                AttachmentsId = "6942",
                ExternalId = "20394",
                BaseCurrency = "USD",
                TransactionCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                State = "Pending",
                ProjectId = "Project1",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            PurchasingTransactionLineCreate line1 = new PurchasingTransactionLineCreate()
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
        
        [Fact]
        public void GetXmlDateDueNullTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_potransaction>
        <transactiontype>Purchase Order</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <vendorid>2530</vendorid>
        <termname>N30</termname>
        <returnto>
            <contactname />
        </returnto>
        <payto>
            <contactname />
        </payto>
        <potransitems>
            <potransitem>
                <itemid>02354032</itemid>
                <quantity>1200</quantity>
            </potransitem>
        </potransitems>
    </create_potransaction>
</function>";

            PurchasingTransactionCreate record = new PurchasingTransactionCreate("unittest")
            {
                TransactionDefinition = "Purchase Order",
                TransactionDate = new DateTime(2015, 06, 30),
                VendorId = "2530",
                PaymentTerm = "N30",
            };

            PurchasingTransactionLineCreate line1 = new PurchasingTransactionLineCreate()
            {
                ItemId = "02354032",
                Quantity = 1200,
            };

            record.Lines.Add(line1);

            this.CompareXml(expected, record);
        }
    }
}