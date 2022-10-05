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
    public class ReadByNameTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readByName>
        <object>GLENTRY</object>
        <keys />
        <fields>*</fields>
        <returnFormat>xml</returnFormat>
    </readByName>
</function>";

            ReadByName record = new ReadByName("unittest")
            {
                ObjectName = "GLENTRY"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readByName>
        <object>GLENTRY</object>
        <keys>987</keys>
        <fields>TRX_AMOUNT,RECORDNO,BATCHNO</fields>
        <returnFormat>xml</returnFormat>
        <docparid>Sales Invoice</docparid>
    </readByName>
</function>";

            ReadByName record = new ReadByName("unittest")
            {
                ObjectName = "GLENTRY",
                Fields = new List<string>
                {
                    "TRX_AMOUNT",
                    "RECORDNO",
                    "BATCHNO",
                },
                Names = new List<string>
                {
                    "987",
                },
                DocParId = "Sales Invoice"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void TooManyNamesTest()
        {
            ReadByName record = new ReadByName("unittest")
            {
                ObjectName = "GLENTRY"
            };

            List<string> names = new List<string>();
            for (int i = 1; i <= 101; i++)
            {
                names.Add(i.ToString());
            }
            
            
            var ex = Record.Exception(() => record.Names = names);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Names count cannot exceed 100", ex.Message);
        }
    }
}