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
using Intacct.Sdk.Xml.Response.Operation;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml.Response.Operation
{

    [TestClass]
    public class AuthenticationTest
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
            Authentication auth = response.Operation.Authentication;
            Assert.AreEqual("success", auth.Status);
            Assert.AreEqual("fakeuser", auth.UserId);
            Assert.AreEqual("fakecompany", auth.CompanyId);
            Assert.AreEqual(false, auth.SlideInUser);
        }

        [TestMethod()]
        public void SlideInUserExternalLoginTest()
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
                  <userid>ExtUser|fakeconsole|fakeuser</userid>
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
            Authentication auth = response.Operation.Authentication;
            Assert.AreEqual("ExtUser|fakeconsole|fakeuser", auth.UserId);
            Assert.AreEqual(true, auth.SlideInUser);
        }

        [TestMethod()]
        public void SlideInCPAUserLoginTest()
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
                  <userid>CPAUser</userid>
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
            Authentication auth = response.Operation.Authentication;
            Assert.AreEqual("CPAUser", auth.UserId);
            Assert.AreEqual(true, auth.SlideInUser);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Authentication block is missing status element")]
        public void MissingStatusElementTest()
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
                  <!--<status>success</status>-->
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
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
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Authentication block is missing userid element")]
        public void MissingUserIdElementTest()
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
                  <!--<userid>fakeuser</userid>-->
                  <companyid>fakecompany</companyid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
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
        [ExpectedExceptionWithMessage(typeof(IntacctException), "Authentication block is missing companyid element")]
        public void MissingCompanyIdElementTest()
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
                  <!--<companyid>fakecompany</companyid>-->
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
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
