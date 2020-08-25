/*
 * Copyright 2020 Sage Intacct, Inc.
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

using System.IO;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Response
{
    public class ControlTest
    {
        [Fact]
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
            Assert.Equal("testsenderid", control.SenderId);
            Assert.Equal("ControlIdHere", control.ControlId);
            Assert.Equal("false", control.UniqueId);
            Assert.Equal("3.0", control.DtdVersion);
        }

        [Fact]
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

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Control block is missing status element", ex.Message);
        }
    }
}