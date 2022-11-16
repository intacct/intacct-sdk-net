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
    public class UserUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update>
        <USERINFO>
            <LOGINID>U1234</LOGINID>
        </USERINFO>
    </update>
</function>";

            UserUpdate record = new UserUpdate("unittest")
            {
                UserId = "U1234"
            };

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void RestrictionsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update>
        <USERINFO>
            <LOGINID>U1234</LOGINID>
            <USERLOCATIONS>
                <LOCATIONID>E100</LOCATIONID>
            </USERLOCATIONS>
            <USERLOCATIONS>
                <LOCATIONID>E200</LOCATIONID>
            </USERLOCATIONS>
            <USERDEPARTMENTS>
                <DEPARTMENTID>D100</DEPARTMENTID>
            </USERDEPARTMENTS>
            <USERDEPARTMENTS>
                <DEPARTMENTID>D200</DEPARTMENTID>
            </USERDEPARTMENTS>
        </USERINFO>
    </update>
</function>";

            UserUpdate record = new UserUpdate("unittest")
            {
                UserId = "U1234",
                RestrictedEntities = new List<string>
                {
                    "E100",
                    "E200",
                },
                RestrictedDepartments = new List<string>
                {
                    "D100",
                    "D200",
                }
            };

            this.CompareXml(expected, record);
        }
    }
}