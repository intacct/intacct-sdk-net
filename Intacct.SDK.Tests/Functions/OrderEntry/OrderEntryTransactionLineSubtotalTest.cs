using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.InventoryControl;
using Intacct.SDK.Functions.OrderEntry;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.OrderEntry
{
    public class OrderEntryTransactionLineSubtotalTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<sotransitem>
    <bundlenumber>092304</bundlenumber>
    <itemid>26323</itemid>
    <itemdesc>Item Description</itemdesc>
    <taxable>true</taxable>
    <warehouseid>93294</warehouseid>
    <quantity>2340</quantity>
    <unit>593</unit>
    <linelevelsimpletaxtype>Test</linelevelsimpletaxtype>
    <discountpercent>10.00</discountpercent>
    <price>32.35</price>
    <discsurchargememo>None</discsurchargememo>
    <locationid>SF</locationid>
    <departmentid>Receiving</departmentid>
    <memo>Memo</memo>
    <customfields>
        <customfield>
            <customfieldname>customfield1</customfieldname>
            <customfieldvalue>customvalue1</customfieldvalue>
        </customfield>
    </customfields>
    <revrectemplate>template</revrectemplate>
    <revrecstartdate>
        <year>2015</year>
        <month>06</month>
        <day>30</day>
    </revrecstartdate>
    <revrecenddate>
        <year>2015</year>
        <month>07</month>
        <day>31</day>
    </revrecenddate>
    <renewalmacro>Quarterly</renewalmacro>
    <projectid>235</projectid>
    <customerid>23423434</customerid>
    <vendorid>797656</vendorid>
    <employeeid>90295</employeeid>
    <classid>243609</classid>
    <contractid>9062</contractid>
    <fulfillmentstatus>Complete</fulfillmentstatus>
    <taskno>9850</taskno>
    <billingtemplate>3525</billingtemplate>
    <linesubtotals>
        <linesubtotal>
            <overridedetailid>GST</overridedetailid>
            <trx_tax>17.5</trx_tax>
        </linesubtotal>
        <linesubtotal>
            <overridedetailid>WET</overridedetailid>
            <trx_tax>4.25</trx_tax>
        </linesubtotal>
    </linesubtotals>
</sotransitem>";

            OrderEntryTransactionLineCreate record = new OrderEntryTransactionLineCreate()
            {
                BundleNumber = "092304",
                ItemId = "26323",
                ItemDescription = "Item Description",
                Taxable = true,
                WarehouseId = "93294",
                Quantity = 2340,
                Unit = "593",
                LineLevelSimpleTaxType = "Test",
                DiscountPercent = 10.00M,
                Price = 32.35M,
                DiscountSurchargeMemo = "None",
                Memo = "Memo",
                RevRecTemplate = "template",
                RevRecStartDate = new DateTime(2015, 06, 30),
                RevRecEndDate = new DateTime(2015, 07, 31),
                RenewalMacro = "Quarterly",
                FulfillmentStatus = "Complete",
                TaskNumber = "9850",
                BillingTemplate = "3525",
                LocationId = "SF",
                DepartmentId = "Receiving",
                ProjectId = "235",
                CustomerId = "23423434",
                VendorId = "797656",
                EmployeeId = "90295",
                ClassId = "243609",
                ContractId = "9062",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                }
            };

            record.LineSubtotals.Add(new LineSubtotal()
            {
                OverrideDetailId = "GST",
                Trx_tax = 17.5m
            });

            record.LineSubtotals.Add(new LineSubtotal()
            {
                OverrideDetailId = "WET",
                Trx_tax = 4.25m
            });                        

            this.CompareXml(expected, record);
        }
    }
}
