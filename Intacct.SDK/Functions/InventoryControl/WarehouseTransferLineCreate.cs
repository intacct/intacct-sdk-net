using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.InventoryControl
{
    public class WarehouseTransferLineCreate : AbstractWarehouseTransferLine
    {
        public WarehouseTransferLineCreate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("ICTRANSFERITEM");
            
            xml.WriteElement("IN_OUT", InOut);
            xml.WriteElement("ITEMID", ItemId);
            xml.WriteElement("WAREHOUSEID", WarehouseId);
            xml.WriteElement("MEMO", Memo);
            xml.WriteElement("QUANTITY", Quantity);
            xml.WriteElement("UNIT", Unit);
            xml.WriteElement("LOCATIONID", LocationId);
            xml.WriteElement("DEPARTMENTID", DepartmentId);
            xml.WriteElement("PROJECTID", ProjectId);
            xml.WriteElement("CUSTOMERID", CustomerId);
            xml.WriteElement("VENDORID", VendorId);
            xml.WriteElement("EMPLOYEEID", EmployeeId);
            xml.WriteElement("CLASSID", ClassId);
            
            xml.WriteEndElement(); //ICTRANSFERITEM
        }
    }
}