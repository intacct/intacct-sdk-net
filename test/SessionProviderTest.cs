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
using Intacct.Sdk.Xml.Request;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intacct.Sdk.Tests
{

    [TestClass()]
    public class SessionProviderTest
    {
        
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

            ClientConfig config = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
                MockHandler = mockHandler,
            };

            ClientConfig sessionCreds = await SessionProvider.Factory(config);

            Assert.AreEqual("fAkESesSiOnId..", sessionCreds.SessionId);
            StringAssert.Equals("https://unittest.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.EndpointUrl);
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

            ClientConfig config = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "fAkESesSiOnId..",
                EndpointUrl = "https://unittest.intacct.com/ia/xml/xmlgw.phtml",
                MockHandler = mockHandler,
            };

            ClientConfig sessionCreds = await SessionProvider.Factory(config);
            
            Assert.AreEqual("fAkESesSiOnId..", sessionCreds.SessionId);
            StringAssert.Equals("https://unittest.intacct.com/ia/xml/xmlgw.phtml", sessionCreds.EndpointUrl);
        }
    }

}