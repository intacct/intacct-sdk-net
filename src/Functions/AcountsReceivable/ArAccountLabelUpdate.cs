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

namespace Intacct.Sdk.Functions.AccountsReceivable
{

    public class ArAccountLabelUpdate : AbstractArAccountLabel
    {

        public ArAccountLabelUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");

            xml.WriteStartElement("ARACCOUNTLABEL");

            xml.WriteElement("ACCOUNTLABEL", AccountLabel, true);

            xml.WriteElement("DESCRIPTION", Description);

            xml.WriteElement("GLACCOUNTNO", GlAccountNo);

            xml.WriteElement("OFFSETGLACCOUNTNO", OffsetGlAccountNo);
            
            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteEndElement(); //ARACCOUNTLABEL

            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }

}