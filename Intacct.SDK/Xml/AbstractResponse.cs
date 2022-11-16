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

using System.IO;
using System.Xml.Linq;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml.Response;

namespace Intacct.SDK.Xml
{
    public abstract class AbstractResponse
    {
        protected XElement Response { get; set; }

        public Control Control { get; }

        protected AbstractResponse(Stream body)
        {
            XDocument xml = XDocument.Load(body);

            Response = xml.Element("response");

            if (Response == null)
            {
                throw new IntacctException("XML body is missing response block");
            }
            
            if (Response.Element("control") == null)
            {
                throw new IntacctException("Response is missing control block");
            }
            Control = new Control(Response.Element("control"));

            if (Control.Status != "success")
            {
                var errorElement = Response.Element("errormessage");
                if (errorElement == null)
                {
                    throw new ResponseException("Response control status failure");
                }
                ErrorMessage errorMessage = new ErrorMessage(errorElement.Elements("error"));
                throw new ResponseException("Response control status failure", errorMessage.Errors);
            }
        }
    }
}