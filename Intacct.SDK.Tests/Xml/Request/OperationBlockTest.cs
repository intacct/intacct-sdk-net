using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Functions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Request;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Request
{
    public class OperationBlockTest : XmlObjectTestHelper
    {
        [Fact]
        public void WriteXmlSessionTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SessionId = "fakesession..",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            ApiSessionCreate func = new ApiSessionCreate()
            {
                ControlId = "unittest",
            };
            contentBlock.Add(func);

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<operation transaction=""false"">
    <authentication>
        <sessionid>fakesession..</sessionid>
    </authentication>
    <content>
        <function controlid=""unittest"">
            <getAPISession />
        </function>
    </content>
</operation>";

            OperationBlock operationBlock = new OperationBlock(clientConfig, new RequestConfig(), contentBlock);
            
            this.CompareXml(expected, operationBlock);
        }
        
        [Fact]
        public void WriteXmlLoginTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "testpass",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            ApiSessionCreate func = new ApiSessionCreate()
            {
                ControlId = "unittest",
            };
            contentBlock.Add(func);

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<operation transaction=""false"">
    <authentication>
        <login>
            <userid>testuser</userid>
            <companyid>testcompany</companyid>
            <password>testpass</password>
        </login>
    </authentication>
    <content>
        <function controlid=""unittest"">
            <getAPISession />
        </function>
    </content>
</operation>";

            OperationBlock operationBlock = new OperationBlock(clientConfig, new RequestConfig(), contentBlock);
            
            this.CompareXml(expected, operationBlock);
        }

        [Fact]
        public void WriteXmlTransactionTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SessionId = "fakesession..",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                Transaction = true,
            };

            List<IFunction> contentBlock = new List<IFunction>();
            ApiSessionCreate func = new ApiSessionCreate()
            {
                ControlId = "unittest",
            };
            contentBlock.Add(func);

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<operation transaction=""true"">
    <authentication>
        <sessionid>fakesession..</sessionid>
    </authentication>
    <content>
        <function controlid=""unittest"">
            <getAPISession />
        </function>
    </content>
</operation>";

            OperationBlock operationBlock = new OperationBlock(clientConfig, requestConfig, contentBlock);
            
            this.CompareXml(expected, operationBlock);
        }
        
        [Fact]
        public void NoCredentialsTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SessionId = "",
                CompanyId = "",
                UserId = "",
                UserPassword = "",
            };

            List<IFunction> contentBlock = new List<IFunction>();
            ApiSessionCreate func = new ApiSessionCreate();
            contentBlock.Add(func);

            var ex = Record.Exception(() => new OperationBlock(clientConfig, new RequestConfig(), contentBlock));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Authentication credentials [Company ID, User ID, and User Password] or [Session ID] are required and cannot be blank", ex.Message);
        }
    }
}