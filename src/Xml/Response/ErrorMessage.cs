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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Intacct.Sdk.Xml.Response
{
    public class ErrorMessage
    {

        private List<string> errors;
        public List<string> Errors
        {
            get { return errors; }
            set
            {
                errors = value;
            }
        }

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
                        string piece = cleanse(message.Value);
                        pieces.Add(piece);
                    }
                }
                Errors.Add(String.Join(" ", pieces));
            }
        }

        private string cleanse(string value)
        {
            string noHTML = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            string noHTMLNormalised = Regex.Replace(noHTML, @"\s{2,}", " ");

            return noHTMLNormalised;
        }

    }
}
