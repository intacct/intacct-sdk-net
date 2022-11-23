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
using Intacct.SDK.Functions.AccountsPayable;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsPayable
{
    public class BillLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lineitem>
    <glaccountno />
    <amount>76343.43</amount>
</lineitem>";

            BillLineCreate record = new BillLineCreate
            {
                TransactionAmount = 76343.43M
            };
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lineitem>
    <accountlabel>TestBill Account1</accountlabel>
    <offsetglaccountno>93590253</offsetglaccountno>
    <amount>76343.43</amount>
    <memo>Just another memo</memo>
    <locationid>Location1</locationid>
    <departmentid>Department1</departmentid>
    <item1099>true</item1099>
    <key>1234</key>
    <totalpaid>23484.93</totalpaid>
    <totaldue>0.00</totaldue>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <projectid>Project1</projectid>
    <taskid>Task1</taskid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
    <billable>true</billable>
</lineitem>";

            BillLineCreate record = new BillLineCreate() {
                AccountLabel = "TestBill Account1",
                OffsetGlAccountNumber = "93590253",
                TransactionAmount = 76343.43M,
                Memo = "Just another memo",
                Form1099 = true,
                Key = 1234,
                TotalPaid = 23484.93M,
                TotalDue = 0.00M,
                Billable = true,
                LocationId = "Location1",
                DepartmentId = "Department1",
                ProjectId = "Project1",
                TaskId = "Task1",
                CustomerId = "Customer1",
                VendorId = "Vendor1",
                EmployeeId = "Employee1",
                ItemId = "Item1",
                ClassId = "Class1",
                ContractId = "Contract1",
                WarehouseId = "Warehouse1",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };
            this.CompareXml(expected, record);
        }
    }
}