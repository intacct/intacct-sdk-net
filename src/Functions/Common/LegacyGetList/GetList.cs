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
using System.Collections.Generic;

namespace Intacct.Sdk.Functions.Common.LegacyGetList
{

    public class GetList : AbstractFunction
    {
        public string ObjectName;
        
        public int MaxTotalCount;
        
        public int StartAtCount;
        
        public bool? ShowPrivate = false;
        
        public List<SortField> SortFields = new List<SortField>();
        
        public List<string> ReturnFields = new List<string>();
        
        public List<FilterInterface> Filters = new List<FilterInterface>();
        
        public List<AdditionalParameter> AdditionalParameters = new List<AdditionalParameter>();

        public GetList(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("get_list");

            xml.WriteAttribute("object", ObjectName, true);

            xml.WriteAttribute("showprivate", ShowPrivate.Value);
            
            if (Filters.Count > 0)
            {
                xml.WriteStartElement("filter");
                foreach (FilterInterface Filter in Filters)
                {
                    Filter.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //filter
            }

            if (SortFields.Count > 0)
            {
                xml.WriteStartElement("sorts");
                foreach (SortField Field in SortFields)
                {
                    Field.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //sorts
            }

            if (ReturnFields.Count > 0)
            {
                xml.WriteStartElement("filter");
                foreach (string Field in ReturnFields)
                {
                    xml.WriteElement("field", Field);
                }
                xml.WriteEndElement(); //filter
            }

            if (AdditionalParameters.Count > 0)
            {
                xml.WriteStartElement("additional_parameters");
                foreach (AdditionalParameter Parameter in AdditionalParameters)
                {
                    Parameter.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //additional_parameters
            }

            xml.WriteEndElement(); //get_list

            xml.WriteEndElement(); //function
        }

    }

}