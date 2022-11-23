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

namespace Intacct.SDK.Functions.GeneralLedger
{
    public class JournalEntryCreate : AbstractJournalEntry
    {

        public JournalEntryCreate(string controlId = null) : base(controlId)
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
            xml.WriteElement("BASELOCATION_NO", SourceEntityId);
            xml.WriteElement("SUPDOCID", AttachmentsId);
            xml.WriteElement("STATE", Action);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteStartElement("ENTRIES");
            if (Lines.Count > 0)
            {
                foreach (JournalEntryLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //ENTRIES

            xml.WriteEndElement(); //GLBATCH
            xml.WriteEndElement(); //create

            xml.WriteEndElement(); //function
        }

    }
}