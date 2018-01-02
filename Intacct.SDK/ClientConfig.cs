﻿/*
 * Copyright 2018 Sage Intacct, Inc.
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

using Intacct.SDK.Credentials;
using Intacct.SDK.Logging;
using Intacct.SDK.Xml.Request;
using Microsoft.Extensions.Logging;

namespace Intacct.SDK
{
    public class ClientConfig
    {

        public string ProfileFile;

        public string ProfileName;

        public string EndpointUrl;

        public string SenderId;

        public string SenderPassword;

        public string SessionId;

        public string CompanyId;

        public string UserId;

        public string UserPassword;

        public ICredentials Credentials;

        public ILoggerAdapter Logger;

        public LogLevel LogLevel;

        public MessageFormatter LogMessageFormatter;

        public MockHandler MockHandler;

        public ClientConfig()
        {
            this.LogLevel = LogLevel.Debug;
            this.LogMessageFormatter = new MessageFormatter();
        }
    }
}