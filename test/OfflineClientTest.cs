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
    public class OfflineClientTest
    {

        [TestMethod()]
        public async Task ExecuteTest()
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

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsender",
                SenderPassword = "testsendpass",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                PolicyId = "asyncPolicyId",
            };

            OfflineClient client = new OfflineClient(clientConfig);

            OfflineResponse response = await client.execute(new ApiSessionCreate("func1UnitTest"), requestConfig);

            Assert.AreEqual("requestUnitTest", response.Control.ControlId);
        }

        [TestMethod()]
        public async Task ExecuteBatchTest()
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

            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsender",
                SenderPassword = "testsendpass",
                SessionId = "testsession..",
                MockHandler = mockHandler,
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                PolicyId = "asyncPolicyId",
            };

            OfflineClient client = new OfflineClient(clientConfig);

            List<IFunction> contentBlock = new List<IFunction>();
            contentBlock.Add(new ApiSessionCreate("func1UnitTest"));

            OfflineResponse response = await client.executeBatch(contentBlock, requestConfig);

            Assert.AreEqual("requestUnitTest", response.Control.ControlId);
        }
    }

}