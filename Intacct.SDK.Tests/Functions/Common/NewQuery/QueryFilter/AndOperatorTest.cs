/*
 * Copyright 2022 Sage Intacct, Inc.
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
using System.Text;
using System.Xml;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Functions.Common.NewQuery.QueryFilter;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.NewQuery.QueryFilter
{
    public class AndOperatorTest : XmlObjectTestHelper
    {
        [Fact]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
"<and>" + 
    "<greaterthanorequalto>" +
        "<field>RECORDNO</field>" +
        "<value>1</value>" +
    "</greaterthanorequalto>" +
    "<lessthanorequalto>" +
        "<field>RECORDNO</field>" +
        "<value>100</value>" +
    "</lessthanorequalto>" +
"</and>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            List<IFilter> filters = new List<IFilter>();
            filters.Add((new Filter("RECORDNO")).SetGreaterThanOrEqualTo("1"));
            filters.Add((new Filter("RECORDNO")).SetLessThanOrEqualTo("100"));
            IFilter andFilter = new AndOperator(filters);
            andFilter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }

        [Fact]
        public void SingleFilterTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            List<IFilter> filters = new List<IFilter>();
            filters.Add((new Filter("RECORDNO")).SetGreaterThanOrEqualTo("1"));
            IFilter andFilter = new AndOperator(filters);

            var ex = Record.Exception(() => andFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for and", ex.Message);
        }

        [Fact]
        public void EmptyFilterTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            List<IFilter> filters = new List<IFilter>();
            
            IFilter andFilter = new AndOperator(filters);

            var ex = Record.Exception(() => andFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for and", ex.Message);
        }

        [Fact]
        public void NullFilterTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter andFilter = new AndOperator(null);

            var ex = Record.Exception(() => andFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for and", ex.Message);
        }

        [Fact]
        public void AddFilterTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<and>" + 
                              "<greaterthanorequalto>" +
                              "<field>RECORDNO</field>" +
                              "<value>1</value>" +
                              "</greaterthanorequalto>" +
                              "<lessthanorequalto>" +
                              "<field>RECORDNO</field>" +
                              "<value>100</value>" +
                              "</lessthanorequalto>" +
                              "</and>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            AndOperator andFilter = new AndOperator(null);
            andFilter.AddFilter((new Filter("RECORDNO")).SetGreaterThanOrEqualTo("1"));
            andFilter.AddFilter((new Filter("RECORDNO")).SetLessThanOrEqualTo("100"));
            andFilter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void AddNullFilterTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            AndOperator andFilter = new AndOperator(null);
            andFilter.AddFilter(null);

            var ex = Record.Exception(() => andFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for and", ex.Message);
        }
    }
}