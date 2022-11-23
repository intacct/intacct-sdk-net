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
using Intacct.SDK.Functions.EmployeeExpenses;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.EmployeeExpenses
{
    public class ExpenseReportLineCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<expense>
    <glaccountno />
</expense>";

            ExpenseReportLineCreate record = new ExpenseReportLineCreate();
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<expense>
    <glaccountno>7000</glaccountno>
    <amount>1025.99</amount>
    <currency>USD</currency>
    <trx_amount>76343.43</trx_amount>
    <exchratedate>
        <year>2016</year>
        <month>06</month>
        <day>30</day>
    </exchratedate>
    <exchratetype>Intacct Daily Rate</exchratetype>
    <expensedate>
        <year>2016</year>
        <month>06</month>
        <day>30</day>
    </expensedate>
    <memo>Marriott</memo>
    <form1099>true</form1099>
    <paidfor>Hotel</paidfor>
    <locationid>Location1</locationid>
    <departmentid>Department1</departmentid>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <projectid>Project1</projectid>
    <taskid>Task1</taskid>
    <costtypeid>CostType1</costtypeid>
    <customerid>Customer1</customerid>
    <vendorid>Vendor1</vendorid>
    <employeeid>Employee1</employeeid>
    <itemid>Item1</itemid>
    <classid>Class1</classid>
    <contractid>Contract1</contractid>
    <warehouseid>Warehouse1</warehouseid>
    <billable>true</billable>
    <exppmttype>AMEX</exppmttype>
    <quantity>10</quantity>
    <rate>12.34</rate>
</expense>";

            ExpenseReportLineCreate record = new ExpenseReportLineCreate() {
                GlAccountNumber = "7000",
                ReimbursementAmount = 1025.99M,
                TransactionCurrency = "USD",
                TransactionAmount = 76343.43M,
                ExchangeRateDate = new DateTime(2016, 06, 30),
                ExchangeRateType = "Intacct Daily Rate",
                ExpenseDate = new DateTime(2016, 06, 30),
                PaidTo = "Marriott",
                PaidFor = "Hotel",
                Form1099 = true,
                Billable = true,
                PaymentTypeName = "AMEX",
                Quantity = 10,
                UnitRate = 12.34M,
                LocationId = "Location1",
                DepartmentId = "Department1",
                ProjectId = "Project1",
                TaskId = "Task1",
                CostTypeId = "CostType1",
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