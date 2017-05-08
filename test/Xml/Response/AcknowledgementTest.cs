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

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml.Response;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Exceptions;

namespace Intacct.Sdk.Tests.Xml.Response
{

    [TestClass]
    public class AcknowledgementTest
    {

        [TestMethod()]
        public void SuccessTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <acknowledgement>
            <status>success</status>
      </acknowledgement>
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

            AsynchronousResponse response = new AsynchronousResponse(stream);
            Assert.AreEqual("success", response.Acknowledgement.Status);
        }
        
        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Acknowledgement block is missing status element")]
        public void MissingStatusElementTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <acknowledgement />
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

            AsynchronousResponse response = new AsynchronousResponse(stream);
        }

    }

}
