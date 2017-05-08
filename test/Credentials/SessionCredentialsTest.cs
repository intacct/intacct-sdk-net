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

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class SessionCredentialsTest
    {

        protected SenderCredentials senderCreds;

        [TestInitialize()]
        public void Initialize()
        {
            SdkConfig config = new SdkConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!"
            };
            senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        public void CredsFromArrayTest()
        {
            SdkConfig config = new SdkConfig
            {
                SessionId = "faKEsesSiOnId..",
                EndpointUrl = "https://p1.intacct.com/ia/xml/xmlgw.phtml"
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, senderCreds);

            Assert.AreEqual("faKEsesSiOnId..", sessionCreds.SessionId);
            StringAssert.Equals("https://p1.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.Endpoint);
            Assert.IsInstanceOfType(sessionCreds.SenderCreds, typeof(SenderCredentials));
        }

        [TestMethod()]
        public void CredsFromArrayNoEndpointTest()
        {
            SdkConfig config = new SdkConfig
            {
                SessionId = "faKEsesSiOnId..",
                EndpointUrl = null
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, senderCreds);

            Assert.AreEqual("faKEsesSiOnId..", sessionCreds.SessionId);
            StringAssert.Equals("https://api.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.Endpoint);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required SessionId not supplied in params")]
        public void CredsFromArrayNoSessionTest()
        {
            SdkConfig config = new SdkConfig
            {
                SessionId = null
            };
            SessionCredentials sessionCreds = new SessionCredentials(config, senderCreds);
        }

        [TestMethod()]
        public void GetMockHandlerTest()
        {
            HttpResponseMessage mockResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
            List<HttpResponseMessage> mockResponses = new List<HttpResponseMessage>
            {
                mockResponse,
            };

            MockHandler mockHandler = new MockHandler(mockResponses);

            SdkConfig config = new SdkConfig()
            {
                SessionId = "faKEsesSiOnId..",
                EndpointUrl = "https://p1.intacct.com/ia/xml/xmlgw.phtml",
                MockHandler = mockHandler,
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, senderCreds);

            Assert.IsInstanceOfType(sessionCreds.MockHandler, typeof(MockHandler));
        }
    }

}