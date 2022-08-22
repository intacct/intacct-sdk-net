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
    public class AuthenticationTest
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
      <operation>
            <authentication>
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <clientid>clientCompanyId</clientid>
                  <clientstatus>active</clientstatus>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
                  <sessiontimeout>2015-10-24T22:56:52-07:00</sessiontimeout>
            </authentication>
            <result>
                  <status>success</status>
                  <function>getAPISession</function>
                  <controlid>testControlId</controlid>
                  <data>
                        <api>
                              <sessionid>faKEsesSiOnId..</sessionid>
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
            Authentication auth = response.Authentication;
            Assert.Equal("success", auth.Status);
            Assert.Equal("fakeuser", auth.UserId);
            Assert.Equal("fakecompany", auth.CompanyId);
            Assert.Equal("clientCompanyId", auth.ClientId);
            Assert.Equal("active", auth.ClientStatus);
            Assert.Equal("2015-10-24T18:56:52-07:00", auth.SessionTimeStamp);
            Assert.Equal("2015-10-24T22:56:52-07:00", auth.SessionTimeOut);
            Assert.Equal("", auth.EntityId);
        }

        [Fact]
        public void MissingStatusElementTest()
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
                  <!--<status>success</status>-->
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Authentication block is missing status element", ex.Message);
        }

        [Fact]
        public void MissingUserIdElementTest()
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
                  <!--<userid>fakeuser</userid>-->
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Authentication block is missing userid element", ex.Message);
        }
          
        [Fact]
        public void MissingCompanyIdElementTest()
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
                  <!--<companyid>fakecompany</companyid>-->
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-24T18:56:52-07:00</sessiontimestamp>
            </authentication>
            <result/>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Authentication block is missing companyid element", ex.Message);
        }
    }
}