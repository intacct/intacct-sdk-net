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

namespace Intacct.Sdk.Tests.Credentials
{

    [TestClass()]
    public class EndpointTest
    {

        [TestMethod()]
        public void DefaultEndpointTest()
        {
            ClientConfig config = new ClientConfig();

            Endpoint endpoint = new Endpoint(config);

            Assert.AreEqual("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.Url);
            Equals("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint);
        }

        [TestMethod()]
        public void EnvVariableEndpointTest()
        {
            string url = "https://envunittest.intacct.com/ia/xml/xmlgw.phtml";

            Environment.SetEnvironmentVariable("INTACCT_ENDPOINT_URL", url);

            ClientConfig config = new ClientConfig();

            Endpoint endpoint = new Endpoint(config);

            Assert.AreEqual(url, endpoint.Url);
            StringAssert.Equals(url, endpoint);

            Environment.SetEnvironmentVariable("INTACCT_ENDPOINT_URL", null);
        }

        [TestMethod()]
        public void ConfigEndpointTest()
        {
            string url = "https://configunittest.intacct.com/ia/xml/xmlgw.phtml";

            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = url
            };

            Endpoint endpoint = new Endpoint(config);

            Assert.AreEqual(url, endpoint.Url);
            StringAssert.Equals(url, endpoint);
        }

        [TestMethod()]
        public void NullEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = ""
            };

            Endpoint endpoint = new Endpoint(config);

            Assert.AreEqual("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.Url);
            StringAssert.Equals("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Endpoint URL is not a valid URL")]
        public void InvalidUrlEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = "invalidurl"
            };

            Endpoint endpoint = new Endpoint(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Endpoint URL is not a valid intacct.com domain name")]
        public void InvalidIntacctEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = "https://api.example.com/xmlgw.phtml"
            };

            Endpoint endpoint = new Endpoint(config);
        }

    }

}