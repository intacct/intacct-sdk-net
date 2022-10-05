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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common
{
    public class Read : AbstractFunction
    {
        private const int MaxKeyCount = 100;

        public string ObjectName { get; set; }

        public List<string> Fields = new List<string>(
            new List<string>
            {
            }
        );
        
        private List<int> _keys;
        public List<int> Keys
        {
            get { return _keys; }
            set
            {
                if (value.Count > MaxKeyCount)
                {
                    throw new ArgumentException("Keys count cannot exceed " + MaxKeyCount.ToString());
                }
                _keys = value;
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

        public Read(string controlId = null) : base(controlId)
        {
            Keys = new List<int>();
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("read");

            xml.WriteElement("object", ObjectName, true);
            xml.WriteElement("keys", Keys.Count > 0 ? string.Join(",", Keys) : "", true);
            xml.WriteElement("fields", Fields.Count > 0 ? string.Join(",", Fields) : "*", true);
            xml.WriteElement("returnFormat", "xml");
            xml.WriteElement("docparid", DocParId);

            xml.WriteEndElement(); //read

            xml.WriteEndElement(); //function
        }

    }
}