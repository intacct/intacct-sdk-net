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
    public class ReadMoreTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readMore>
        <resultId>6465763031VyprCMCoHYQAAGr@aRsAAAAU4</resultId>
    </readMore>
</function>";

            ReadMore record = new ReadMore("unittest")
            {
                ResultId = "6465763031VyprCMCoHYQAAGr@aRsAAAAU4"
            };

            this.CompareXml(expected, record);
        }

        [Fact]
        public void NoResultIdTest()
        {
            ReadMore record = new ReadMore("unittest");
            
            var ex = Record.Exception(() => this.CompareXml("N/A", record));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Result ID is required for read more", ex.Message);
        }
    }
}