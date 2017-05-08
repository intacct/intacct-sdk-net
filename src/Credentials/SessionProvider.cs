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

using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Xml.Response;
using Intacct.Sdk.Xml.Response.Operation;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Intacct.Sdk.Credentials
{
    public class SessionProvider
    {
        
        public SessionProvider()
        {
            //nothing to see here
        }

        /// <summary>
        /// Constructs an SdkConfig based on the params
        /// </summary>
        /// <param name="senderCreds"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private SdkConfig getConfig(SenderCredentials senderCreds, Endpoint endpoint)
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = senderCreds.SenderId,
                SenderPassword = senderCreds.Password,
                ControlId = "sessionProvider",
                UniqueId = false,
                DtdVersion = "3.0",
                Transaction = false,
                EndpointUrl = endpoint.Url,
                NoRetryServerErrorCodes = null // Retry all 500 level errors
            };
                
            return config;
        }

        /// <summary>
        /// Gets a new Intacct API session ID and endpoint URL
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private async Task<SdkConfig> getAPISession(SdkConfig config)
        {
            Content content = new Content();
            content.Add(new ApiSessionCreate());

            RequestHandler requestHandler = new RequestHandler(config);

            SynchronousResponse response = await requestHandler.ExecuteSynchronous(config, content);

            OperationBlock operation = response.Operation;
            Authentication authentication = operation.Authentication;
            XElement api = operation.Results[0].Data.Element("api");

            SdkConfig session = new SdkConfig()
            {
                SessionId = api.Element("sessionid").Value,
                EndpointUrl = api.Element("endpoint").Value,
                CurrentCompanyId = authentication.CompanyId,
                CurrentUserId = authentication.UserId,
                CurrentUserIsExternal = authentication.SlideInUser,
                Logger = config.Logger,
                LogFormatter = config.LogFormatter,
                LogLevel = config.LogLevel,
            };

            return session;
        }

        /// <summary>
        /// Gets a new SessionCredentials object based on the params
        /// </summary>
        /// <param name="loginCreds"></param>
        /// <returns></returns>
        public async Task<SessionCredentials> FromLoginCredentials(LoginCredentials loginCreds)
        {
            SenderCredentials senderCreds = loginCreds.SenderCreds;
            Endpoint endpoint = loginCreds.Endpoint;

            SdkConfig config = getConfig(senderCreds, endpoint);
            config.CompanyId = loginCreds.CompanyId;
            config.UserId = loginCreds.UserId;
            config.UserPassword = loginCreds.Password;
            config.MockHandler = loginCreds.MockHandler;
            config.Logger = loginCreds.Logger;
            config.LogFormatter = loginCreds.LogMessageFormat;
            config.LogLevel = loginCreds.LogLevel;

            SdkConfig session = await getAPISession(config);

            return new SessionCredentials(session, senderCreds);
        }

        /// <summary>
        /// Gets a new SessionCredentials object based on the params
        /// </summary>
        /// <param name="sessionCreds"></param>
        /// <returns></returns>
        public async Task<SessionCredentials> FromSessionCredentials(SessionCredentials sessionCreds)
        {
            SenderCredentials senderCreds = sessionCreds.SenderCreds;
            Endpoint endpoint = sessionCreds.Endpoint;
            
            SdkConfig config = getConfig(senderCreds, endpoint);
            config.SessionId = sessionCreds.SessionId;
            config.MockHandler = sessionCreds.MockHandler;
            config.Logger = sessionCreds.Logger;
            config.LogFormatter = sessionCreds.LogMessageFormat;
            config.LogLevel = sessionCreds.LogLevel;

            SdkConfig session = await getAPISession(config);

            return new SessionCredentials(session, senderCreds);
        }

    }

}