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
    public class TransactionItemDetailTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<itemdetail>
    <quantity>5523</quantity>
    <lotno>223</lotno>
    <itemexpiration>
        <year>2017</year>
        <month>12</month>
        <day>31</day>
    </itemexpiration>
</itemdetail>";

            TransactionItemDetail record = new TransactionItemDetail()
            {
                Quantity = 5523,
                LotNumber = "223",
                ItemExpiration = new DateTime(2017, 12, 31),
            };
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<itemdetail>
    <quantity>15325</quantity>
    <serialno>S2355235</serialno>
    <aisle>55</aisle>
    <row>1</row>
    <bin>12</bin>
</itemdetail>";

            TransactionItemDetail record = new TransactionItemDetail()
            {
                Quantity = 15325,
                SerialNumber = "S2355235",
                Aisle = "55",
                Row = "1",
                Bin = "12",
            };
            
            this.CompareXml(expected, record);
        }
    }
}