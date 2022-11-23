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
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery.QueryFilter
{
    public abstract class AbstractOperator : IFilter
    {
        private readonly List<IFilter> _filters;

        protected AbstractOperator(List<IFilter> filters)
        {
            if (filters != null)
            {
                this._filters = filters;
            }
            else
            {
                this._filters = new List<IFilter>();
            }
        }

        public IFilter AddFilter(IFilter filter)
        {
            this._filters.Add(filter);
            return this;
        }

        protected abstract string GetOperator();
        
        public void WriteXml(ref IaXmlWriter xml)
        {
            if ((this._filters != null) && (this._filters.Count >= 2))
            {
                xml.WriteStartElement(GetOperator());
                foreach (var filter in this._filters)
                {
                    filter.WriteXml(ref xml);
                }
                xml.WriteEndElement();
            }
            else
            {
                throw new IntacctException("Two or more IFilter objects required for " + GetOperator());
            }
        }
    }
}