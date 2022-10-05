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
    public class WarehouseTransferLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ICTRANSFERITEM>
    <IN_OUT>I</IN_OUT>
    <ITEMID>321654</ITEMID>
    <WAREHOUSEID>9</WAREHOUSEID>
    <MEMO>test memo</MEMO>
    <QUANTITY>500</QUANTITY>
    <UNIT>b7</UNIT>
    <LOCATIONID>7</LOCATIONID>
    <DEPARTMENTID>6</DEPARTMENTID>
    <PROJECTID>5</PROJECTID>
    <CUSTOMERID>4</CUSTOMERID>
    <VENDORID>3</VENDORID>
    <EMPLOYEEID>2</EMPLOYEEID>
    <CLASSID>1</CLASSID>
</ICTRANSFERITEM>";

            WarehouseTransferLineCreate record = new WarehouseTransferLineCreate
            {
                InOut = "I",
                ItemId = "321654",
                WarehouseId = "9",
                Memo = "test memo",
                Quantity = 500,
                Unit = "b7",
                LocationId = "7",
                DepartmentId = "6",
                ProjectId = "5",
                CustomerId = "4",
                VendorId = "3",
                EmployeeId = "2",
                ClassId = "1"
            };

            this.CompareXml(expected, record);
        }
    }
}