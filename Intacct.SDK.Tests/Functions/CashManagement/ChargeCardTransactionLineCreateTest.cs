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
    public class ChargeCardTransactionLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ccpayitem>
    <glaccountno />
    <paymentamount>76343.43</paymentamount>
</ccpayitem>";

            ChargeCardTransactionLineCreate record = new ChargeCardTransactionLineCreate
            {
                TransactionAmount = 76343.43M
            };
            
            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ccpayitem>
    <accountlabel>TestBill Account1</accountlabel>
    <description>Just another memo</description>
    <paymentamount>76343.43</paymentamount>
    <departmentid>Department1</departmentid>
    <locationid>Location1</locationid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <projectid>Project1</projectid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
</ccpayitem>";

            ChargeCardTransactionLineCreate record = new ChargeCardTransactionLineCreate
            {
                TransactionAmount = 76343.43M,
                AccountLabel = "TestBill Account1",
                Memo = "Just another memo",
                LocationId = "Location1",
                DepartmentId = "Department1",
                ProjectId = "Project1",
                CustomerId = "Customer1",
                VendorId = "Vendor1",
                EmployeeId = "Employee1",
                ItemId = "Item1",
                ClassId = "Class1",
                ContractId = "Contract1",
                WarehouseId = "Warehouse1",
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            this.CompareXml(expected, record);
        }
    }
}