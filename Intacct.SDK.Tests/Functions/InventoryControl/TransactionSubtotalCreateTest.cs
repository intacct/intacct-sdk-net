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
    public class TransactionSubtotalCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<subtotal>
    <description>Subtotal Description</description>
    <total>2340</total>
</subtotal>";

            TransactionSubtotalCreate record = new TransactionSubtotalCreate()
            {
                Description = "Subtotal Description",
                Total = 2340,
            };
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<subtotal>
    <description>Subtotal Description</description>
    <total>4202</total>
    <percentval>9.2</percentval>
    <locationid>2355</locationid>
    <departmentid>RCVNG</departmentid>
    <projectid>FOW</projectid>
    <customerid>CUST5893</customerid>
    <vendorid>VEN53222</vendorid>
    <employeeid>EM5925</employeeid>
    <classid>CLS322</classid>
    <itemid>I5266235</itemid>
    <contractid>C23662</contractid>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
</subtotal>";

            TransactionSubtotalCreate record = new TransactionSubtotalCreate()
            {
                Description = "Subtotal Description",
                Total = 4202,
                PercentageValue = 9.2M,
                LocationId = "2355",
                DepartmentId = "RCVNG",
                ProjectId = "FOW",
                CustomerId = "CUST5893",
                VendorId = "VEN53222",
                EmployeeId = "EM5925",
                ClassId = "CLS322",
                ItemId = "I5266235",
                ContractId = "C23662",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };
            
            this.CompareXml(expected, record);
        }
    }
}