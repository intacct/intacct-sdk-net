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
using Intacct.Sdk.Exceptions;

namespace Intacct.Sdk.Tests.Xml.Response
{

    [TestClass]
    public class ControlTest
    {

        [TestMethod()]
        public void SuccessTest()
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
            Control control = response.Control;
            Assert.AreEqual("success", control.Status);
            Assert.AreEqual("testsenderid", control.SenderId);
            Assert.AreEqual("ControlIdHere", control.ControlId);
            Assert.AreEqual("false", control.UniqueId);
            Assert.AreEqual("3.0", control.DtdVersion);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Control block is missing status element")]
        public void MissingStatusElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control />
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Control block is missing senderid element")]
        public void MissingSenderIdElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <!--<senderid>testsenderid</senderid>-->
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
        }
        
        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Control block is missing controlid element")]
        public void MissingControlIdElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <!--<controlid>ControlIdHere</controlid>-->
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
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Control block is missing uniqueid element")]
        public void MissingUniqueIdElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <!--<uniqueid>false</uniqueid>-->
            <dtdversion>3.0</dtdversion>
      </control>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Control block is missing dtdversion element")]
        public void MissingDtdVersionElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <uniqueid>false</uniqueid>
            <!--<dtdversion>3.0</dtdversion>-->
      </control>
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
