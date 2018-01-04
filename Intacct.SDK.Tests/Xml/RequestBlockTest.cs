using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Xml
{
    public class RequestBlockTest
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?><request><control><senderid>testsenderid</senderid><password>pass123!</password><controlid>unittest</controlid><uniqueid>false</uniqueid><dtdversion>3.0</dtdversion><includewhitespace>false</includewhitespace></control><operation transaction=""false""><authentication><sessionid>testsession..</sessionid></authentication><content /></operation></request>";

            ClientConfig config = new ClientConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
            };

            RequestConfig requestConfig = new RequestConfig()
            {
                ControlId = "unittest",
            };

            List<IFunction> contentBlock = new List<IFunction>();

            RequestBlock requestBlock = new RequestBlock(config, requestConfig, contentBlock);

            Stream stream = requestBlock.WriteXml();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
    }
}