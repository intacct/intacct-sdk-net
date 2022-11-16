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
    public class WarehouseTransferCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <ICTRANSFER>
            <TRANSACTIONDATE>06/30/2015</TRANSACTIONDATE>
            <REFERENCENO>654789</REFERENCENO>
            <DESCRIPTION>test desc</DESCRIPTION>
            <ICTRANSFERITEMS>
                <ICTRANSFERITEM>
                    <ITEMID>321654</ITEMID>
                </ICTRANSFERITEM>
            </ICTRANSFERITEMS>
        </ICTRANSFER>
    </create>
</function>";

            WarehouseTransferCreate record = new WarehouseTransferCreate("unittest")
            {
                TransactionDate = new DateTime(2015, 06, 30),
                ReferenceNumber = "654789",
                Description = "test desc"
            };

            WarehouseTransferLineCreate line1 = new WarehouseTransferLineCreate
            {
                ItemId = "321654"
            };
            
            record.Lines.Add(line1);

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <ICTRANSFER>
            <TRANSACTIONDATE>06/30/2015</TRANSACTIONDATE>
            <REFERENCENO>654789</REFERENCENO>
            <DESCRIPTION>test desc</DESCRIPTION>
            <TRANSFERTYPE>Immediate</TRANSFERTYPE>
            <ACTION>Post</ACTION>
            <OUTDATE>06/30/2015</OUTDATE>
            <INDATE>06/30/2015</INDATE>
            <EXCH_RATE_TYPE_ID>Intacct Daily Rate</EXCH_RATE_TYPE_ID>
            <EXCH_RATE_DATE>06/30/2015</EXCH_RATE_DATE>
            <EXCHANGE_RATE>100</EXCHANGE_RATE>
            <ICTRANSFERITEMS>
                <ICTRANSFERITEM>
                    <ITEMID>321654</ITEMID>
                </ICTRANSFERITEM>
            </ICTRANSFERITEMS>
        </ICTRANSFER>
    </create>
</function>";
            
            WarehouseTransferCreate record = new WarehouseTransferCreate("unittest")
            {
                TransactionDate = new DateTime(2015, 06, 30),
                ReferenceNumber = "654789",
                Description = "test desc",
                TransferType = "Immediate",
                Action = "Post",
                OutDate = new DateTime(2015, 06, 30),
                InDate = new DateTime(2015, 06, 30),
                ExchangeRateTypeId = "Intacct Daily Rate",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRate = 100
            };

            WarehouseTransferLineCreate line1 = new WarehouseTransferLineCreate
            {
                ItemId = "321654"
            };
            record.Lines.Add(line1);

            this.CompareXml(expected, record);
        }
    }
}