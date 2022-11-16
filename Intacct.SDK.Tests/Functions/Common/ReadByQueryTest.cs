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
using Intacct.SDK.Functions.Common.Query;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common
{
    public class ReadByQueryTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readByQuery>
        <object>CLASS</object>
        <query>RECORDNO &lt; 2</query>
        <fields>*</fields>
        <pagesize>1000</pagesize>
        <returnFormat>xml</returnFormat>
    </readByQuery>
</function>";

            ReadByQuery record = new ReadByQuery("unittest")
            {
                ObjectName = "CLASS",
                Query = new QueryString("RECORDNO < 2"),
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readByQuery>
        <object>CLASS</object>
        <query />
        <fields>RECORDNO</fields>
        <pagesize>100</pagesize>
        <returnFormat>xml</returnFormat>
        <docparid>255252235</docparid>
    </readByQuery>
</function>";

            ReadByQuery record = new ReadByQuery("unittest")
            {
                ObjectName = "CLASS",
                PageSize = 100,
                Fields = new List<string>
                {
                    "RECORDNO",
                },
                DocParId = "255252235"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void MinPageSizeTest()
        {
            ReadByQuery record = new ReadByQuery("unittest");

            var ex = Record.Exception(() => record.PageSize = 0);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Page Size cannot be less than 1", ex.Message);
        }
        
        [Fact]
        public void MaxPageSizeTest()
        {
            ReadByQuery record = new ReadByQuery("unittest");

            var ex = Record.Exception(() => record.PageSize = 1001);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Page Size cannot be greater than 1000", ex.Message);
        }
    }
}