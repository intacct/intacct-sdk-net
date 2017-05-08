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
using System;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Credentials;
using Intacct.Sdk.Xml.Request;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace Intacct.Sdk.Tests
{

    [TestClass()]
    public class IntacctClientTest
    {

        protected IntacctClient client;

        [TestInitialize()]
        public void Initialize()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>sessionProvider</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>testuser</userid>
                  <companyid>testcompany</companyid>
                  <sessiontimestamp>2015-12-06T15:57:08-08:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>getSession</controlid>
                  <data>
                        <api>
                              <sessionid>testSeSsionID..</sessionid>
                              <endpoint>https://p1.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
                MockHandler = mockHandler,
            };

            client = new IntacctClient(config);
        }

        [TestMethod()]
        public void ConstructWithSessionIdTest()
        {
            Assert.AreEqual("testSeSsionID..", client.SessionCreds.SessionId);
            StringAssert.Equals("https://p1.intacct.com/ia/xml/xmlgw.phtml", client.SessionCreds.Endpoint);
            Assert.AreEqual("testcompany", client.SessionCreds.CurrentCompanyId);
            Assert.AreEqual("testuser", client.SessionCreds.CurrentUserId);
            Assert.AreEqual(false, client.SessionCreds.CurrentUserIsExternal);
        }

        [TestMethod()]
        public void ConstructWithLoginTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>sessionProvider</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>testuser</userid>
                  <companyid>testcompany</companyid>
                  <sessiontimestamp>2015-12-06T15:57:08-08:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>getSession</controlid>
                  <data>
                        <api>
                              <sessionid>helloworld..</sessionid>
                              <endpoint>https://p1.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "originalSeSsIonID..",
                MockHandler = mockHandler,
            };

            IntacctClient test = new IntacctClient(config);
            Assert.AreEqual("helloworld..", test.SessionCreds.SessionId);
            StringAssert.Equals("https://p1.intacct.com/ia/xml/xmlgw.phtml", test.SessionCreds.Endpoint);
        }

        [TestMethod()]
        public async Task ExecuteSynchronousTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>requestUnitTest</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>testuser</userid>
                  <companyid>testcompany</companyid>
                  <sessiontimestamp>2015-12-06T15:57:08-08:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>func1UnitTest</controlid>
                  <data>
                        <api>
                              <sessionid>unittest..</sessionid>
                              <endpoint>https://unittest.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig
            {
                MockHandler = mockHandler,
            };

            Content content = new Content();
            content.Add(new ApiSessionCreate("func1UnitTest"));

            SynchronousResponse response = await client.Execute(content, false, "requestUnitTest", false, config);

            Assert.AreEqual("requestUnitTest", response.Control.ControlId);
        }

        [TestMethod()]
        public async Task ExecuteAsynchronousTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <acknowledgement>
            <status>success</status>
      </acknowledgement>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>requestUnitTest</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
</response>";

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig
            {
                MockHandler = mockHandler,
            };

            Content content = new Content();
            content.Add(new ApiSessionCreate("func1UnitTest"));

            AsynchronousResponse response = await client.ExecuteAsync(content, "asyncPolicyId", false, "requestUnitTest", false, config);

            Assert.AreEqual("requestUnitTest", response.Control.ControlId);
        }

        [TestMethod()]
        public void RandomControlIdTest()
        {
            string guid = client.GenerateRandomControlId();
            StringAssert.Contains(guid, "-");
        }


        [TestMethod]
        public async Task MockExecuteWithDebugLoggerTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>sessionProvider</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>testuser</userid>
                  <companyid>testcompany</companyid>
                  <sessiontimestamp>2015-12-06T15:57:08-08:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>func1UnitTest</controlid>
                  <data>
                        <api>
                              <sessionid>unittest..</sessionid>
                              <endpoint>https://unittest.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            HttpResponseMessage mockResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            MemoryTarget target = new MemoryTarget();
            target.Layout = "${message}";
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);

            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
                Logger = LogManager.GetCurrentClassLogger(),
            };

            Content content = new Content()
            {
                new ApiSessionCreate(),
            };

            RequestHandler requestHandler = new RequestHandler(config);
            SynchronousResponse response = await requestHandler.ExecuteSynchronous(config, content);

            Assert.IsTrue(target.Logs[0].Contains("intacct-api-net-client/")); // Check for the user agent
        }



        [TestMethod()]
        public async Task LoggerTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>requestUnitTest</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>testuser</userid>
                  <companyid>testcompany</companyid>
                  <sessiontimestamp>2015-12-06T15:57:08-08:00</sessiontimestamp>
            </authentication>
            <result>
                <status>success</status>
                <function>readByQuery</function>
                <controlid>func1UnitTest</controlid>
                <data listtype=""customer"" count=""1"" totalcount=""1"" numremaining=""0"" resultId="""">
                    <customer>
                        <CUSTOMERID>C0001</CUSTOMERID>
                        <NAME>Intacct Corporation</NAME>
                    </customer>
                </data>
            </result>
      </operation>
</response>";

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            MemoryTarget target = new MemoryTarget();
            target.Layout = "${message}";
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);

            SdkConfig config = new SdkConfig
            {
                MockHandler = mockHandler,
                Logger = LogManager.GetCurrentClassLogger(),
            };

            Content content = new Content();
            content.Add(new ApiSessionCreate("func1UnitTest"));

            SynchronousResponse response = await client.Execute(content, false, "requestUnitTest", false, config);

            Assert.IsTrue(target.Logs[0].Contains("<password>REDACTED</password>"));
        }
    }

}