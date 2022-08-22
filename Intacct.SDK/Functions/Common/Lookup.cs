using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common
{
    public class Lookup : AbstractFunction
    {
        public string Object;
        public string DocParId;

        public Lookup(string controlId = "") : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttributeString("controlid", ControlId);

            xml.WriteStartElement("lookup");
            xml.WriteElement("object", Object);

            if (!string.IsNullOrWhiteSpace(DocParId))
            {
                xml.WriteElement("docparid", DocParId);
            }
            
            xml.WriteEndElement(); //lookup

            xml.WriteEndElement(); //function
        }
    }
}
