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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml.Request;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;

namespace Intacct.Sdk.Tests.Xml.Request
{

    [TestClass]
    public class ControlBlockTest
    {

        [TestMethod()]
        public void GetXmlDefaultsTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ControlBlock controlBlock = new ControlBlock(config);
            controlBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required SenderId not supplied in params")]
        public void InvalidSenderIdTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = null,
                SenderPassword = "pass123!",
            };

            ControlBlock controlBlock = new ControlBlock(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Required SenderPassword not supplied in params")]
        public void InvalidSenderPasswordTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsender",
                SenderPassword = null,
            };

            ControlBlock controlBlock = new ControlBlock(config);
        }

        [TestMethod()]
        public void WriteXmlDefaultsOverride30Test()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                ControlId = "testcontrol",
                UniqueId = true,
                DtdVersion = "3.0",
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ControlBlock controlBlock = new ControlBlock(config);
            controlBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void WriteXmlDefaultsOverride21Test()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                ControlId = "testcontrol",
                UniqueId = true,
                DtdVersion = "2.1",
                PolicyId = "testpolicy",
                Debug = true,
            };

            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<control>
    <senderid>testsenderid</senderid>
    <password>pass123!</password>
    <controlid>testcontrol</controlid>
    <uniqueid>true</uniqueid>
    <dtdversion>2.1</dtdversion>
    <policyid>testpolicy</policyid>
    <includewhitespace>false</includewhitespace>
    <debug>true</debug>
</control>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ControlBlock controlBlock = new ControlBlock(config);
            controlBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void WriteXmlInvalidControlIdShortTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                ControlId = "unittest",
            };

            ControlBlock controlBlock = new ControlBlock(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "ControlId must be between 1 and 256 characters in length")]
        public void WriteXmlInvalidControlIdLongTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                ControlId = new StringBuilder(10 * 30).Insert(0, "1234567890", 30).ToString(),
            };

            ControlBlock controlBlock = new ControlBlock(config);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "DtdVersion is not a valid version")]
        public void WriteXmlInvalidDtdVersionTest()
        {
            SdkConfig config = new SdkConfig()
            {
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                DtdVersion = "1.2",
            };

            ControlBlock controlBlock = new ControlBlock(config);
        }
    }

}
