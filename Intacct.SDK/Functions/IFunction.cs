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

using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions
{
    public interface IFunction : IXmlObject
    {

        string ControlId { get; set; }
        
        /// <summary>
        /// This method should use an IaXmlWriter reference to write the `function` block element with its `controlid`
        /// attribute. This and everything inside the function block must follow the Web Services schema.
        /// </summary>
        /// <param name="xml"></param>
        new void WriteXml(ref IaXmlWriter xml);
    }
}