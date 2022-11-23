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

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml.Response;

namespace Intacct.SDK.Xml
{
    public class OnlineResponse: AbstractResponse
    {
        public Authentication Authentication { get; }

        public List<Result> Results;

        public OnlineResponse(Stream body) : base(body)
        {
            var operation = Response.Element("operation");
            if (operation == null)
            {
                throw new IntacctException("Response is missing operation block");
            }

            var authentication = operation.Element("authentication");
            if (authentication == null)
            {
                throw new IntacctException("Authentication block is missing from operation element");
            }
            Authentication = new Authentication(authentication);

            if (Authentication.Status != "success")
            {
                var errorElement = operation.Element("errormessage");
                if (errorElement == null)
                {
                    throw new ResponseException("Response authentication status failure");
                }
                ErrorMessage errorMessage = new ErrorMessage(errorElement.Elements("error"));
                throw new ResponseException("Response authentication status failure", errorMessage.Errors);
            }

            if (operation.Element("result") == null)
            {
                throw new IntacctException("Result block is missing from operation element");
            }

            IEnumerable<XElement> results = operation.Elements("result");

            Results = new List<Result>();

            foreach (XElement res in results)
            {
                Results.Add(new Result(res));
            }
        }
    }
}