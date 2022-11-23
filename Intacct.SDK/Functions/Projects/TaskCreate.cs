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
    public class TaskCreate : AbstractTask
    {

        public TaskCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("TASK");

            xml.WriteElement("NAME", TaskName, true);
            xml.WriteElement("PROJECTID", ProjectId, true);

            xml.WriteElement("PBEGINDATE", PlannedBeginDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("PENDDATE", PlannedEndDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("CLASSID", ClassId);
            xml.WriteElement("ITEMID", ItemId);
            xml.WriteElement("BILLABLE", Billable);
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("ISMILESTONE", Milestone);
            xml.WriteElement("UTILIZED", Utilized);
            xml.WriteElement("PRIORITY", Priority);
            xml.WriteElement("TASKNO", WbsCode);
            xml.WriteElement("TASKSTATUS", TaskStatus);
            xml.WriteElement("TIMETYPENAME", TimeType);
            xml.WriteElement("PARENTKEY", ParentTaskRecordNo);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            // xml.WriteElement("OBSPERCENTCOMPLETE", ObservedPercentCompleted);
            xml.WriteElement("BUDGETQTY", PlannedDuration);
            xml.WriteElement("ESTQTY", EstimatedDuration);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //TASK
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}