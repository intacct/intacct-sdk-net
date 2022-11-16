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
using Intacct.SDK.Functions.Common;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common
{
    public class ReadTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <read>
        <object>CLASS</object>
        <keys />
        <fields>*</fields>
        <returnFormat>xml</returnFormat>
    </read>
</function>";

            Read record = new Read("unittest")
            {
                ObjectName = "CLASS"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <read>
        <object>CLASS</object>
        <keys>1,2</keys>
        <fields>Field1,Field2</fields>
        <returnFormat>xml</returnFormat>
        <docparid>Sales Invoice</docparid>
    </read>
</function>";

            Read record = new Read("unittest")
            {
                ObjectName = "CLASS",
                Fields = new List<string>
                {
                    "Field1",
                    "Field2",
                },
                Keys = new List<int>
                {
                    1,
                    2,
                },
                DocParId = "Sales Invoice"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void TooManyKeysTest()
        {
            Read record = new Read("unittest")
            {
                ObjectName = "CLASS"
            };

            List<int> keys = new List<int>();
            for (int i = 1; i <= 101; i++)
            {
                keys.Add(i);
            }
            
            
            var ex = Record.Exception(() => record.Keys = keys);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Keys count cannot exceed 100", ex.Message);
        }
    }
}