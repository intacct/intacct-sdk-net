using Intacct.SDK.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intacct.SDK.Functions.InventoryControl
{
    public class LineSubtotal : AbstractLineSubtotal
    {
        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("linesubtotal");
            xml.WriteElement("overridedetailid", OverrideDetailId);
            xml.WriteElement("trx_tax", Trx_tax);
            xml.WriteEndElement();
        }
    }
}
