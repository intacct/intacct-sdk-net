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

using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions
{
    public class ApiSessionCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <getAPISession />
</function>";

            ApiSessionCreate apiFunction = new ApiSessionCreate("unittest");
            
            this.CompareXml(expected, apiFunction);
        }
        
        [Fact]
        public void GetXmlWithLocationTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <getAPISession>
        <locationid>100</locationid>
    </getAPISession>
</function>";

            ApiSessionCreate apiFunction = new ApiSessionCreate("unittest");
            apiFunction.EntityId = "100";
            
            this.CompareXml(expected, apiFunction);
        }
        
        [Fact]
        public void GetXmlWithEmptyLocationTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <getAPISession>
        <locationid />
    </getAPISession>
</function>";

            ApiSessionCreate apiFunction = new ApiSessionCreate("unittest");
            apiFunction.EntityId = "";
            
            this.CompareXml(expected, apiFunction);
        }
    }
}