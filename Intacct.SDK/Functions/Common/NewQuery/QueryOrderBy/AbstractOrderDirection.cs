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

namespace Intacct.SDK.Functions.Common.NewQuery.QueryOrderBy
{
    public abstract class AbstractOrderDirection : IOrder
    {
        private readonly string _fieldName;

        protected AbstractOrderDirection(string fieldName)
        {
            this._fieldName = fieldName;
        }
        
        protected abstract string GetDirection();

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("order");
            xml.WriteElement("field", _fieldName);
            xml.WriteElement(GetDirection(), "", true);
            xml.WriteEndElement(); // order
        }
    }
}