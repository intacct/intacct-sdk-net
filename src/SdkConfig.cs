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

namespace Intacct.Sdk
{
    public class SdkConfig
    {

        /// <summary>
        /// Intacct Company ID
        /// </summary>
        public string CompanyId;

        /// <summary>
        /// Control ID of the API request
        /// </summary>
        public string ControlId;

        /// <summary>
        /// Set the request into debug mode, not available to public
        /// </summary>
        public bool Debug;

        /// <summary>
        /// API version to use for the request
        /// </summary>
        public string DtdVersion;

        /// <summary>
        /// The XML encoding to use for the request
        /// </summary>
        public string Encoding;

        /// <summary>
        /// The API endpoint URL to send requests to
        /// </summary>
        public string EndpointUrl;

        /// <summary>
        /// NLog Logger to use
        /// </summary>
        public ILogger Logger;

        /// <summary>
        /// Log message formatter
        /// </summary>
        public MessageFormatter LogFormatter;

        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel LogLevel;

        /// <summary>
        /// Maximum number of retries to allow
        /// </summary>
        public int MaxRetires = 5;

        /// <summary>
        /// MockHandler object used by the unit tests
        /// </summary>
        public MockHandler MockHandler;

        /// <summary>
        /// List of HTTP 5XX server errors codes to not retry
        /// </summary>
        public int[] NoRetryServerErrorCodes;

        /// <summary>
        /// Intacct Policy ID to use for asyncrhonous API requests
        /// </summary>
        public string PolicyId;

        /// <summary>
        /// Profile ini file to use, instead of default, to load credentials
        /// </summary>
        public string ProfileFile;

        /// <summary>
        /// Profile name to use to load credentials
        /// </summary>
        public string ProfileName;

        /// <summary>
        /// Intacct Sender ID
        /// </summary>
        public string SenderId;

        /// <summary>
        /// Intacct Sender Password
        /// </summary>
        public string SenderPassword;

        /// <summary>
        /// Intacct Session ID
        /// </summary>
        public string SessionId;

        /// <summary>
        /// How long to wait until a request times out
        /// </summary>
        public TimeSpan Timeout = TimeSpan.FromSeconds(300);

        /// <summary>
        /// Enable unique control ID's for each function to guarantee idempotence
        /// </summary>
        public bool UniqueId;

        /// <summary>
        /// Intacct User ID
        /// </summary>
        public string UserId;

        /// <summary>
        /// Intacct User Password
        /// </summary>
        public string UserPassword;

        /// <summary>
        /// Enable operation as a transaction instead of each function
        /// </summary>
        public bool Transaction;

        /// <summary>
        /// The current company ID. This is set by the SDK, not the developer.
        /// </summary>
        public string CurrentCompanyId;

        /// <summary>
        /// The current user ID. This is set by the SDK, not the developer.
        /// </summary>
        public string CurrentUserId;

        /// <summary>
        /// The current user is external or not. This is set by the SDK, not the developer.
        /// </summary>
        public bool? CurrentUserIsExternal;

        /// <summary>
        /// Constructs an SdkConfig object
        /// </summary>
        public SdkConfig()
        {
        }

    }
}