/*
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

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Intacct.SDK.Logging;
using Microsoft.Extensions.Logging;

namespace Intacct.SDK.Xml
{
    public class LoggingHandler : DelegatingHandler
    {
        private ILoggerAdapter _logger;
        
        private MessageFormatter _logMessageFormatter;
        
        private LogLevel _logLevel;

        public LoggingHandler(HttpMessageHandler innerHandler, ILoggerAdapter logger, MessageFormatter logMessageFormat, LogLevel logLevel) : base(innerHandler)
        {
            this._logger = logger;
            this._logMessageFormatter = logMessageFormat;
            this._logLevel = logLevel;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await base.SendAsync(request, cancellationToken);

                switch (_logLevel)
                {
                    case LogLevel.Trace:
                        _logger.LogTrace(_logMessageFormatter.Format(request, response));
                        break;
                    case LogLevel.Debug:
                        _logger.LogDebug( _logMessageFormatter.Format(request, response));
                        break;
                    case LogLevel.Information:
                        _logger.LogInformation(_logMessageFormatter.Format(request, response));
                        break;
                    case LogLevel.Warning:
                        _logger.LogWarning(_logMessageFormatter.Format(request, response));
                        break;
                    case LogLevel.Error:
                        _logger.LogError(_logMessageFormatter.Format(request, response));
                        break;
                    case LogLevel.Critical:
                        _logger.LogCritical(_logMessageFormatter.Format(request, response));
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logMessageFormatter.Format(request, response), ex);
                throw ex;
            }

            return response;
        }

    }
}