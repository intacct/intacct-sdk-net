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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Intacct.Sdk.Functions.Common
{

    public class Read : AbstractFunction
    {

        private static readonly IList<string> ReturnFormats = new ReadOnlyCollection<string>(
            new List<string> {
                "xml",
            }
        );

        const string DefaultReturnFormat = "xml";

        const int MaxKeyCount = 100;

        private string objectName;
        public string ObjectName
        {
            get { return objectName; }
            set
            {
                objectName = value;
            }
        }

        public List<string> Fields = new List<string>(
            new List<string>
            {
            }
        );

        private string FieldsString()
        {
            if (Fields.Count > 0)
            {
                return string.Join(",", Fields);
            }
            else
            {
                return "*";
            }
        }

        private List<string> keys;
        public List<string> Keys
        {
            get { return keys; }
            set
            {
                if (value.Count > MaxKeyCount)
                {
                    throw new ArgumentException("Keys count cannot exceed " + MaxKeyCount.ToString());
                }
                keys = value;
            }
        }

        private string KeysString()
        {
            if (Keys.Count > 0)
            {
                return string.Join(",", Keys);
            }
            else
            {
                return "";
            }
        }

        private string returnFormat;
        public string ReturnFormat
        {
            get { return returnFormat; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = DefaultReturnFormat;
                }

                if (!ReturnFormats.Contains(value))
                {
                    throw new ArgumentException("Return Format is not a valid format");
                }

                returnFormat = value;
            }
        }

        private string docParId;
        public string DocParId
        {
            get { return docParId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = "";
                }
                docParId = value;
            }
        }

        public Read(string controlId = null) : base(controlId)
        {
            ReturnFormat = DefaultReturnFormat;
            Keys = new List<string>();
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("read");

            xml.WriteElement("object", ObjectName, true);
            xml.WriteElement("keys", KeysString(), true);
            xml.WriteElement("fields", FieldsString(), true);
            xml.WriteElement("returnFormat", ReturnFormat);
            xml.WriteElement("docparid", DocParId);

            xml.WriteEndElement(); //read

            xml.WriteEndElement(); //function
        }

    }

}