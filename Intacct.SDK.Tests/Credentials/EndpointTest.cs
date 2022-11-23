/*
 * Copyright 2022 Sage Intacct, Inc.
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
using Intacct.SDK.Credentials;
using Xunit;

namespace Intacct.SDK.Tests.Credentials
{
    public class EndpointTest
    {
        [Fact]
        public void DefaultEndpointTest()
        {
            ClientConfig config = new ClientConfig();

            Endpoint endpoint = new Endpoint(config);

            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.Url);
            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.ToString());
        }

        [Fact]
        public void EnvVariableEndpointTest()
        {
            string url = "https://envunittest.intacct.com/ia/xml/xmlgw.phtml";

            Environment.SetEnvironmentVariable("INTACCT_ENDPOINT_URL", url);

            ClientConfig config = new ClientConfig();

            Endpoint endpoint = new Endpoint(config);

            Assert.Equal(url, endpoint.Url);
            Assert.Equal(url, endpoint.ToString());

            Environment.SetEnvironmentVariable("INTACCT_ENDPOINT_URL", null);
        }
        
        [Fact]
        public void ConfigEndpointTest()
        {
            string url = "https://configunittest.intacct.com/ia/xml/xmlgw.phtml";

            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = url
            };

            Endpoint endpoint = new Endpoint(config);

            Assert.Equal(url, endpoint.Url);
            Assert.Equal(url, endpoint.ToString());
        }
        
        [Fact]
        public void NullEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = ""
            };

            Endpoint endpoint = new Endpoint(config);

            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.Url);
            Assert.Equal("https://api.intacct.com/ia/xml/xmlgw.phtml", endpoint.ToString());
        }
        
        [Fact]
        public void InvalidUrlEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = "invalidurl"
            };

            var ex = Record.Exception(() => new Endpoint(config));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Endpoint URL is not a valid URL", ex.Message);
        }
        
        [Fact]
        public void InvalidIntacctEndpointTest()
        {
            ClientConfig config = new ClientConfig()
            {
                EndpointUrl = "https://api.example.com/xmlgw.phtml"
            };

            var ex = Record.Exception(() => new Endpoint(config));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Endpoint URL is not a valid intacct.com domain name", ex.Message);
        }
    }
}