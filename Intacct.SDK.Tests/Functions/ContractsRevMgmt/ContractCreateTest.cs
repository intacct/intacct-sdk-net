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
    public class ContractCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <CONTRACT>
            <CONTRACTID>CT1234</CONTRACTID>
            <CUSTOMERID>C1234</CUSTOMERID>
            <NAME>Contract name</NAME>
            <BEGINDATE>01/01/2017</BEGINDATE>
            <ENDDATE>12/31/2017</ENDDATE>
            <BILLINGFREQUENCY>Monthly</BILLINGFREQUENCY>
            <TERMNAME>N30</TERMNAME>
        </CONTRACT>
    </create>
</function>";

            ContractCreate record = new ContractCreate("unittest")
            {
                ContractId = "CT1234",
                CustomerId = "C1234",
                ContractName = "Contract name",
                BeginDate = new DateTime(2017, 01, 01),
                EndDate = new DateTime(2017, 12, 31),
                BillingFrequency = "Monthly",
                PaymentTerm = "N30",
            };
            this.CompareXml(expected, record);
        }
    }
}