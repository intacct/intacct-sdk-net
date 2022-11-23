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
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.InventoryControl
{
    public class InventoryTransactionCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_ictransaction>
        <transactiontype>Purchase Order</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <ictransitems>
            <ictransitem>
                <itemid>02354032</itemid>
                <warehouseid>W1234</warehouseid>
                <quantity>1200</quantity>
            </ictransitem>
        </ictransitems>
    </create_ictransaction>
</function>";

            InventoryTransactionCreate record = new InventoryTransactionCreate("unittest")
            {
                TransactionDefinition = "Purchase Order",
                TransactionDate = new DateTime(2015, 06, 30),
            };

            InventoryTransactionLineCreate line1 = new InventoryTransactionLineCreate()
            {
                ItemId = "02354032",
                WarehouseId = "W1234",
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
    <create_ictransaction>
        <transactiontype>Inventory Shipper</transactiontype>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <createdfrom>Inventory Shipper-P1002</createdfrom>
        <documentno>23430</documentno>
        <referenceno>234235</referenceno>
        <message>Submit</message>
        <externalid>20394</externalid>
        <basecurr>USD</basecurr>
        <customfields>
            <customfield>
                <customfieldname>customfield1</customfieldname>
                <customfieldvalue>customvalue1</customfieldvalue>
            </customfield>
        </customfields>
        <state>Pending</state>
        <ictransitems>
            <ictransitem>
                <itemid>2390552</itemid>
                <warehouseid>W1234</warehouseid>
                <quantity>223</quantity>
            </ictransitem>
        </ictransitems>
        <subtotals>
            <subtotal>
                <description>Subtotal description</description>
                <total>223</total>
            </subtotal>
        </subtotals>
    </create_ictransaction>
</function>";

            InventoryTransactionCreate record = new InventoryTransactionCreate("unittest")
            {
                TransactionDefinition = "Inventory Shipper",
                TransactionDate = new DateTime(2015, 06, 30),
                CreatedFrom = "Inventory Shipper-P1002",
                DocumentNumber = "23430",
                ReferenceNumber = "234235",
                Message = "Submit",
                ExternalId = "20394",
                BaseCurrency = "USD",
                State = "Pending",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            InventoryTransactionLineCreate line1 = new InventoryTransactionLineCreate()
            {
                ItemId = "2390552",
                WarehouseId = "W1234",
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