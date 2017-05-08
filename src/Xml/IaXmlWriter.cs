using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Intacct.Sdk.Xml
{
    public class IaXmlWriter : XmlWriter
    {

        public const string IntacctDateFormat = "MM/dd/yyyy";

        public const string IntacctDateTimeFormat = "MM/dd/yyyy HH:mm:ss";

        public const string IntacctMultiSelectGlue = "#~#";

        private XmlWriter writer;
        
        public override WriteState WriteState
        {
            get
            {
                return writer.WriteState;
            }
        }

        public IaXmlWriter(StringBuilder output)
        {
            writer = XmlWriter.Create(output);
        }

        public IaXmlWriter(TextWriter output)
        {
            writer = XmlWriter.Create(output);
        }

        public IaXmlWriter(XmlWriter output)
        {
            writer = XmlWriter.Create(output);
        }

        public IaXmlWriter(Stream output)
        {
            writer = XmlWriter.Create(output);
        }

        public IaXmlWriter(string outputFileName)
        {
            writer = XmlWriter.Create(outputFileName);
        }

        public IaXmlWriter(Stream output, XmlWriterSettings settings)
        {
            writer = XmlWriter.Create(output, settings);
        }

        public IaXmlWriter(TextWriter output, XmlWriterSettings settings)
        {
            writer = XmlWriter.Create(output, settings);
        }

        public IaXmlWriter(StringBuilder output, XmlWriterSettings settings)
        {
            writer = XmlWriter.Create(output, settings);
        }

        public IaXmlWriter(string outputFileName, XmlWriterSettings settings)
        {
            writer = XmlWriter.Create(outputFileName, settings);
        }

        public IaXmlWriter(XmlWriter output, XmlWriterSettings settings)
        {
            writer = XmlWriter.Create(output, settings);
        }


        public override void WriteStartDocument()
        {
            writer.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            writer.WriteStartDocument(standalone);
        }

        public override void WriteEndDocument()
        {
            writer.WriteEndDocument();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            writer.WriteDocType(name, pubid, sysid, subset);
        }
        
        public new void WriteStartElement(string localName)
        {
            writer.WriteStartElement(localName);
        }

        public new void WriteStartElement(string localName, string ns)
        {
            writer.WriteStartElement(localName, ns);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            writer.WriteStartElement(prefix, localName, ns);
        }

        public override void WriteEndElement()
        {
            writer.WriteEndElement();
        }

        public override void WriteFullEndElement()
        {
            writer.WriteFullEndElement();
        }

        public new void WriteStartAttribute(string localName)
        {
            writer.WriteStartAttribute(localName);
        }

        public new void WriteStartAttribute(string localName, string ns)
        {
            writer.WriteStartAttribute(localName, ns);
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            writer.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteEndAttribute()
        {
            writer.WriteEndAttribute();
        }

        public override void WriteCData(string text)
        {
            writer.WriteCData(text);
        }

        public override void WriteComment(string text)
        {
            writer.WriteComment(text);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            writer.WriteProcessingInstruction(name, text);
        }

        public override void WriteEntityRef(string name)
        {
            writer.WriteEntityRef(name);
        }

        public override void WriteCharEntity(char ch)
        {
            writer.WriteCharEntity(ch);
        }

        public override void WriteWhitespace(string ws)
        {
            writer.WriteWhitespace(ws);
        }

        public override void WriteString(string text)
        {
            writer.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            writer.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            writer.WriteChars(buffer, index, count);
        }
        
        public override void WriteRaw(char[] buffer, int index, int count)
        {
            writer.WriteRaw(buffer, index, count);
        }

        public override void WriteRaw(string data)
        {
            writer.WriteRaw(data);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            writer.WriteBase64(buffer, index, count);
        }
        
        public override void Flush()
        {
            writer.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return writer.LookupPrefix(ns);
        }

        // XML Helper Functions

        public void WriteElement(string localName, string value, bool writeNull = false)
        {
            if (value == null)
            {
                if (writeNull == true)
                {
                    writer.WriteElementString(localName, value);
                }
            }
            else
            {
                // if value == "" we are writing it
                writer.WriteElementString(localName, value);
            }
        }

        public void WriteElement(string localName, int value)
        {
            writer.WriteElementString(localName, value.ToString());
        }

        public void WriteElement(string localName, int? value, bool writeNull = false)
        {
            if (value == null)
            {
                if (writeNull == true)
                {
                    writer.WriteElementString(localName, "");
                }
            }
            else
            {
                writer.WriteElementString(localName, value.ToString());
            }
        }

        public void WriteElement(string localName, decimal value)
        {
            writer.WriteElementString(localName, value.ToString());
        }

        public void WriteElement(string localName, decimal? value, bool writeNull = false)
        {
            if (value == null)
            {
                if (writeNull == true)
                {
                    writer.WriteElementString(localName, "");
                }
            }
            else
            {
                writer.WriteElementString(localName, value.ToString());
            }
        }

        public void WriteElement(string localName, bool value)
        {
            string val = (value == true) ? "true" : "false";
            writer.WriteElementString(localName, val);
        }

        public void WriteElement(string localName, bool? value, bool writeNull = false)
        {
            if (value == true)
            {
                writer.WriteElementString(localName, "true");
            }
            else if (value == false)
            {
                writer.WriteElementString(localName, "false");
            }
            else
            {
                if (writeNull == true)
                {
                    writer.WriteElementString(localName, "");
                }
            }
        }
        
        public void WriteElement(string localName, DateTime value, string format = IntacctDateTimeFormat)
        {
            if (value == default(DateTime))
            {
                writer.WriteElementString(localName, "");
            }
            else
            {
                writer.WriteElementString(localName, value.ToString(format));
            }
        }

        public void WriteElement(string localName, DateTime? value, string format = IntacctDateTimeFormat, bool writeNull = false)
        {
            if (!value.HasValue)
            {
                if (writeNull == true)
                {
                    writer.WriteElementString(localName, "");
                }
            }
            else
            {
                if (value == default(DateTime))
                {
                    writer.WriteElementString(localName, "");
                }
                else
                {
                    writer.WriteElementString(localName, value.Value.ToString(format));
                }
            }
        }

        public void WriteAttribute(string localName, string value, bool writeNull = false)
        {
            if (!String.IsNullOrEmpty(value) || writeNull == true)
            {

                writer.WriteAttributeString(localName, value);
            }
        }

        public void WriteAttribute(string localName, int? value, bool writeNull = false)
        {
            if (value == null)
            {
                if (writeNull == true)
                {
                    writer.WriteAttributeString(localName, "");
                }
            }
            else
            {
                writer.WriteAttributeString(localName, value.ToString());
            }
        }

        public void WriteAttribute(string localName, bool value)
        {
            string val = (value == true) ? "true" : "false";
            writer.WriteAttributeString(localName, val);
        }

        public void WriteDateSplitElements(DateTime date, bool writeNull = true)
        {
            WriteElement("year", date.ToString("yyyy"), writeNull);
            WriteElement("month", date.ToString("MM"), writeNull);
            WriteElement("day", date.ToString("dd"), writeNull);
        }

        public void WriteCustomFieldsExplicit(Dictionary<string, dynamic> CustomFields)
        {
            if (CustomFields.Count > 0)
            {
                WriteStartElement("customfields");
                foreach (KeyValuePair<string, dynamic> customField in CustomFields)
                {
                    WriteStartElement("customfield");
                    WriteElement("customfieldname", customField.Key, true);
                    WriteElement("customfieldvalue", customField.Value, true);
                    WriteEndElement(); //customfield
                }
                WriteEndElement(); //customfields
            }
        }

        public void WriteCustomFieldsImplicit(Dictionary<string, dynamic> CustomFields)
        {
            if (CustomFields.Count > 0)
            {
                foreach (KeyValuePair<string, dynamic> customField in CustomFields)
                {
                    WriteElement(customField.Key, customField.Value, true);
                }
            }
        }

    }
}
