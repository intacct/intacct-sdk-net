using Intacct.SDK.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public class InvoiceLineTaxEntriesCreate:AbstractInvoiceLineTaxEntries
    {
        public InvoiceLineTaxEntriesCreate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            //xml.WriteStartElement("taxentries");
            xml.WriteStartElement("taxentry");

            xml.WriteElement("detailid", TaxId);
            xml.WriteElement("trx_tax", TaxValue);

            xml.WriteEndElement();//taxentry
            //xml.WriteEndElement(); //taxentries
        }
    }
}
