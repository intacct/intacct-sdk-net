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
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Intacct.Sdk.Xml
{
    abstract public class AbstractResponse
    {

        protected XDocument xml;

        public Control Control;

        public AbstractResponse(Stream body)
        {
            xml = XDocument.Load(body);

            if (xml.Element("response").Element("control") == null)
            {
                throw new IntacctException("Response is missing control block");
            }
            Control = new Control(xml.Element("response").Element("control"));

            if (Control.Status != "success")
            {
                ErrorMessage errorMessage = new ErrorMessage(xml.Element("response").Element("errormessage").Elements("error"));

                throw new ResponseException("Response control status failure", errorMessage.Errors);
            }
        }

    }
}