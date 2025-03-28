using Intacct.SDK.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intacct.SDK.Functions.InventoryControl
{
    public abstract class AbstractLineSubtotal : IXmlObject
    {
        public string OverrideDetailId;

        public decimal? Trx_tax;

        public abstract void WriteXml(ref IaXmlWriter xml);

        protected AbstractLineSubtotal()
        {
        }
    }
}
