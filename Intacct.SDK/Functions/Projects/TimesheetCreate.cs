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

namespace Intacct.SDK.Functions.Projects
{
    public class TimesheetCreate : AbstractTimesheet
    {

        public TimesheetCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("TIMESHEET");

            xml.WriteElement("EMPLOYEEID", EmployeeId, true);
            xml.WriteElement("BEGINDATE", BeginDate, IaXmlWriter.IntacctDateFormat);
            
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            xml.WriteElement("STATE", Action);

            xml.WriteStartElement("TIMESHEETENTRIES");
            if (Entries.Count > 0)
            {
                foreach (TimesheetEntryCreate entry in Entries)
                {
                    entry.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //TIMESHEETENTRIES

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //TIMESHEET
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}