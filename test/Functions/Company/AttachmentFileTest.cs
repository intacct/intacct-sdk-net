/*
 * Copyright 2017 Intacct Corporation.
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

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.Company;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;

namespace Intacct.Sdk.Tests.Functions.Company
{

    [TestClass]
    public class AttachmentFileTest
    {
        [TestInitialize()]
        public void TestInitialize()
        {

        }

         [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<attachment>
    <attachmentname>input</attachmentname>
    <attachmenttype>csv</attachmenttype>
    <attachmentdata>aGVsbG8sd29ybGQKdW5pdCx0ZXN0</attachmentdata>
</attachment>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";
            
            var path = @"c:\csv\input.csv";

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { path, new MockFileData("hello,world\nunit,test") },
            });

            var attachment = new AttachmentFile(fileSystem);
            
            attachment.FilePath = path;
            
            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            attachment.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
        

    }

}
