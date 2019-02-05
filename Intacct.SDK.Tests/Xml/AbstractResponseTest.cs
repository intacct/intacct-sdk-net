using System;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;
using Xunit;

namespace Intacct.SDK.Tests.Xml
{
    public class AbstractResponseTest
    {
        [Fact]
        public void GetControlBlockTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>success</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            MockAbstractResponse response = new MockAbstractResponse(stream);
            Assert.IsType<Control>(response.Control);
        }
        
        [Fact]
        public void InvalidXmlResponseTest()
        {
            string xml = @"<bad></xml>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new MockAbstractResponse(stream));
            Assert.IsType<XmlException>(ex);
            Assert.Equal("The 'bad' start tag on line 1 position 2 does not match the end tag of 'xml'. Line 1, position 8.", ex.Message);
        }
        
        [Fact]
        public void MissingControlBlockTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <nocontrolblock/>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new MockAbstractResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Response is missing control block", ex.Message);
        }
        
        [Fact]
        public void ControlBlockFailureTest()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control>
            <status>failure</status>
            <senderid>testsenderid</senderid>
            <controlid>ControlIdHere</controlid>
            <uniqueid>false</uniqueid>
            <dtdversion>3.0</dtdversion>
      </control>
      <errormessage>
            <error>
                  <errorno>XL03000006</errorno>
                  <description></description>
                  <description2>test is not a valid transport policy.</description2>
                  <correction></correction>
            </error>
      </errormessage>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new MockAbstractResponse(stream));
            Assert.IsType<ResponseException>(ex);
            Assert.Equal("Response control status failure - XL03000006 test is not a valid transport policy.", ex.Message);
        }
    }
}