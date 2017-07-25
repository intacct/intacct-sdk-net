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
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intacct.Sdk.Xml.Request;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml.Response;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Tests.Helpers;
using NLog.Config;
using NLog;
using NLog.Targets;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml
{
    [TestClass]
    public class RequestHandlerTest
    {
        
        [TestMethod]
        public async Task MockExecuteSynchronousTest()
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

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "unittest",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OnlineResponse response = await requestHandler.ExecuteOnline(contentBlock);
            
            Assert.IsInstanceOfType(response, typeof(OnlineResponse));
        }

        [TestMethod]
        public async Task MockExecuteAsynchronousTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
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

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                PolicyId = "policyid321",
                ControlId = "requestUnitTest",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OfflineResponse response = await requestHandler.ExecuteOffline(contentBlock);
            
            Assert.IsInstanceOfType(response, typeof(OfflineResponse));
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required Policy ID not supplied in config for offline request")]
        public async Task ExecuteAsynchronousMissingPolicyIdTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "requestUnitTest",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OfflineResponse response = await requestHandler.ExecuteOffline(contentBlock);
        }

        [TestMethod]
        public async Task MockRetryTest()
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

            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.BadGateway,
            };
            HttpResponseMessage mockResponse2 = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xml)
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
                mockResponse2,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig();

            List<IFunction> contentBlock = new List<IFunction>();

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OnlineResponse response = await requestHandler.ExecuteOnline(contentBlock);
            Assert.AreEqual("testsenderid", response.Control.SenderId);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(HttpRequestException), "Request retry count exceeded max retry count: 5")]
        public async Task MockDefaultRetryFailureTest()
        {
            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };
            HttpResponseMessage mockResponse2 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotImplemented,
            };
            HttpResponseMessage mockResponse3 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadGateway,
            };
            HttpResponseMessage mockResponse4 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.GatewayTimeout,
            };
            HttpResponseMessage mockResponse5 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.HttpVersionNotSupported,
            };
            HttpResponseMessage mockResponse6 = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
                mockResponse2,
                mockResponse3,
                mockResponse4,
                mockResponse5,
                mockResponse6,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig();

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OnlineResponse response = await requestHandler.ExecuteOnline(contentBlock);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(HttpRequestException), "Response status code does not indicate success: 524 ().")]
        public async Task MockDefaultNo524RetryTest()
        {
            HttpResponseMessage mockResponse1 = new HttpResponseMessage()
            {
                StatusCode = (HttpStatusCode) 524,
            };

            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse1,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig();

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OnlineResponse response = await requestHandler.ExecuteOnline(contentBlock);
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



            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
                Logger = LogManager.GetCurrentClassLogger(),
            };

            RequestConfig requestConfig = new RequestConfig();

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OnlineResponse response = await requestHandler.ExecuteOnline(contentBlock);

            Assert.IsTrue(target.Logs[0].Contains("intacct-api-net-client/")); // Check for the user agent
        }

        [TestMethod]
        public async Task MockExecuteOfflineWithSessionCredsTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
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
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Warn);

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                MockHandler = mockHandler,
                Logger = LogManager.GetCurrentClassLogger(),
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "requestUnitTest",
                PolicyId = "policyid123",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(clientConfig, requestConfig);
            OfflineResponse response = await requestHandler.ExecuteOffline(contentBlock);

            Assert.IsTrue(target.Logs[0].Contains("Offline execution sent to Intacct using Session-based credentials."));
        }

    }
}
