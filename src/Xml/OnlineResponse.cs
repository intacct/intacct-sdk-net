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

using Intacct.Sdk.Exceptions;
using Intacct.Sdk.Xml.Response;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Intacct.Sdk.Xml
{

    public class OnlineResponse : AbstractResponse
    {

        private Authentication authentication;

        public Authentication Authentication
        {
            get { return authentication; }
            private set
            {
                authentication = value;
            }
        }

        public List<Result> Results;

        public OnlineResponse(Stream body) : base(body)
        {
            if (Xml.Element("response").Element("operation") == null)
            {
                throw new IntacctException("Response is missing operation block");
            }
            
            if (Xml.Element("response").Element("operation").Element("authentication") == null)
            {
                throw new IntacctException("Authentication block is missing from operation element");
            }
            Authentication = new Authentication(Xml.Element("response").Element("operation").Element("authentication"));

            if (Authentication.Status != "success")
            {
                ErrorMessage errorMessage = new ErrorMessage(Xml.Element("response").Element("operation").Element("errormessage").Elements("error"));

                throw new ResponseException("Response authentication status failure", errorMessage.Errors);
            }

            if (Xml.Element("response").Element("operation").Element("result") == null)
            {
                throw new IntacctException("Result block is missing from operation element");
            }

            IEnumerable<XElement> results = Xml.Element("response").Element("operation").Elements("result");

            Results = new List<Result>();

            foreach (XElement res in results)
            {
                Results.Add(new Result(res));
            }
        }

    }

}
