using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.OrderEntry;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;
using Intacct.SDK.Functions.InventoryControl;

namespace Intacct.SDK.Tests.Functions.InventoryControl
{
    public class LineSubtotalTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<linesubtotal>
    <overridedetailid>GST</overridedetailid>
    <trx_tax>17.5</trx_tax>
</linesubtotal>";

            var record = new LineSubtotal()
            {
                OverrideDetailId = "GST",
                Trx_tax = 17.5m
            };

            this.CompareXml(expected, record);
        }
    }
}
