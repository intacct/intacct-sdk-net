using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Response;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Response
{
    public class ResultTest
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>readByQuery</function>
                  <controlid>testControlId</controlid>
                  <data listtype=""department"" count=""0"" totalcount=""0"" numremaining=""0"" resultId=""""/>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            OnlineResponse response = new OnlineResponse(stream);
            var result = response.Results[0];
            Assert.IsType<Result>(result);
            Assert.Equal("success", result.Status);
            Assert.Equal("readByQuery", result.Function);
            Assert.Equal("testControlId", result.ControlId);
            Assert.IsType<List<XElement>>(result.Data);
            result.EnsureStatusSuccess();
        }
        
        [Fact]
        public void GetErrorsTest()
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
                  <sessiontimestamp>2015-10-25T11:07:22-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>failure</status>
                  <function>readByQuery</function>
                  <controlid>testControlId</controlid>
                  <errormessage>
                        <error>
                              <errorno>Query Failed</errorno>
                              <description></description>
                              <description2>Object definition BADOBJECT not found</description2>
                              <correction></correction>
                        </error>
                  </errormessage>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            OnlineResponse response = new OnlineResponse(stream);
            var result = response.Results[0];
            Assert.Equal("failure", result.Status);
            Assert.IsType<List<string>>(result.Errors);
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
                  <status>success</status>
                  <userid>fakeuser</userid>
                  <companyid>fakecompany</companyid>
                  <locationid></locationid>
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <!--<status>success</status>-->
                  <function>readByQuery</function>
                  <controlid>testControlId</controlid>
                  <data listtype=""department"" count=""0"" totalcount=""0"" numremaining=""0"" resultId=""""/>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Result block is missing status element", ex.Message);
        }
        
        [Fact]
        public void MissingFunctionElementTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <!--<function>readByQuery</function>-->
                  <controlid>testControlId</controlid>
                  <data listtype=""department"" count=""0"" totalcount=""0"" numremaining=""0"" resultId=""""/>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Result block is missing function element", ex.Message);
        }
        
        [Fact]
        public void MissingControlIdElementTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>success</status>
                  <function>readByQuery</function>
                  <!--<controlid>testControlId</controlid>-->
                  <data listtype=""department"" count=""0"" totalcount=""0"" numremaining=""0"" resultId=""""/>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;

            var ex = Record.Exception(() => new OnlineResponse(stream));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Result block is missing controlid element", ex.Message);
        }
        
        [Fact]
        public void StatusFailureTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>failure</status>
                  <function>read</function>
                  <controlid>testFunctionId</controlid>
                  <errormessage>
                        <error>
                              <errorno>XXX</errorno>
                              <description></description>
                              <description2>Object definition VENDOR2 not found</description2>
                              <correction></correction>
                        </error>
                  </errormessage>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;
            
            OnlineResponse response = new OnlineResponse(stream);
            Result result = response.Results[0];
            
            var ex = Record.Exception(() => result.EnsureStatusNotFailure());
            Assert.IsType<ResultException>(ex);
            Assert.Equal("Result status: failure for Control ID: testFunctionId - XXX Object definition VENDOR2 not found", ex.Message);
        }
        
        [Fact]
        public void StatusAbortedTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>aborted</status>
                  <function>readByQuery</function>
                  <controlid>testFunctionId</controlid>
                  <errormessage>
                          <error>
                                <errorno>Query Failed</errorno>
                                <description></description>
                                <description2>Object definition VENDOR9 not found</description2>
                                <correction></correction>
                          </error>
                          <error>
                                <errorno>XL03000009</errorno>
                                <description></description>
                                <description2>The entire transaction in this operation has been rolled back due to an error.</description2>
                                <correction></correction>
                          </error>
                  </errormessage>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;
            
            OnlineResponse response = new OnlineResponse(stream);
            Result result = response.Results[0];
            
            var ex = Record.Exception(() => result.EnsureStatusSuccess());
            Assert.IsType<ResultException>(ex);
            Assert.Equal("Result status: aborted for Control ID: testFunctionId - Query Failed Object definition VENDOR9 not found - XL03000009 The entire transaction in this operation has been rolled back due to an error.", ex.Message);
        }
        
        [Fact]
        public void StatusNotFailuredOnAbortedTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                  <status>aborted</status>
                  <function>readByQuery</function>
                  <controlid>testFunctionId</controlid>
                  <errormessage>
                          <error>
                                <errorno>Query Failed</errorno>
                                <description></description>
                                <description2>Object definition VENDOR9 not found</description2>
                                <correction></correction>
                          </error>
                          <error>
                                <errorno>XL03000009</errorno>
                                <description></description>
                                <description2>The entire transaction in this operation has been rolled back due to an error.</description2>
                                <correction></correction>
                          </error>
                  </errormessage>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;
            
            OnlineResponse response = new OnlineResponse(stream);
            Result result = response.Results[0];
            result.EnsureStatusNotFailure();
        }
        
        [Fact]
        public void LegacyGetListClassTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                <status>success</status>
                <function>get_list</function>
                <controlid>ccdeafa7-4f22-49ae-b6ae-b5e1a39423e7</controlid>
                <listtype start=""0"" end=""1"" total=""2"">class</listtype>
                <data>
                    <class>
                        <key>C1234</key>
                        <name>hello world</name>
                        <description/>
                        <parentid/>
                        <whenmodified>07/24/2017 15:19:46</whenmodified>
                        <status>active</status>
                    </class>
                    <class>
                        <key>C1235</key>
                        <name>hello world</name>
                        <description/>
                        <parentid/>
                        <whenmodified>07/24/2017 15:20:27</whenmodified>
                        <status>active</status>
                    </class>
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
            Result result = response.Results[0];
            
            Assert.Equal(0, result.Start);
            Assert.Equal(1, result.End);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.Data.Count);
        }
        
        [Fact]
        public void ReadByQueryClassTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                <status>success</status>
                <function>readByQuery</function>
                <controlid>818b0a96-3faf-4931-97e6-1cf05818ea44</controlid>
                <data listtype=""class"" count=""1"" totalcount=""2"" numremaining=""1"" resultId=""myResultId"">
                    <class>
                        <RECORDNO>8</RECORDNO>
                        <CLASSID>C1234</CLASSID>
                        <NAME>hello world</NAME>
                        <DESCRIPTION></DESCRIPTION>
                        <STATUS>active</STATUS>
                        <PARENTKEY></PARENTKEY>
                        <PARENTID></PARENTID>
                        <PARENTNAME></PARENTNAME>
                        <WHENCREATED>07/24/2017 15:19:46</WHENCREATED>
                        <WHENMODIFIED>07/24/2017 15:19:46</WHENMODIFIED>
                        <CREATEDBY>9</CREATEDBY>
                        <MODIFIEDBY>9</MODIFIEDBY>
                        <MEGAENTITYKEY></MEGAENTITYKEY>
                        <MEGAENTITYID></MEGAENTITYID>
                        <MEGAENTITYNAME></MEGAENTITYNAME>
                    </class>
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
            Result result = response.Results[0];
            
            Assert.Equal(1, result.Count);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(1, result.NumRemaining);
            Assert.Equal("myResultId", result.ResultId);
            Assert.Single(result.Data);
        }
        
        [Fact]
        public void LegacyCreateClassKeyTest()
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
                  <sessiontimestamp>2015-10-25T10:08:34-07:00</sessiontimestamp>
            </authentication>
            <result>
                <status>success</status>
                <function>create_class</function>
                <controlid>d4814563-1e97-4708-b9c5-9a49569d2a0d</controlid>
                <key>C1234</key>
            </result>
      </operation>
</response>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(xml);
            streamWriter.Flush();

            stream.Position = 0;
            
            OnlineResponse response = new OnlineResponse(stream);
            Result result = response.Results[0];

            Assert.Equal("C1234", result.Key);
        }
    }
}