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

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.Common;
using System.Collections.Generic;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.Common
{

    [TestClass]
    public class ReadViewTest
    {

        [TestMethod()]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readView>
        <view>TestBill Date Runtime</view>
        <pagesize>1000</pagesize>
        <returnFormat>xml</returnFormat>
    </readView>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            readView.ViewName = "TestBill Date Runtime";

            readView.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        public void ParamOverridesTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readView>
        <view>TestBill Date Runtime</view>
        <pagesize>10</pagesize>
        <returnFormat>xml</returnFormat>
    </readView>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            readView.ViewName = "TestBill Date Runtime";
            readView.PageSize = 10;
            readView.ReturnFormat = "xml";

            readView.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "View Name is required for read view")]
        public void NoViewTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            //readView.ViewName = "TestBill Date Runtime";

            readView.WriteXml(ref xml);
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Page Size cannot be less than 1")]
        public void MinPageSizeTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            readView.PageSize = 0;
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Page Size cannot be greater than 1000")]
        public void MaxPageSizeTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            readView.PageSize = 1001;
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Return Format is not a valid format")]
        public void InvalidReturnFormatTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadView readView = new ReadView("unittest");
            readView.ReturnFormat = "blah";
        }
    }
    
}