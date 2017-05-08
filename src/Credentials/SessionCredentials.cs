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

using Intacct.Sdk.Logging;
using Intacct.Sdk.Xml.Request;
using NLog;
using System;

namespace Intacct.Sdk.Credentials
{
    public class SessionCredentials
    {
        public string SessionId;

        public SenderCredentials SenderCreds;

        public Endpoint Endpoint;

        public string CurrentCompanyId;

        public string CurrentUserId;

        public bool? CurrentUserIsExternal;

        public MockHandler MockHandler;

        public ILogger Logger;

        public MessageFormatter LogMessageFormat;

        public LogLevel LogLevel;

        public SessionCredentials(SdkConfig config, SenderCredentials senderCreds)
        {
            if (String.IsNullOrWhiteSpace(config.SessionId))
            {
                throw new ArgumentException("Required SessionId not supplied in params");
            }

            SessionId = config.SessionId;
            if (!String.IsNullOrWhiteSpace(config.EndpointUrl))
            {
                Endpoint = new Endpoint(config);
            }
            else
            {
                Endpoint = senderCreds.Endpoint;
            }

            SenderCreds = senderCreds;
            CurrentCompanyId = config.CurrentCompanyId;
            CurrentUserId = config.CurrentUserId;
            CurrentUserIsExternal = config.CurrentUserIsExternal;
            MockHandler = config.MockHandler;

            Logger = config.Logger;
            LogMessageFormat = config.LogFormatter;
            LogLevel = config.LogLevel;
        }
    }
}