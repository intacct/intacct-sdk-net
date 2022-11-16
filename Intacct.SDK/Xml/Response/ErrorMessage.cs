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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Intacct.SDK.Xml.Response
{
    public class ErrorMessage
    {
        public List<string> Errors { get; }

        public ErrorMessage(IEnumerable<XElement> errorMessages)
        {
            Errors = new List<string>();
            foreach (XElement errorMessage in errorMessages)
            {
                List<string> pieces = new List<string>();
                foreach (XElement message in errorMessage.Elements())
                {
                    if (message.Value.Length > 0)
                    {
                        string piece = Cleanse(message.Value);
                        pieces.Add(piece);
                    }
                }
                Errors.Add(string.Join(" ", pieces));
            }
        }

        private static string Cleanse(string value)
        {
            string noHtml = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            string noHtmlNormalised = Regex.Replace(noHtml, @"\s{2,}", " ");

            return noHtmlNormalised;
        }
    }
}