using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.InventoryControl
{
    public class WarehouseTransferUpdate : AbstractWarehouseTransfer
    {
        public WarehouseTransferUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("ICTRANSFER");
            
            xml.WriteElement("RECORDNO", RecordNumber);
            xml.WriteElement("ACTION", Action);
            xml.WriteElement("TRANSACTIONDATE", TransactionDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("OUTDATE", OutDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("INDATE", InDate, IaXmlWriter.IntacctDateFormat);
            xml.WriteElement("REFERENCENO", ReferenceNumber);
            xml.WriteElement("DESCRIPTION",Description);
            xml.WriteElement("EXCH_RATE_TYPE_ID", ExchangeRateTypeId);
            
            if (ExchangeRateDate.HasValue)
            {
                xml.WriteElement("EXCH_RATE_DATE", ExchangeRateDate, IaXmlWriter.IntacctDateFormat);
            }
            
            xml.WriteElement("EXCHANGE_RATE", ExchangeRate);

            if (Lines.Count > 0)
            {
                xml.WriteStartElement("ICTRANSFERITEMS");
                foreach (WarehouseTransferLineUpdate line in Lines)
                {
                    line.WriteXml(ref xml);
                }
                xml.WriteEndElement(); //ICTRANSFERITEMS
            }
            
            xml.WriteEndElement(); //ICTRANSFER
            xml.WriteEndElement(); //update
            xml.WriteEndElement(); //function
        }
    }
}