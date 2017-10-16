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

using Intacct.Sdk.Credentials;
using Intacct.Sdk.Xml;
using System;
using System.Threading.Tasks;

namespace Intacct.Sdk
{

    abstract public class AbstractClient
    {

        /// <summary>
        /// Profile environment variable
        /// </summary>
        const string ProfileEnvName = "INTACCT_PROFILE";

        /// <summary>
        /// Default Timeout for all SDK Calls
        /// </summary>
        protected readonly TimeSpan DefaultTimeout;

        /// <summary>
        /// <seealso cref="Intacct.Sdk.Credentials.SessionCredentials"/>
        /// </summary>
        protected SessionCredentials sessionCreds;

        public AbstractClient(SdkConfig config)
        {
            this.DefaultTimeout = config.Timeout;
            InitializeAsync(config).Wait();
        }

        private async Task InitializeAsync(SdkConfig config)
        {
            if (String.IsNullOrWhiteSpace(config.ProfileName))
            {
                config.ProfileName = Environment.GetEnvironmentVariable(ProfileEnvName);
            }

            SessionProvider provider = new SessionProvider();

            SenderCredentials senderCreds = new SenderCredentials(config);

            if (!String.IsNullOrWhiteSpace(config.SessionId))
            {
                SessionCredentials session = new SessionCredentials(config, senderCreds);

                sessionCreds = await provider.FromSessionCredentials(session);
            }
            else
            {
                LoginCredentials login = new LoginCredentials(config, senderCreds);

                sessionCreds = await provider.FromLoginCredentials(login);
            }
        }

        /// <summary>
        /// Constructs an SdkConfig object based on the environment/config/runtime params
        /// </summary>
        /// <returns></returns>
        private SdkConfig SessionConfig()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = sessionCreds.SenderCreds.SenderId,
                SenderPassword = sessionCreds.SenderCreds.Password,
                EndpointUrl = sessionCreds.Endpoint.Url,
                SessionId = sessionCreds.SessionId,
                Logger = sessionCreds.Logger,
                LogFormatter = sessionCreds.LogMessageFormat,
                LogLevel = sessionCreds.LogLevel,
                Timeout = DefaultTimeout
            };

            return config;
        }

        public string GenerateRandomControlId()
        {
            return Guid.NewGuid().ToString();
        }

        protected async Task<SynchronousResponse> Execute(
            Content contentBlock,
            bool transaction,
            string requestControlId,
            bool uniqueFunctionControlIds,
            SdkConfig config
        )
        {
            SdkConfig sessionConfig = SessionConfig();

            if (!String.IsNullOrWhiteSpace(sessionConfig.SenderId))
            {
                config.SenderId = sessionConfig.SenderId;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.SenderPassword))
            {
                config.SenderPassword = sessionConfig.SenderPassword;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.EndpointUrl))
            {
                config.EndpointUrl = sessionConfig.EndpointUrl;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.SessionId))
            {
                config.SessionId = sessionConfig.SessionId;
            }

            config.Logger = config.Logger != null ? config.Logger : sessionConfig.Logger;
            config.LogFormatter = config.LogFormatter != null ? config.LogFormatter : sessionConfig.LogFormatter;
            config.LogLevel = config.LogLevel != null ? config.LogLevel : sessionConfig.LogLevel;

            config.Transaction = transaction;
            config.ControlId = requestControlId;
            config.UniqueId = uniqueFunctionControlIds;

            RequestHandler requestHandler = new RequestHandler(config);

            SynchronousResponse response = await requestHandler.ExecuteSynchronous(config, contentBlock);

            return response;
        }

        protected async Task<AsynchronousResponse> ExecuteAsync(
            Content contentBlock,
            string asyncPolicyId,
            bool transaction,
            string requestControlId,
            bool uniqueFunctionControlIds,
            SdkConfig config
        )
        {
            SdkConfig sessionConfig = SessionConfig();

            if (String.IsNullOrWhiteSpace(config.PolicyId))
            {
                config.PolicyId = asyncPolicyId;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.SenderId))
            {
                config.SenderId = sessionConfig.SenderId;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.SenderPassword))
            {
                config.SenderPassword = sessionConfig.SenderPassword;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.EndpointUrl))
            {
                config.EndpointUrl = sessionConfig.EndpointUrl;
            }

            if (!String.IsNullOrWhiteSpace(sessionConfig.SessionId))
            {
                config.SessionId = sessionConfig.SessionId;
            }

            config.Logger = config.Logger != null ? config.Logger : sessionConfig.Logger;
            config.LogFormatter = config.LogFormatter != null ? config.LogFormatter : sessionConfig.LogFormatter;
            config.LogLevel = config.LogLevel != null ? config.LogLevel : sessionConfig.LogLevel;

            config.Transaction = transaction;
            config.ControlId = requestControlId;
            config.UniqueId = uniqueFunctionControlIds;

            RequestHandler requestHandler = new RequestHandler(config);

            AsynchronousResponse response = await requestHandler.ExecuteAsynchronous(config, contentBlock);

            return response;
        }
    }
}
