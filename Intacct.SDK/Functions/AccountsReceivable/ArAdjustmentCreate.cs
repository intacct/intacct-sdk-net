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

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class ArAdjustmentCreate : AbstractArAdjustment
    {

        public ArAdjustmentCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_aradjustment");

            xml.WriteElement("customerid", CustomerId, true);
            
            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate);
            xml.WriteEndElement(); //datecreated

            if (GlPostingDate.HasValue)
            {
                xml.WriteStartElement("dateposted");
                xml.WriteDateSplitElements(GlPostingDate.Value);
                xml.WriteEndElement(); //dateposted
            }

            xml.WriteElement("batchkey", SummaryRecordNo);

            xml.WriteElement("adjustmentno", AdjustmentNumber);
            
            xml.WriteElement("action", Action);

            xml.WriteElement("invoiceno", InvoiceNumber);

            xml.WriteElement("description", Description);

            xml.WriteElement("externalid", ExternalId);

            WriteXmlMultiCurrencySection(ref xml);

            xml.WriteElement("nogl", DoNotPostToGl);
            
            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteStartElement("aradjustmentitems");

            if (Lines.Count > 0)
            {
                foreach (ArAdjustmentLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //aradjustmentitems

            xml.WriteEndElement(); //create_aradjustment

            xml.WriteEndElement(); //function
        }

    }
}