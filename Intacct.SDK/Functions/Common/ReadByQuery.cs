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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Intacct.SDK.Functions.Common.Query;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common
{
    public class ReadByQuery : AbstractFunction
    {
        private const int MinPageSize = 1;

        private const int MaxPageSize = 1000;

        private const int DefaultPageSize = 1000;

        public string ObjectName { get; set; }

        public List<string> Fields = new List<string>(
            new List<string>
            {
            }
        );

        public IQuery Query;
        
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value < MinPageSize)
                {
                    throw new ArgumentException("Page Size cannot be less than " + MinPageSize.ToString());
                }
                else if (value > MaxPageSize)
                {
                    throw new ArgumentException("Page Size cannot be greater than " + MaxPageSize.ToString());
                }
                _pageSize = value;
            }
        }
        
        private string _docParId;
        public string DocParId
        {
            get { return _docParId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "";
                }
                _docParId = value;
            }
        }

        public ReadByQuery(string controlId = null) : base(controlId)
        {
            PageSize = DefaultPageSize;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);
            
            xml.WriteStartElement("readByQuery");

            xml.WriteElement("object", ObjectName, true);
            xml.WriteElement("query", Query == null ? "" : Query.ToString(), true);
            xml.WriteElement("fields", Fields.Count > 0 ? string.Join(",", Fields) : "*", true);
            xml.WriteElement("pagesize", PageSize);
            xml.WriteElement("returnFormat", "xml");
            xml.WriteElement("docparid", DocParId);
            
            xml.WriteEndElement(); //readByQuery

            xml.WriteEndElement(); //function
        }

    }
}