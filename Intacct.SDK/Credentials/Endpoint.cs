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

namespace Intacct.SDK.Credentials
{
    public class Endpoint
    {
        public const string DefaultEndpoint = "https://api.intacct.com/ia/xml/xmlgw.phtml";

        public const string EndpointUrlEnvName = "INTACCT_ENDPOINT_URL";

        public const string DomainName = "intacct.com";

        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = Endpoint.DefaultEndpoint;
                }

                if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult))
                {
                    throw new ArgumentException("Endpoint URL is not a valid URL");
                }
                if (!uriResult.Host.EndsWith("." + Endpoint.DomainName))
                {
                    throw new ArgumentException("Endpoint URL is not a valid " + Endpoint.DomainName + " domain name");
                }

                _url = value;
            }
        }

        public Endpoint(ClientConfig config)
        {
            Url = string.IsNullOrEmpty(config.EndpointUrl)
                ? Environment.GetEnvironmentVariable(Endpoint.EndpointUrlEnvName)
                : config.EndpointUrl;
        }

        public override string ToString()
        {
            return Url;
        }
    }
}