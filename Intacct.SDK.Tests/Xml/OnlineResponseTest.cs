using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;
using Xunit;

namespace Intacct.SDK.Tests.Xml
{
    public class OnlineResponseTest
    {
        [Fact]
        public void GetOperationTest()
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
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-22T20:58:27-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>testControlId</controlid>
                  <data>
                        <api>
                              <sessionid>fAkESesSiOnId..</sessionid>
                              <endpoint>https://api.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                              <locationid></locationid>
                        </api>
                  </data>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            OnlineResponse response = new OnlineResponse(stream);
            Assert.IsType<List<Result>>(response.Results);
        }
        
        [Fact]
        public void MissingOperationBlockTest()
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

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Response is missing operation block", ex.Message);
        }
        
        [Fact]
        public void AuthenticationFailureTest()
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
      <operation>
            <authentication>
                  <status>failure</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
            </authentication>
            <errormessage>
                  <error>
                        <errorno>XL03000006</errorno>
                        <description></description>
                        <description2>Sign-in information is incorrect</description2>
                        <correction></correction>
                  </error>
            </errormessage>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<ResponseException>(ex);
            Assert.Equal("Response authentication status failure - XL03000006 Sign-in information is incorrect", ex.Message);
        }
        
        [Fact]
        public void MissingAuthenticationBlockTest()
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
      <operation/>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Authentication block is missing from operation element", ex.Message);
        }
        
        [Fact]
        public void MissingResultBlockTest()
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
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-22T20:58:27-07:00</sessiontimestamp>
            </authentication>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();
            
            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Result block is missing from operation element", ex.Message);
        }
        
        [Fact]
        public void ThrowResponseExceptionWithErrorsTest()
        {
              string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<response>
    <control>
        <status>failure</status>
        <senderid></senderid>
        <controlid></controlid>
    </control>
    <errormessage>
        <error>
            <errorno>PL04000055</errorno>
            <description></description>
            <description2>This company is a demo company and has expired.</description2>
            <correction></correction>
        </error>
    </errormessage>
</response>";

              Stream stream = new MemoryStream();
              StreamWriter streamWriter = new StreamWriter(stream);
              streamWriter.Write(xml);
              streamWriter.Flush();
            
              stream.Position = 0;

              var ex = Record.Exception(() => new OnlineResponse(stream));
              Assert.IsType<ResponseException>(ex);
              Assert.Equal("Response control status failure - PL04000055 This company is a demo company and has expired.", ex.Message);
        }
    }
}