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
using Intacct.Sdk.Functions;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml.Request
{

    [TestClass]
    public class OperationBlockTest
    {

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OperationBlock operationBlock = new OperationBlock(clientConfig, new RequestConfig(), contentBlock);
            operationBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OperationBlock operationBlock = new OperationBlock(clientConfig, new RequestConfig(), contentBlock);
            operationBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            OperationBlock operationBlock = new OperationBlock(clientConfig, requestConfig, contentBlock);
            operationBlock.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Authentication credentials [Company ID, User ID, and User Password] or [Session ID] are required and cannot be blank")]
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

            OperationBlock operationBlock = new OperationBlock(clientConfig, new RequestConfig(), contentBlock);
        }

    }

}
