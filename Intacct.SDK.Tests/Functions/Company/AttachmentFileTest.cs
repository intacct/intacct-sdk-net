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
using Intacct.SDK.Functions.Company;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Company
{
    public class AttachmentFileTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string fileData = @"hello,world
unit,test";
            
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<attachment>
    <attachmentname>input</attachmentname>
    <attachmenttype>csv</attachmenttype>
    <attachmentdata>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(fileData)) + @"</attachmentdata>
</attachment>";

            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "Functions", "Company", "File", "input.csv");

            AttachmentFile record = new AttachmentFile()
            {
                FilePath = tempFile,
            };
            
            this.CompareXml(expected, record);
        }
    }
}