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
using Xunit;

namespace Intacct.SDK.Tests.Xml
{
    public class OfflineResponseTest
    {
        [Fact]
        public void GetAcknowledgementTest()
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

            OfflineResponse response = new OfflineResponse(stream);
            Assert.Equal("success", response.Status);
        }
        
        [Fact]
        public void MissingAcknowledgementBlockTest()
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

            var ex = Record.Exception(() => new OfflineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Response is missing acknowledgement block", ex.Message);
        }
        
        [Fact]
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

            var ex = Record.Exception(() => new OfflineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Acknowledgement block is missing status element", ex.Message);
        }
    }
}