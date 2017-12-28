using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Intacct.SDK.Functions;
using Intacct.SDK.Logging;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Logging
{
    public class MessageFormatterTest
    {
        [Fact]
        public void RequestAndResponseRemovalTest()
        {
            ClientConfig config = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "P@ssW0rd!123",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "unittest",
            };
            
            List<IFunction> content = new List<IFunction>()
            {
                new ApiSessionCreate(),
            };

            RequestBlock xmlRequest = new RequestBlock(config, requestConfig, content);

            string xmlResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
    <control>
        <status>success</status>
        <senderid>testsenderid</senderid>
        <controlid>testControl</controlid>
        <uniqueid>false</uniqueid>
        <dtdversion>3.0</dtdversion>
    </control>
    <operation>
        <authentication>
            <status>success</status>
            <userid>testuser</userid>
            <companyid>testcompany</companyid>
            <sessiontimestamp>2016-08-22T10:58:43-07:00</sessiontimestamp>
        </authentication>
        <result>
            <status>success</status>
            <function>get_list</function>
            <controlid>test1</controlid>
            <listtype start=""0"" end=""0"" total=""1"">vendor</listtype>
            <data>
                <vendor>
                    <recordno>4</recordno>
                    <vendorid>V0004</vendorid>
                    <name>Vendor 4</name>
                    <taxid>99-9999999</taxid>
                    <achenabled>true</achenabled>
                    <achaccountnumber>1111222233334444</achaccountnumber>
                    <achaccounttype>Checking Account</achaccounttype>
                </vendor>
            </data>
        </result>
        <result>
            <status>success</status>
            <function>readByQuery</function>
            <controlid>test2</controlid>
            <data listtype=""vendor"" count=""1"" totalcount=""1"" numremaining=""0"" resultId="""">
                <vendor>
                    <RECORDNO>4</RECORDNO>
                    <VENDORID>V0004</VENDORID>
                    <NAME>Vendor 4</NAME>
                    <TAXID>99-9999999</TAXID>
                    <ACHENABLED>true</ACHENABLED>
                    <ACHACCOUNTNUMBER>1111222233334444</ACHACCOUNTNUMBER>
                    <ACHACCOUNTTYPE>Checking Account</ACHACCOUNTTYPE>
                </vendor>
            </data>
        </result>
        <result>
            <status>success</status>
            <function>getAPISession</function>
            <controlid>test3</controlid>
            <data>
                <api>
                    <sessionid>helloWorld..</sessionid>
                    <endpoint>https://unittest.intacct.com/ia/xml/xmlgw.phtml</endpoint>
                </api>
            </data>
        </result>
    </operation>
</response>";


            Stream requestStream = xmlRequest.WriteXml();
            requestStream.Position = 0;
            
            HttpRequestMessage mockRequest = new HttpRequestMessage(HttpMethod.Post, "https://unittest.intacct.com")
            {
                Content = new StreamContent(requestStream),
            };
            HttpResponseMessage mockResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xmlResponse)
            };

            MessageFormatter formatter = new MessageFormatter();
            string message = formatter.Format(mockRequest, mockResponse);
            
            Assert.DoesNotContain("<password>pass123!</password>", message);
            Assert.DoesNotContain("<password>P@ssW0rd!123</password>", message);
            Assert.Contains("<password>REDACTED</password>", message);
            
            Assert.DoesNotContain("<taxid>99-9999999</taxid>", message);
            Assert.Contains("<taxid>REDACTED</taxid>", message);
            
            Assert.DoesNotContain("<TAXID>99-9999999</TAXID>", message);
            Assert.Contains("<TAXID>REDACTED</TAXID>", message);
            
            Assert.DoesNotContain("<achaccountnumber>1111222233334444</achaccountnumber>", message);
            Assert.Contains("<achaccountnumber>REDACTED</achaccountnumber>", message);
            
            Assert.DoesNotContain("<ACHACCOUNTNUMBER>1111222233334444</ACHACCOUNTNUMBER>", message);
            Assert.Contains("<ACHACCOUNTNUMBER>REDACTED</ACHACCOUNTNUMBER>", message);
            
            Assert.DoesNotContain("<sessionid>helloWorld..</sessionid>", message);
            Assert.Contains("<sessionid>REDACTED</sessionid>", message);
        }
    }
}
