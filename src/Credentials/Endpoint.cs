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

namespace Intacct.Sdk.Credentials
{
    public class Endpoint
    {
        
        const string DefaultEndpoint = "https://api.intacct.com/ia/xml/xmlgw.phtml";
        
        const string EndpointUrlEnvName = "INTACCT_ENDPOINT_URL";
        
        const string DomainName = "intacct.com";
        
        private string url;
        
        public string Url
        {
            get { return url; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = Endpoint.DefaultEndpoint;
                }
                Uri uriResult;
                if (!Uri.TryCreate(value, UriKind.Absolute, out uriResult))
                {
                    throw new ArgumentException("Endpoint URL is not a valid URL");
                }
                if (!uriResult.Host.EndsWith("." + DomainName))
                {
                    throw new ArgumentException("Endpoint URL is not a valid " + DomainName + " domain name");
                }

                url = value;
            }
        }
        
        public Endpoint(ClientConfig config)
        {
            if (String.IsNullOrEmpty(config.EndpointUrl))
            {
                Url = Environment.GetEnvironmentVariable(EndpointUrlEnvName);
            }
            else
            {
                Url = config.EndpointUrl;
            }
        }
        
        public override string ToString()
        {
            return Url;
        }
    }
}