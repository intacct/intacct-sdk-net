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

using System;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.InventoryControl
{
    public class TransactionItemDetail : AbstractTransactionItemDetail
    {

        public TransactionItemDetail() : base()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("itemdetail");

            xml.WriteElement("quantity", Quantity);
            if (!string.IsNullOrWhiteSpace(SerialNumber))
            {
                xml.WriteElement("serialno", SerialNumber);
            }
            if (!string.IsNullOrWhiteSpace(LotNumber))
            {
                xml.WriteElement("lotno", LotNumber);
            }

            xml.WriteElement("aisle", Aisle);
            xml.WriteElement("row", Row);
            xml.WriteElement("bin", Bin);
            
            if (ItemExpiration.HasValue)
            {
                xml.WriteStartElement("itemexpiration");
                xml.WriteDateSplitElements(ItemExpiration.Value);
                xml.WriteEndElement(); // itemexpiration
            }

            xml.WriteEndElement(); //itemdetail
        }

    }
}