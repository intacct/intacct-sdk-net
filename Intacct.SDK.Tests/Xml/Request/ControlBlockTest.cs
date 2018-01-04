using System;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Tests.Functions;
using Intacct.SDK.Xml;
using Intacct.SDK.Xml.Request;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Request
{
    public class ControlBlockTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlDefaultsTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "unittest",
            };

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<control>
    <senderid>testsenderid</senderid>
    <password>pass123!</password>
    <controlid>unittest</controlid>
    <uniqueid>false</uniqueid>
    <dtdversion>3.0</dtdversion>
    <includewhitespace>false</includewhitespace>
</control>";

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
            
            this.CompareXml(expected, controlBlock);
        }

        [Fact]
        public void InvalidSenderIdTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                //SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            var ex = Record.Exception(() => new ControlBlock(clientConfig, new RequestConfig()));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Sender ID is required and cannot be blank", ex.Message);
        }
        
        [Fact]
        public void InvalidSenderPasswordTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                //SenderPassword = "pass123!",
            };

            var ex = Record.Exception(() => new ControlBlock(clientConfig, new RequestConfig()));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Sender Password is required and cannot be blank", ex.Message);
        }

        [Fact]
        public void WriteXmlDefaultsOverrideTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "testcontrol",
                UniqueId = true,
                PolicyId = "testpolicy",
            };

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<control>
    <senderid>testsenderid</senderid>
    <password>pass123!</password>
    <controlid>testcontrol</controlid>
    <uniqueid>true</uniqueid>
    <dtdversion>3.0</dtdversion>
    <policyid>testpolicy</policyid>
    <includewhitespace>false</includewhitespace>
</control>";

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
            
            this.CompareXml(expected, controlBlock);
        }

        [Fact]
        public void WriteXmlInvalidControlIdShortTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "",
            };

            var ex = Record.Exception(() => new ControlBlock(clientConfig, requestConfig));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Request Control ID must be between 1 and 256 characters in length", ex.Message);
        }

        [Fact]
        public void WriteXmlInvalidControlIdLongTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = new StringBuilder(10 * 30).Insert(0, "1234567890", 30).ToString(),
            };

            var ex = Record.Exception(() => new ControlBlock(clientConfig, requestConfig));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Request Control ID must be between 1 and 256 characters in length", ex.Message);
        }
    }
}