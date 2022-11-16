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

namespace Intacct.SDK.Functions.GlobalConsolidations
{
    public class ConsolidationCreate : AbstractConsolidation
    {
        public ConsolidationCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("consolidate");

            xml.WriteElement("bookid", ReportingBookId, true);
            
            xml.WriteElement("reportingperiodname", ReportingPeriodName, true);
            
            switch (ProcessOffline)
            {
                case true:
                    xml.WriteElement("offline", "T");
                    break;
                case false:
                    xml.WriteElement("offline", "F");
                    break;
            }

            switch (UpdateSucceedingPeriods)
            {
                case true:
                    xml.WriteElement("updatesucceedingperiods", "T");
                    break;
                case false:
                    xml.WriteElement("updatesucceedingperiods", "F");
                    break;
            }
            
            xml.WriteElement("changesonly", ChangesOnly);
            xml.WriteElement("email", NotificationEmail);

            if (Entities.Count > 0)
            {
                xml.WriteStartElement("entities");

                foreach (ConsolidationEntity entity in Entities)
                {
                    entity.WriteXml(ref xml);
                }

                xml.WriteEndElement(); //entities
            }

            xml.WriteEndElement(); //consolidate

            xml.WriteEndElement(); //function
        }
    }
}