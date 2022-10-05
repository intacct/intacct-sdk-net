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
    public class ReimbursementRequestCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_reimbursementrequest>
        <bankaccountid>BA1143</bankaccountid>
        <employeeid>E0001</employeeid>
        <paymentmethod>Printed Check</paymentmethod>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <eppaymentrequestitems>
            <eppaymentrequestitem>
                <key>123</key>
                <paymentamount>100.12</paymentamount>
            </eppaymentrequestitem>
        </eppaymentrequestitems>
    </create_reimbursementrequest>
</function>";

            ReimbursementRequestCreate record = new ReimbursementRequestCreate("unittest")
            {
                BankAccountId = "BA1143",
                EmployeeId = "E0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30)
            };

            ReimbursementRequestItem line1 = new ReimbursementRequestItem
            {
                ApplyToRecordId = 123,
                AmountToApply = 100.12M
            };

            record.ApplyToTransactions.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
}