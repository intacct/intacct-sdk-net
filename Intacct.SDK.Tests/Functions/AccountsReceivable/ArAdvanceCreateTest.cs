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
using Intacct.SDK.Functions.AccountsReceivable;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsReceivable
{
    public class ArAdvanceCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <ARADVANCE>
            <CUSTOMERID>CUSTOMER1</CUSTOMERID>
            <PAYMENTMETHOD>Cash</PAYMENTMETHOD>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <RECEIPTDATE>06/30/2015</RECEIPTDATE>
            <ARADVANCEITEMS>
                <ARADVANCEITEM>
                    <TRX_AMOUNT>76343.43</TRX_AMOUNT>
                </ARADVANCEITEM>
            </ARADVANCEITEMS>
        </ARADVANCE>
    </create>
</function>";

            ArAdvanceCreate record = new ArAdvanceCreate("unittest")
            {
                CustomerId = "CUSTOMER1",
                PaymentMethod = "Cash",
                PaymentDate = new DateTime(2015, 06, 30),
                ReceiptDate = new DateTime(2015, 06, 30)
            };

            ArAdvanceLine line1 = new ArAdvanceLine
            {
                TransactionAmount = 76343.43M
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
        <ARADVANCE>
            <CUSTOMERID>CUSTOMER1</CUSTOMERID>
            <PAYMENTMETHOD>Cash</PAYMENTMETHOD>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <RECEIPTDATE>06/30/2015</RECEIPTDATE>
            <FINANCIALENTITY>Entity1</FINANCIALENTITY>
            <UNDEPOSITEDACCOUNTNO>20323</UNDEPOSITEDACCOUNTNO>
            <PRBATCH>234</PRBATCH>
            <DOCNUMBER>234235</DOCNUMBER>
            <DESCRIPTION>Some description</DESCRIPTION>
            <CURRENCY>USD</CURRENCY>
            <BASECURR>USD</BASECURR>
            <EXCH_RATE_DATE>06/30/2015</EXCH_RATE_DATE>
            <EXCH_RATE_TYPE_ID>Intacct Daily Rate</EXCH_RATE_TYPE_ID>
            <EXCHANGE_RATE>1.25</EXCHANGE_RATE>
            <SUPDOCID>6942</SUPDOCID>
            <ACTION>Submit</ACTION>
            <ARADVANCEITEMS>
                <ARADVANCEITEM>
                    <ACCOUNTNO>123456</ACCOUNTNO>
                    <ACCOUNTLABEL>Label1</ACCOUNTLABEL>
                    <TRX_AMOUNT>76343.43</TRX_AMOUNT>
                    <ENTRYDESCRIPTION>Description</ENTRYDESCRIPTION>
                    <LOCATIONID>123</LOCATIONID>
                    <DEPARTMENTID>321</DEPARTMENTID>
                    <PROJECTID>Project1</PROJECTID>
                    <TASKID>Task1</TASKID>
                    <CUSTOMERID>Customer1</CUSTOMERID>
                    <VENDORID>Vendor1</VENDORID>
                    <EMPLOYEEID>Emp1</EMPLOYEEID>
                    <ITEMID>Item1</ITEMID>
                    <CLASSID>Class1</CLASSID>
                    <CONTRACTID>Contract1</CONTRACTID>
                    <WAREHOUSEID>Warehouse1</WAREHOUSEID>
                    <GLDIM>654</GLDIM>
                </ARADVANCEITEM>
            </ARADVANCEITEMS>
        </ARADVANCE>
    </create>
</function>";

            ArAdvanceCreate record = new ArAdvanceCreate("unittest")
            {
                CustomerId = "CUSTOMER1",
                PaymentMethod = "Cash",
                PaymentDate = new DateTime(2015, 06, 30),
                ReceiptDate = new DateTime(2015, 06, 30),
                FinancialEntity = "Entity1",
                UndepositedAccountNo = "20323",
                PrBatch = "234",
                DocNumber = "234235",
                Description = "Some description",
                Currency = "USD",
                BaseCurrency = "USD",
                ExchangeRateDate = new DateTime(2015, 06, 30),
                ExchangeRateTypeId = "Intacct Daily Rate",
                ExchangeRate = 1.25M,
                AttachmentId = "6942",
                Action = "Submit",
            };


            ArAdvanceLine line1 = new ArAdvanceLine
            {
                AccountNo = "123456",
                AccountLabel = "Label1",
                TransactionAmount = 76343.43M,
                EntryDescription = "Description",
                LocationId = "123",
                DepartmentId = "321",
                ProjectId = "Project1",
                TaskId = "Task1",
                CustomerId = "Customer1",
                VendorId = "Vendor1",
                EmployeeId = "Emp1",
                ItemId = "Item1",
                ClassId = "Class1",
                ContractId = "Contract1",
                WarehouseId = "Warehouse1",
                GlDim = 654
            };

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
}
