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
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Functions.Purchasing;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Purchasing
{
    public class PurchasingTransactionUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_potransaction key=""Purchase Order-PO0213"" />
</function>";

            PurchasingTransactionUpdate record = new PurchasingTransactionUpdate("unittest")
            {
                DocumentId = "Purchase Order-PO0213",
            };

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update_potransaction key=""20394"" externalkey=""true"">
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
        <updatepotransitems>
            <updatepotransitem line_num=""1"">
                <itemid>02354032</itemid>
            </updatepotransitem>
            <potransitem>
                <itemid>2390552</itemid>
                <quantity>223</quantity>
            </potransitem>
        </updatepotransitems>
        <updatesubtotals>
            <updatesubtotal>
                <description>Subtotal description</description>
                <total>223</total>
            </updatesubtotal>
        </updatesubtotals>
    </update_potransaction>
</function>";

            PurchasingTransactionUpdate record = new PurchasingTransactionUpdate("unittest")
            {
                DocumentId = "PO-2234",
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
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            PurchasingTransactionLineUpdate line1 = new PurchasingTransactionLineUpdate()
            {
                LineNo = 1,
                ItemId = "02354032",
            };

            record.Lines.Add(line1);

            PurchasingTransactionLineCreate line2 = new PurchasingTransactionLineCreate()
            {
                ItemId = "2390552",
                Quantity = 223,
            };
            record.Lines.Add(line2);

            TransactionSubtotalUpdate subtotal1 = new TransactionSubtotalUpdate()
            {
                Description = "Subtotal description",
                Total = 223,
            };
            record.Subtotals.Add(subtotal1);
            
            this.CompareXml(expected, record);
        }
    }
}