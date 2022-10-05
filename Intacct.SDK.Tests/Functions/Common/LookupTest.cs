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

using Intacct.SDK.Functions.Common;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common
{
    public class LookupTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <lookup>
        <object>CLASS</object>
    </lookup>
</function>";

            Lookup record = new Lookup("unittest")
            {
                Object = "CLASS"
            };

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetXmlTestDocParId()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <lookup>
        <object>CLASS</object>
        <docparid>41583</docparid>
    </lookup>
</function>";

            Lookup record = new Lookup("unittest")
            {
                Object = "CLASS",
                DocParId = "41583"
            };

            this.CompareXml(expected, record);
        }
    }
}