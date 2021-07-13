using Intacct.SDK.Functions.AccountsReceivable;
using Intacct.SDK.Tests.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsReceivable
{
    public class InvoiceLineTaxEntriesCreateTest: XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<taxentry>
    <detailid>TaxName</detailid>
</taxentry>";


            InvoiceLineTaxEntriesCreate taxEntries = new InvoiceLineTaxEntriesCreate()
            {
                TaxId="TaxName"
            };
            
            this.CompareXml(expected, taxEntries);
        }
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<taxentry>
    <detailid>TaxName</detailid>
    <trx_tax>10</trx_tax>
</taxentry>";
            InvoiceLineTaxEntriesCreate taxEntries = new InvoiceLineTaxEntriesCreate()
            {
                TaxId = "TaxName",
                TaxValue=10
               
            };

            this.CompareXml(expected, taxEntries);
        }

        }
}
