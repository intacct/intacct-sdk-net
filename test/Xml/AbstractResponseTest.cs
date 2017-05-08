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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Intacct.Sdk.Xml.Response;
using Intacct.Sdk.Tests.Helpers;
using System.Xml;
using Intacct.Sdk.Exceptions;

namespace Intacct.Sdk.Tests.Xml
{

    [TestClass]
    public class AbstractResponseTest
    {

        [TestMethod()]
        public void GetControlBlockTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
            Assert.IsInstanceOfType(response.Control, typeof(Control));
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(XmlException), "The 'bad' start tag on line 1 position 2 does not match the end tag of 'xml'. Line 1, position 8.")]
        public void InvalidXmlResponseTest()
        {
            string xml = @"<bad></xml>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Response is missing control block")]
        public void MissingControlBlockTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <nocontrolblock/>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ResponseException), "Response control status failure")]
        public void ControlBlockFailureTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>failure</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <errormessage>
            <error>
                  <errorno>XL03000006</errorno>
                  <description></description>
                  <description2>test is not a valid transport policy.</description2>
                  <correction></correction>
            </error>
      </errormessage>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
        }

    }

}
