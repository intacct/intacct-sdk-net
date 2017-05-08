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

using Intacct.Sdk.Xml;
using System;

namespace Intacct.Sdk.Functions
{
    abstract public class AbstractFunction : IFunction
    {

        private string _controlId;
        public string ControlId
        {
            get { return _controlId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = Guid.NewGuid().ToString();
                }

                if (value.Length < 1 || value.Length > 256)
                {
                    throw new ArgumentException("ControlId must be between 1 and 256 characters in length");
                }

                _controlId = value;
            }
        }

        public AbstractFunction(string controlId = null)
        {
            ControlId = controlId;
        }

        public abstract void WriteXml(ref IaXmlWriter xml);

    }
}
