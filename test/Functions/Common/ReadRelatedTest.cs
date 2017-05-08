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
    public class ReadRelatedTest
    {

        [TestMethod()]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <readRelated>
        <object>CUSTOM_OBJECT</object>
        <relation>CUSTOM_OBJECT_ITEM</relation>
        <keys />
        <fields>*</fields>
        <returnFormat>xml</returnFormat>
    </readRelated>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadRelated readRelated = new ReadRelated("unittest");
            readRelated.ObjectName = "CUSTOM_OBJECT";
            readRelated.RelationName = "CUSTOM_OBJECT_ITEM";

            readRelated.WriteXml(ref xml);

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
    <readRelated>
        <object>CUSTOM_OBJECT</object>
        <relation>CUSTOM_OBJECT_ITEM</relation>
        <keys>Key1,Key2</keys>
        <fields>Field1,Field2</fields>
        <returnFormat>xml</returnFormat>
    </readRelated>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadRelated readRelated = new ReadRelated("unittest");
            readRelated.ObjectName = "CUSTOM_OBJECT";
            readRelated.RelationName = "CUSTOM_OBJECT_ITEM";
            readRelated.ReturnFormat = "xml";
            readRelated.Fields = new List<string> {
                "Field1",
                "Field2",
            };
            readRelated.Keys = new List<string> {
                "Key1",
                "Key2",
            };

            readRelated.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Relation Name is required for read related")]
        public void NoRelationTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadRelated readRelated = new ReadRelated("unittest");
            readRelated.ObjectName = "CUSTOM_OBJECT";

            readRelated.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
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

            ReadRelated readRelated = new ReadRelated("unittest");
            readRelated.ObjectName = "CUSTOM_OBJECT";
            readRelated.ReturnFormat = "bad";
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Keys count cannot exceed 100")]
        public void TooManyKeysTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReadRelated readRelated = new ReadRelated("unittest");
            readRelated.ObjectName = "CLASS";

            List<string> keys = new List<string>();
            for (int i = 1; i <= 101; i++)
            {
                keys.Add(i.ToString());
            }
            readRelated.Keys = keys;
        }
    }

}
