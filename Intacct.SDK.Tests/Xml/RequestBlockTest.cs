/*
 * Copyright 2020 Sage Intacct, Inc.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * or in the "LICENSE" file accompanying this file. This file is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
 * express or implied. See the License for the specific language governing 
 * permissions and limitations under the License.
 */

using System.Collections.Generic;
using System.IO;
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