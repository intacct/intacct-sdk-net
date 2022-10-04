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
    
    public class ArAdvanceUpdate : AbstractArAdvance
    {
        public ArAdvanceUpdate(string controlId = null) : base(controlId)
        {
        }
        
        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            
            xml.WriteStartElement("ARADVANCE");
            
            xml.WriteElement("RECORDNO", RecordNo, true);
            
            xml.WriteElement("CUSTOMERID", CustomerId);
            xml.WriteElement("PAYMENTMETHOD", PaymentMethod);
            
            xml.WriteElement("PAYMENTDATE", PaymentDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("RECEIPTDATE", ReceiptDate, IaXmlWriter.IntacctDateFormat);
            
            xml.WriteElement("FINANCIALENTITY", FinancialEntity);
            xml.WriteElement("UNDEPOSITEDACCOUNTNO", UndepositedAccountNo);
            xml.WriteElement("PRBATCH", PrBatch);
            xml.WriteElement("DOCNUMBER", DocNumber);
            xml.WriteElement("DESCRIPTION", Description);
            xml.WriteElement("CURRENCY", Currency);
            xml.WriteElement("BASECURR", BaseCurrency);
            xml.WriteElement("EXCH_RATE_DATE", ExchangeRateDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("EXCH_RATE_TYPE_ID", ExchangeRateTypeId);
            xml.WriteElement("EXCHANGE_RATE", ExchangeRate);
            xml.WriteElement("SUPDOCID", AttachmentId);
            xml.WriteElement("ACTION", Action);

            xml.WriteStartElement("ARADVANCEITEMS");
            if (Lines.Count > 0)
            {
                foreach (ArAdvanceLine line in Lines)
                {
                    line.WriteXml(ref xml);
                }
            }
            xml.WriteEndElement(); //ARADVANCEITEMS
            
            xml.WriteEndElement(); //ARADVANCE

            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }
    }
}