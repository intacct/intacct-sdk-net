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
using Intacct.SDK.Functions.ContractsRevMgmt;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.ContractsRevMgmt
{
    public class ContractLineResumeTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <resume>
        <CONTRACTDETAIL>
            <RECORDNO>123</RECORDNO>
            <ASOFDATE>07/31/2017</ASOFDATE>
            <BILLING>true</BILLING>
            <REVENUE>true</REVENUE>
            <EXPENSE>true</EXPENSE>
        </CONTRACTDETAIL>
    </resume>
</function>";

            ContractLineResume record = new ContractLineResume("unittest")
            {
                RecordNo = 123,
                AsOfDate = new DateTime(2017, 7, 31),
                ResumeBilling = true,
                ResumeRevenue = true,
                ResumeExpense = true,
            };
            this.CompareXml(expected, record);
        }
    }
}