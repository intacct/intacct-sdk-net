/*
 * Copyright 2022 Sage Intacct, Inc.
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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Intacct.SDK.Logging
{
    public sealed class MessageFormatter
    {

        /// <summary>
        /// Apache Common Log Format.
        /// http://httpd.apache.org/docs/2.4/logs.html#common
        /// </summary>
        public const string Clf = "{hostname} {req_header_User-Agent} - [{date_common_log}] "
            + "\"{method} {target} HTTP/{version}\" {code} {res_header_Content-Length}";

        /// <summary>
        /// Debug format
        /// </summary>
        public const string Debug = ">>>>>>>>\n{request}\n<<<<<<<<\n{response}\n--------\n{error}";

        /// <summary>
        /// Short format
        /// </summary>
        public const string Short = "[{ts}] \"{method} {target} HTTP/{version}\" {code}";

        private readonly string _template;

        /// <summary>
        /// MessageFormat constructor
        /// </summary>
        /// <param name="formatTemplate">Log message format template</param>
        public MessageFormatter(string formatTemplate = MessageFormatter.Debug)
        {
            if (string.IsNullOrWhiteSpace(formatTemplate))
            {
                formatTemplate = MessageFormatter.Clf;
            }
            this._template = formatTemplate;
        }

        private static string Headers(HttpHeaders headers)
        {
            string result = "";
            foreach (var header in headers)
            {
                result = result + header.Key + ": " + header.Value + Environment.NewLine;
            }
            return result.Trim();
        }

        public string Format(HttpRequestMessage request, HttpResponseMessage response, Exception error = null)
        {
            string message = Regex.Replace(_template, @"{\s*([A-Za-z_\-\.0-9]+)\s*}", match =>
            {
                string result = "";
                switch (match.Value)
                {
                    case "{request}":
                        result = request.Method.ToString() + " "
                            + request.RequestUri.PathAndQuery
                            + " HTTP/" + request.Version.ToString()
                            + Environment.NewLine + "Host: " + request.Headers.Host;
                        foreach (var header in request.Headers)
                        {
                            result = result + Environment.NewLine + "{" + header.Key + "}: " + string.Join(", ", header.Value);
                        }
                        result = result + Environment.NewLine + Environment.NewLine + request.Content.ReadAsStringAsync().Result;
                        break;
                    case "{response}":
                        result = response != null ? " HTTP/" + response.Version.ToString()
                            + " " + response.StatusCode
                            + " " + response.ReasonPhrase
                            : "";
                        if (response != null)
                        {
                            foreach (var header in response.Headers)
                            {
                                result = result + Environment.NewLine + "{" + header.Key + "}: " + string.Join(", ", header.Value);
                            }
                            result = result + Environment.NewLine + Environment.NewLine + response.Content.ReadAsStringAsync().Result;
                        }
                        break;
                    case "{req_headers}":
                        result = request.Method.ToString()
                            + " " + request.RequestUri.PathAndQuery
                            + " HTTP/" + request.Version.ToString() + Environment.NewLine
                            + Headers(request.Headers);
                        break;
                    case "{res_headers}":
                        result = response != null ?
                            "HTTP:/" + response.Version.ToString()
                                + " " + response.StatusCode.ToString()
                                + " " + response.ReasonPhrase
                                + Environment.NewLine + Headers(response.Headers)
                            : "NULL";
                        break;
                    case "{req_header_User-Agent}":
                        result = request.Headers.UserAgent.ToString();
                        break;
                    case "{res_header_Content-Length}":
                        result = response.Content.Headers.ContentLength.ToString();
                        break;
                    case "{req_body}":
                        result = request.Content.ToString();
                        break;
                    case "{res_body}":
                        result = response != null ? response.Content.ToString() : "NULL";
                        break;
                    case "{ts}":
                    case "{date_iso_8601}":
                        result = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "{date_common_log}":
                        result = DateTime.Now.ToString("dd/MM/yyyy:HH:mm:ss zzz");
                        break;
                    case "{method}":
                        result = request.Method.ToString();
                        break;
                    case "{version}":
                        result = request.Version.ToString();
                        break;
                    case "{uri}":
                        result = request.RequestUri.ToString();
                        break;
                    case "{url}":
                        result = request.RequestUri.ToString();
                        break;
                    case "{target}":
                        result = request.RequestUri.ToString();
                        break;
                    case "{req_version}":
                        result = request.Version.ToString();
                        break;
                    case "{res_version}":
                        result = response.Version.ToString();
                        break;
                    case "{host}":
                        result = request.Headers.Host;
                        break;
                    case "{hostname}":
                        result = Dns.GetHostName();
                        break;
                    case "{code}":
                        result = response != null ? response.StatusCode.ToString() : "NULL";
                        break;
                    case "{phrase}":
                        result = response != null ? response.ReasonPhrase : "NULL";
                        break;
                    case "{error}":
                        result = error != null ? error.Message : "NULL";
                        break;
                    default:
                        // Nothing to see here
                        break;
                }
                return result;
            });

            // Intacct specific redactions of sensitive data
            string[] replacements = {
                "password",
                "accountnumber",
                "cardnum",
                "ssn",
                "achaccountnumber",
                "wireaccountnumber",
                "taxid",
                "sessionid",
            };
            foreach (string replacement in replacements)
            {
                message = Regex.Replace(
                    message,
                    @"(<" + replacement + @"[^>]*>)(.*?)(<\/" + replacement + @">)",
                    "$1REDACTED$3",
                    RegexOptions.IgnoreCase
                );
            }

            return message;
        }
    }
}