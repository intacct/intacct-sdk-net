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

namespace Intacct.SDK.Functions.Company
{
    public class AllocationCreate : AbstractAllocation
    {

        public AllocationCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("ALLOCATION");

            xml.WriteElement("ALLOCATIONID", AllocationId, true);
            xml.WriteElement("TYPE", AllocateBy, true);

            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("DOCNUMBER", DocumentNo);
            xml.WriteElement("SUPDOCID", AttachmentsId);

            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            if (Lines.Count > 0)
            {
                xml.WriteStartElement("ALLOCATIONENTRIES");

                foreach (AbstractAllocationLine line in Lines)
                {
                    line.WriteXml(ref xml);
                }

                xml.WriteEndElement(); //ALLOCATIONENTRIES
            }

            xml.WriteEndElement(); //ALLOCATION
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}