using Intacct.SDK.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intacct.SDK.Functions.AccountsReceivable
{
    public abstract class AbstractInvoiceLineTaxEntries : IXmlObject
    {
        public string TaxId;
        public decimal? TaxValue;

        public abstract void WriteXml(ref IaXmlWriter xml);

        protected AbstractInvoiceLineTaxEntries()
        {
        }

    }

}
