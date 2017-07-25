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
using Intacct.Sdk.Logging;
using Intacct.Sdk.Xml.Request;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intacct.Sdk
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

        public ILogger Logger;

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
