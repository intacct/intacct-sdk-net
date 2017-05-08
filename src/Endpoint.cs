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

namespace Intacct.Sdk
{
    public class Endpoint
    {

        /// <summary>
        /// Default endpoint URL
        /// </summary>
        const string DefaultEndpoint = "https://api.intacct.com/ia/xml/xmlgw.phtml";

        /// <summary>
        /// Default environment variable name
        /// </summary>
        const string EndpointUrlEnvName = "INTACCT_ENDPOINT_URL";

        /// <summary>
        /// Domain name to validate
        /// </summary>
        const string DomainName = "intacct.com";

        /// <summary>
        /// Object property containing the endpoint URL
        /// </summary>
        private string url = DefaultEndpoint;

        /// <summary>
        /// Endpoint URL to be used
        /// </summary>
        public string Url
        {
            get { return url; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = DefaultEndpoint;
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

        /// <summary>
        /// Constructs the Endpoint object based on the SdkConfig params
        /// <seealso cref="SdkConfig"/>
        /// </summary>
        /// <param name="config"></param>
        public Endpoint(SdkConfig config)
        {
            if (String.IsNullOrWhiteSpace(config.EndpointUrl))
            {
                Url = Environment.GetEnvironmentVariable(EndpointUrlEnvName);
            }
            else
            {
                Url = config.EndpointUrl;
            }
        }

        /// <summary>
        /// Output the Endpoint to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Url;
        }
    }
}