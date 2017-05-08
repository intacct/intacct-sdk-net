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

namespace Intacct.Sdk.Tests.Functions.Common
{

    [TestClass]
    public class ReadReportTest
    {

        [TestMethod()]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readReport>
        <report>TestBill Date Runtime</report>
        <waitTime>0</waitTime>
        <pagesize>1000</pagesize>
        <returnFormat>xml</returnFormat>
    </readReport>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            readReport.ReportName = "TestBill Date Runtime";

            readReport.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        public void ParamOverridesTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readReport>
        <report>TestBill Date Runtime</report>
        <waitTime>15</waitTime>
        <pagesize>200</pagesize>
        <returnFormat>xml</returnFormat>
        <listSeparator>,</listSeparator>
    </readReport>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            readReport.ReportName = "TestBill Date Runtime";
            readReport.PageSize = 200;
            readReport.ReturnFormat = "xml";
            readReport.WaitTime = 15;
            readReport.ListSeparator = ",";

            readReport.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Report Name is required for read report")]
        public void NoViewTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            //readReport.ReportName = "TestBill Date Runtime";

            readReport.WriteXml(ref xml);
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

            ReadReport readReport = new ReadReport("unittest");
            readReport.PageSize = 0;
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

            ReadReport readReport = new ReadReport("unittest");
            readReport.PageSize = 1001;
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Wait Time cannot be less than 0")]
        public void MinWaitTimeTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            readReport.WaitTime = -1;
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Wait Time cannot be greater than 30")]
        public void MaxWaitTimeTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            readReport.WaitTime = 31;
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

            ReadReport readReport = new ReadReport("unittest");
            readReport.ReturnFormat = "blah";
        }

        [TestMethod()]
        public void ReturnDefTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readReport returnDef=""true"">
        <report>TestBill Date Runtime</report>
    </readReport>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadReport readReport = new ReadReport("unittest");
            readReport.ReportName = "TestBill Date Runtime";
            readReport.ReturnDef = true;

            readReport.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }
    }
    
}