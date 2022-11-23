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

namespace Intacct.SDK.Functions.InventoryControl
{
    public class InventoryTransactionCreate : AbstractInventoryTransaction
    {

        public InventoryTransactionCreate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("create_ictransaction");

            xml.WriteElement("transactiontype", TransactionDefinition, true);

            xml.WriteStartElement("datecreated");
            xml.WriteDateSplitElements(TransactionDate.Value, true);
            xml.WriteEndElement(); //datecreated

            xml.WriteElement("createdfrom", CreatedFrom);
            xml.WriteElement("documentno", DocumentNumber);
            xml.WriteElement("referenceno", ReferenceNumber);
            xml.WriteElement("message", Message);
            xml.WriteElement("externalid", ExternalId);
            xml.WriteElement("basecurr", BaseCurrency);
            
            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteElement("state", State);

            xml.WriteStartElement("ictransitems");
            if (Lines.Count > 0)
            {
                foreach (InventoryTransactionLineCreate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //ictransitems

            if (Subtotals.Count > 0)
            {
                xml.WriteStartElement("subtotals");
                foreach (TransactionSubtotalCreate subtotal in Subtotals)
                {
                    subtotal.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //subtotals
            }

            xml.WriteEndElement(); //create_ictransaction

            xml.WriteEndElement(); //function
        }

    }
}