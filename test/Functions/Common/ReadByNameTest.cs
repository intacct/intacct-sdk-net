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
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.Common;
using System;
using System.Collections.Generic;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.Common
{

    [TestClass]
    public class ReadByNameTest
    {

        [TestMethod()]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readByName>
        <object>GLENTRY</object>
        <keys />
        <fields>*</fields>
        <returnFormat>xml</returnFormat>
    </readByName>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadByName readbyName = new ReadByName("unittest");
            readbyName.ObjectName = "GLENTRY";

            readbyName.WriteXml(ref xml);

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
    <readByName>
        <object>GLENTRY</object>
        <keys>987</keys>
        <fields>TRX_AMOUNT,RECORDNO,BATCHNO</fields>
        <returnFormat>xml</returnFormat>
        <docparid>Sales Invoice</docparid>
    </readByName>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadByName readByName = new ReadByName("unittest");
            readByName.ObjectName = "GLENTRY";
            readByName.ReturnFormat = "xml";
            readByName.Fields = new List<string> {
                "TRX_AMOUNT",
                "RECORDNO",
                "BATCHNO",
            };
            readByName.Names = new List<string> {
                "987",
            };
            readByName.DocParId = "Sales Invoice";

            readByName.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
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

            ReadByName readByName = new ReadByName("unittest");
            readByName.ObjectName = "CLASS";
            readByName.ReturnFormat = "blah";
        }
        
        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Names count cannot exceed 100")]
        public void TooManyNamesTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadByName readByName = new ReadByName("unittest");
            readByName.ObjectName = "CLASS";

            List<string> names = new List<string>();
            for (int i = 1; i <= 101; i++)
            {
                names.Add(i.ToString());
            }
            readByName.Names = names;
        }
    }

}
