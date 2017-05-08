/*
 * Copyright 2017 Intacct Corporation.
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

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml.Request;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;

namespace Intacct.Sdk.Tests.Xml
{

    [TestClass]
    public class RequestBlockTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?><request><control><senderid>testsenderid</senderid><password>pass123!</password><controlid>unittest</controlid><uniqueid>false</uniqueid><dtdversion>3.0</dtdversion><includewhitespace>false</includewhitespace></control><operation transaction=""false""><authentication><sessionid>testsession..</sessionid></authentication><content /></operation></request>";

            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                SessionId = "testsession..",
                ControlId = "unittest",
            };

            Content contentBlock = new Content();

            RequestBlock requestBlock = new RequestBlock(config, contentBlock);

            Stream stream = requestBlock.WriteXml();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Requested encoding is not supported")]
        public void InvalidEncodingTest()
        {
            SdkConfig config = new SdkConfig()
            {
                Encoding = "invalid",
            };

            RequestBlock requestBlock = new RequestBlock(config, new Content());
        }
    }

}
