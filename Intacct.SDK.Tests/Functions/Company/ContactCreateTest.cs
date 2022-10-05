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
using Intacct.SDK.Functions.Company;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Company
{
    public class ContactCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <CONTACT>
            <CONTACTNAME>hello</CONTACTNAME>
            <PRINTAS>world</PRINTAS>
        </CONTACT>
    </create>
</function>";

            ContactCreate record = new ContactCreate("unittest")
            {
                ContactName = "hello",
                PrintAs = "world"
            };
            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetXmlTestIsoCountryCodeTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <CONTACT>
            <CONTACTNAME>hello</CONTACTNAME>
            <PRINTAS>world</PRINTAS>
            <MAILADDRESS>
                <ISOCOUNTRYCODE>USA</ISOCOUNTRYCODE>
            </MAILADDRESS>
        </CONTACT>
    </create>
</function>";

            ContactCreate record = new ContactCreate("unittest")
            {
                ContactName = "hello",
                PrintAs = "world",
                IsoCountryCode = "USA"
            };
            this.CompareXml(expected, record);
        }
    }
}