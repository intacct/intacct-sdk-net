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
using Intacct.SDK.Exceptions;
using Intacct.SDK.Functions.Common.NewQuery.QueryFilter;
using Intacct.SDK.Functions.Common.NewQuery.QueryOrderBy;
using Intacct.SDK.Functions.Common.NewQuery.QuerySelect;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery
{
    public class QueryFunction : AbstractFunction, IQueryFunction
    {
        public ISelect[] SelectFields { get; set; }
        
        public string FromObject { get; set; }
        
        private string _docParId;
        
        public string DocParId
        {
            get => _docParId; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "";
                }
                _docParId = value;
            }
        }
        
        public IFilter Filter { get; set; } 
        
        public IOrder[] OrderBy { get; set; }

        public bool? CaseInsensitive { get; set; }
        
        public bool? ShowPrivate { get; set; }
        
        private int? _pageSize;

        public int? PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1)
                {
                    throw new IntacctException("PageSize cannot be negative. Set PageSize greater than zero.");
                }

                _pageSize = value;
            }
        }

        private int? _offset;

        public int? Offset
        {
            get => _offset;
            set
            {
                if (value < 0)
                {
                    throw new IntacctException("Offset cannot be negative. Set Offset to zero or greater than zero.");
                }

                _offset = value;
            }
        }

        public QueryFunction(string controlId = null) : base(controlId)
        {
        }
        
        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("query");

            if (SelectFields == null || SelectFields.Length == 0)
            {
                throw new ArgumentException("Select fields are required for query; set through method SelectFields setter.");
            }
            
            xml.WriteStartElement("select");
            foreach (var field in SelectFields)
            {
                field.WriteXml(ref xml);
            }
            xml.WriteEndElement(); //select

            if (string.IsNullOrEmpty(FromObject))
            {
                throw new ArgumentException("From Object is required for query; set through method from setter.");
            }
            
            xml.WriteElement("object", FromObject, true);
            
            if (string.IsNullOrEmpty(DocParId) == false)
            {
                xml.WriteElement("docparid", DocParId);
            }

            if (Filter != null)
            {
                xml.WriteStartElement("filter");
                Filter.WriteXml(ref xml);
                xml.WriteEndElement(); //filter
            }
            
            if (OrderBy != null && OrderBy.Length > 0)
            {
                xml.WriteStartElement("orderby");
                foreach (var order in OrderBy)
                {
                    order.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //orderby
            }

            xml.WriteStartElement("options");
            if (CaseInsensitive != null)
            {
                xml.WriteElement("caseinsensitive", CaseInsensitive);
            }
            
            if (ShowPrivate != null)
            {
                xml.WriteElement("showprivate", ShowPrivate);
            }

            xml.WriteEndElement(); //options

            if (_pageSize != null)
            {
                xml.WriteElement("pagesize", _pageSize);
            }

            if (_offset != null)
            {
                xml.WriteElement("offset", _offset);
            }
            
            xml.WriteEndElement(); //query

            xml.WriteEndElement(); //function
        }
    }
}