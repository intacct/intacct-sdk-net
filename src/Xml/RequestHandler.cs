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
using Intacct.Sdk.Xml.Response;
using NLog;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Intacct.Sdk.Xml
{

    public class RequestHandler
    {

        /// <summary>
        /// Current version of the SDK
        /// </summary>
        public const string Version = "1.0";

        /// <summary>
        /// The default HTTP content type header for Intacct XML requests
        /// </summary>
        public const string RequestContentType = "application/xml";

        /// <summary>
        /// The Endpoint URL to post the request to
        /// </summary>
        private string endpointUrl;

        /// <summary>
        /// MockHandler object used by the unit tests
        /// <seealso cref="MockHandler"/>
        /// </summary>
        private MockHandler mockHandler;

        private int maxRetries;
        /// <summary>
        /// Maximum number of retries to allow
        /// </summary>
        public int MaxRetries
        {
            get { return maxRetries; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("MaxRetries must be zero or greater");
                }
                maxRetries = value;
            }
        }

        /// <summary>
        /// How long to wait until a request times out
        /// </summary>
        private TimeSpan timeout;

        private int[] noRetryServerErrorCodes;
        /// <summary>
        /// List of HTTP 5XX server errors codes to not retry
        /// </summary>
        public int[] NoRetryServerErrorCodes
        {
            get { return noRetryServerErrorCodes; }
            set
            {
                foreach (int errorCode in value)
                {
                    if (errorCode < 500 || errorCode > 599)
                    {
                        throw new ArgumentException("NoRetryServerErrorCodes must be between 500-599");
                    }
                }
                noRetryServerErrorCodes = value;
            }
        }
        
        public ILogger Logger;

        public MessageFormatter LogMessageFormatter;

        public LogLevel LogLevel;

        private string userAgent
        {
            get
            {
                return "intacct-api-net-client/" + Version;
            }
        }

        /// <summary>
        /// Construct the RequestHandler object based on SdkConfig
        /// <seealso cref="SdkConfig"/>
        /// </summary>
        /// <param name="config"></param>
        public RequestHandler(SdkConfig config)
        {
            endpointUrl = config.EndpointUrl != null ? config.EndpointUrl : new Endpoint(config).ToString();
            mockHandler = config.MockHandler;
            MaxRetries = config.MaxRetires;
            timeout = config.Timeout;
            Logger = config.Logger;
            LogMessageFormatter = config.LogFormatter != null ? config.LogFormatter : new MessageFormatter();
            LogLevel = config.LogLevel != null ? config.LogLevel : LogLevel.Debug;
            NoRetryServerErrorCodes = config.NoRetryServerErrorCodes != null
                ? NoRetryServerErrorCodes = config.NoRetryServerErrorCodes
                // 524 = CDN cut connection, Intacct is still processing the request
                : NoRetryServerErrorCodes = new int[] { 524 };
        }

        /// <summary>
        /// Gets the retry delay
        /// </summary>
        /// <param name="retries"></param>
        /// <returns></returns>
        private int ExponentialDelay(int retries)
        {
            return (int)Math.Pow(2, retries - 1);
        }

        /// <summary>
        /// Execute a request synchronously
        /// </summary>
        /// <param name="config"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<SynchronousResponse> ExecuteSynchronous(SdkConfig config, Content content)
        {
            if (!String.IsNullOrWhiteSpace(config.PolicyId))
            {
                config.PolicyId = null;
            }
            RequestBlock request = new RequestBlock(config, content);

            SynchronousResponse response = new SynchronousResponse(await Execute(request.WriteXml()));

            return response;
        }

        /// <summary>
        /// Execute a request asynchronously
        /// </summary>
        /// <param name="config"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<AsynchronousResponse> ExecuteAsynchronous(SdkConfig config, Content content)
        {
            if (String.IsNullOrWhiteSpace(config.PolicyId))
            {
                throw new ArgumentException("Required PolicyId not supplied in params for asynchronous request");
            }

            RequestBlock request = new RequestBlock(config, content);

            AsynchronousResponse response = new AsynchronousResponse(await Execute(request.WriteXml()));

            return response;
        }

        /// <summary>
        /// Gets the appropriate HttpMessageHandler
        /// </summary>
        /// <returns></returns>
        private HttpMessageHandler GetHttpMessageHandler()
        {
            if (mockHandler != null)
            {
                if (Logger != null)
                {
                    return new LoggingHandler(mockHandler, Logger, LogMessageFormatter, LogLevel);
                }
                else
                {
                    return mockHandler;
                }
            }
            else
            {
                if (Logger != null)
                {
                    return new LoggingHandler(new HttpClientHandler(), Logger, LogMessageFormatter, LogLevel);
                }
                else
                {
                    return new HttpClientHandler();
                }
            }
        }

        /// <summary>
        /// Execute the XML request to the API endpoint
        /// </summary>
        /// <param name="RequestXml"></param>
        /// <returns></returns>
        private async Task<Stream> Execute(Stream RequestXml)
        {
            HttpClient client = new HttpClient(GetHttpMessageHandler());
            client.Timeout = timeout;
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

            RequestXml.Position = 0;
            StreamContent content = new StreamContent(RequestXml);
            content.Headers.ContentType = new MediaTypeHeaderValue(RequestContentType);
            
            for (int attempt = 0; attempt <= MaxRetries; attempt++)
            {
                if (attempt > 0)
                {
                    // Delay this retry based on exponential delay
                    await Task.Delay(ExponentialDelay(attempt));
                }
                HttpResponseMessage response = client.PostAsync(endpointUrl, content).Result;

                int httpCode = (int)response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    return stream;
                }
                else if (Array.Exists(noRetryServerErrorCodes, element => element == httpCode))
                {
                    // Do not retry this explicitly set 500 level server error
                    response.EnsureSuccessStatusCode();
                }
                else if (httpCode >= 500 && httpCode <= 599)
                {
                    // Retry 500 level server errors
                    continue;
                }
                else
                {
                    // Throw exception for non-500 level errors
                    response.EnsureSuccessStatusCode();

                    // Will this also throw for timeouts?  Let's find out!
                }
            }

            throw new HttpRequestException("Request retry count exceeded max retry count: " + MaxRetries.ToString());
        }

    }
}
