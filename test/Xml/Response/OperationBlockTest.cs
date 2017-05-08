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
using System.Collections.Generic;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Exceptions;
using Intacct.Sdk.Xml.Response.Operation;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml.Response
{

    [TestClass]
    public class OperationBlockTest
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
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>testControlId</controlid>
                  <data>
                        <api>
                              <sessionid>faKEsesSiOnId..</sessionid>
                              <endpoint>https://api.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            SynchronousResponse response = new SynchronousResponse(stream);
            OperationBlock operation = response.Operation;

            Assert.IsInstanceOfType(operation.Authentication, typeof(Authentication));
            Assert.IsInstanceOfType(operation.Results, typeof(List<Result>));
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(OperationException), "Response authentication status failure")]
        public void AuthenticationFailureTest()
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
      <operation>
            <authentication>
                  <status>failure</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
            </authentication>
            <errormessage>
                  <error>
                        <errorno>XL03000006</errorno>
                        <description></description>
                        <description2>Sign-in information is incorrect</description2>
                        <correction></correction>
                  </error>
            </errormessage>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            SynchronousResponse response = new SynchronousResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Operation block is missing authentication element")]
        public void MissingAuthenticationBlockTest()
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
      <operation/>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            SynchronousResponse response = new SynchronousResponse(stream);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Operation block is missing result element")]
        public void MissingResultBlockTest()
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
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <sessiontimestamp>2015-10-22T20:58:27-07:00</sessiontimestamp>
            </authentication>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            SynchronousResponse response = new SynchronousResponse(stream);
        }
    }
}
