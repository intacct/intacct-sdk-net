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
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml.Request
{

    [TestClass]
    public class ControlBlockTest
    {

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
            controlBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Sender ID is required and cannot be blank")]
        public void InvalidSenderIdTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                //SenderId = "testsenderid",
                SenderPassword = "pass123!",
            };

            ControlBlock controlBlock = new ControlBlock(clientConfig, new RequestConfig());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Sender Password is required and cannot be blank")]
        public void InvalidSenderPasswordTest()
        {
            ClientConfig clientConfig = new ClientConfig()
            {
                SenderId = "testsenderid",
                //SenderPassword = "pass123!",
            };

            ControlBlock controlBlock = new ControlBlock(clientConfig, new RequestConfig());
        }

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
            controlBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Request Control ID must be between 1 and 256 characters in length")]
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

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Request Control ID must be between 1 and 256 characters in length")]
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

            ControlBlock controlBlock = new ControlBlock(clientConfig, requestConfig);
        }
    }

}
