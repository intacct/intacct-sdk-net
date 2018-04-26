using System;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Response
{
    public class ControlTest
    {
        [Fact]
        public void SuccessTest()
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
            Control control = response.Control;
            Assert.Equal("testsenderid", control.SenderId);
            Assert.Equal("ControlIdHere", control.ControlId);
            Assert.Equal("false", control.UniqueId);
            Assert.Equal("3.0", control.DtdVersion);
        }

        [Fact]
        public void MissingStatusElementTest()
        {
              string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
      <control />
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Control block is missing status element", ex.Message);
        }
    }
}