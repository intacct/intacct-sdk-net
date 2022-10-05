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
using Intacct.SDK.Functions.AccountsReceivable;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsReceivable
{
    public class ArPaymentCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_arpayment>
        <customerid>C0020</customerid>
        <paymentamount>1922.12</paymentamount>
        <batchkey>123</batchkey>
        <refid>1000</refid>
        <overpaylocid>1020</overpaylocid>
        <overpaydeptid>900</overpaydeptid>
        <datereceived>
            <year>2016</year>
            <month>06</month>
            <day>30</day>
        </datereceived>
        <paymentmethod>Printed Check</paymentmethod>
    </create_arpayment>
</function>";

            ArPaymentCreate record = new ArPaymentCreate("unittest")
            {
                CustomerId = "C0020",
                TransactionPaymentAmount = 1922.12M,
                SummaryRecordNo = 123,
                ReceivedDate = new DateTime(2016, 06, 30),
                PaymentMethod = "Printed Check",
                ReferenceNumber = "1000",
                OverpaymentDepartmentId = "900",
                OverpaymentLocationId = "1020"
            };

            this.CompareXml(expected, record);
        }
    }
}