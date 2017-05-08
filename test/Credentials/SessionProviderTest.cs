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

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class SessionProviderTest
    {

        protected SessionProvider provider;

        protected SenderCredentials senderCreds;

        [TestInitialize()]
        public void Initialize()
        {
            provider = new SessionProvider();

            SdkConfig config = new SdkConfig
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!"
            };
            senderCreds = new SenderCredentials(config);
        }

        [TestMethod()]
        public async Task FromLoginCredentialsTest()
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
                  <function>getSession</function>
                  <controlid>testControlId</controlid>
                  <data>
                        <api>
                              <sessionid>fAkESesSiOnId..</sessionid>
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

            SdkConfig config = new SdkConfig()
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
                MockHandler = mockHandler,
            };

            LoginCredentials loginCreds = new LoginCredentials(config, senderCreds);

            SessionCredentials sessionCreds = await provider.FromLoginCredentials(loginCreds);

            Assert.AreEqual("fAkESesSiOnId..", sessionCreds.SessionId);
            StringAssert.Equals("https://unittest.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.Endpoint);
            Assert.IsInstanceOfType(sessionCreds.SenderCreds, typeof(SenderCredentials));
        }

        [TestMethod()]
        public async Task FromSessionCredentialsTest()
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
                  <function>getSession</function>
                  <controlid>testControlId</controlid>
                  <data>
                        <api>
                              <sessionid>fAkESesSiOnId..</sessionid>
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

            SdkConfig config = new SdkConfig()
            {
                SessionId = "fAkESesSiOnId..",
                EndpointUrl = "https://unittest.intacct.com/ia/xml/xmlgw.phtml",
                MockHandler = mockHandler,
            };

            SessionCredentials sessionCreds = new SessionCredentials(config, senderCreds);

            SessionCredentials newSessionCreds = await provider.FromSessionCredentials(sessionCreds);

            Assert.AreEqual("fAkESesSiOnId..", newSessionCreds.SessionId);
            StringAssert.Equals("https://unittest.intacct.com/ia/xml/xmlgw.phtml", newSessionCreds.Endpoint);
            Assert.IsInstanceOfType(newSessionCreds.SenderCreds, typeof(SenderCredentials));
        }
    }

}