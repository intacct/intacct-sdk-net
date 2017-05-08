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
using NLog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Intacct.Sdk.Xml
{

    public class LoggingHandler : DelegatingHandler
    {

        private ILogger logger;
        private MessageFormatter logMessageFormatter;
        private LogLevel logLevel;

        public LoggingHandler(HttpMessageHandler innerHandler, ILogger Logger, MessageFormatter LogMessageFormat, LogLevel LogLevel) : base(innerHandler)
        {
            logger = Logger;
            logMessageFormatter = LogMessageFormat;
            logLevel = LogLevel;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await base.SendAsync(request, cancellationToken);
                logger.Log(logLevel, logMessageFormatter.Format(request, response));
            }
            catch (Exception ex)
            {
                logger.Log(logLevel, logMessageFormatter.Format(request, response, ex));
                throw ex;
            }

            return response;
        }

    }
}
