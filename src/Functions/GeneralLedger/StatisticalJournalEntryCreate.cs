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

namespace Intacct.Sdk.Functions.GeneralLedger
{

    public class StatisticalJournalEntryCreate : AbstractStatisticalJournalEntry
    {

        public StatisticalJournalEntryCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create");
            xml.WriteStartElement("GLBATCH");

            xml.WriteElement("JOURNAL", JournalSymbol, true);
            xml.WriteElement("BATCH_DATE", PostingDate, IaXmlWriter.IntacctDateFormat, true);
            xml.WriteElement("REVERSEDATE", ReverseDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("BATCH_TITLE", Description, true);
            xml.WriteElement("HISTORY_COMMENT", HistoryComment);
            xml.WriteElement("REFERENCENO", ReferenceNumber);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            xml.WriteElement("STATE", Action);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteStartElement("ENTRIES");
            if (Lines.Count > 0)
            {
                foreach (StatisticalJournalEntryLineCreate Line in Lines)
                {
                    Line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //ENTRIES

            xml.WriteEndElement(); //GLBATCH
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }

}