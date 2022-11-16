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
    public class ExpenseAdjustmentCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_expenseadjustmentreport>
        <employeeid>E0001</employeeid>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <expenseadjustments>
            <expenseadjustment>
                <glaccountno />
                <amount />
            </expenseadjustment>
        </expenseadjustments>
    </create_expenseadjustmentreport>
</function>";

            ExpenseAdjustmentCreate record = new ExpenseAdjustmentCreate("unittest")
            {
                EmployeeId = "E0001",
                TransactionDate = new DateTime(2015, 06, 30)
            };

            ExpenseAdjustmentLineCreate line1 = new ExpenseAdjustmentLineCreate();
            
            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_expenseadjustmentreport>
        <employeeid>E0001</employeeid>
        <datecreated>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </datecreated>
        <dateposted>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </dateposted>
        <batchkey>123</batchkey>
        <adjustmentno>ADJ001</adjustmentno>
        <docnumber>EXP001</docnumber>
        <description>For hotel</description>
        <basecurr>USD</basecurr>
        <currency>USD</currency>
        <expenseadjustments>
            <expenseadjustment>
                <glaccountno />
                <amount />
            </expenseadjustment>
        </expenseadjustments>
        <supdocid>AT122</supdocid>
    </create_expenseadjustmentreport>
</function>";

            ExpenseAdjustmentCreate record = new ExpenseAdjustmentCreate("unittest")
            {
                EmployeeId = "E0001",
                TransactionDate = new DateTime(2015, 06, 30),
                GlPostingDate = new DateTime(2015, 06, 30),
                SummaryRecordNo = 123,
                ExpenseAdjustmentNumber = "ADJ001",
                ExpenseReportNumber = "EXP001",
                Description = "For hotel",
                BaseCurrency = "USD",
                ReimbursementCurrency = "USD",
                AttachmentsId = "AT122"
            };            

            ExpenseAdjustmentLineCreate line1 = new ExpenseAdjustmentLineCreate();

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
}