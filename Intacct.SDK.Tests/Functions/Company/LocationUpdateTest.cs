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
    public class LocationUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update>
        <LOCATION>
            <LOCATIONID>L1234</LOCATIONID>
            <NAME>hello world</NAME>
            <STARTDATE>02/01/2015</STARTDATE>
            <ENDDATE />
        </LOCATION>
    </update>
</function>";

            LocationUpdate record = new LocationUpdate("unittest")
            {
                LocationId = "L1234",
                LocationName = "hello world",
                StartDate = new System.DateTime(2015, 2, 1),
                EndDate = new System.DateTime()
            };
            this.CompareXml(expected, record);
        }
    }
}