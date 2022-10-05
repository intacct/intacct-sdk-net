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
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.NewQuery.QueryFilter
{
    public class OrOperatorTest
    {
        [Fact]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
"<or>" + 
    "<lessthanorequalto>" +
        "<field>RECORDNO</field>" +
        "<value>1</value>" +
    "</lessthanorequalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>100</value>" +
    "</equalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>1000</value>" +
    "</equalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>10000</value>" +
    "</equalto>" +
"</or>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            List<IFilter> filters = new List<IFilter>();
            filters.Add((new Filter("RECORDNO")).SetLessThanOrEqualTo("1"));
            filters.Add((new Filter("RECORDNO")).SetEqualTo("100"));
            filters.Add((new Filter("RECORDNO")).SetEqualTo("1000"));
            filters.Add((new Filter("RECORDNO")).SetEqualTo("10000"));
            IFilter orFilter = new OrOperator(filters);
            orFilter.WriteXml(ref xml);
            
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
            IFilter orFilter = new OrOperator(filters);

            var ex = Record.Exception(() => orFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for or", ex.Message);
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
            
            IFilter orFilter = new OrOperator(filters);

            var ex = Record.Exception(() => orFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for or", ex.Message);
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
            
            IFilter orFilter = new OrOperator(null);

            var ex = Record.Exception(() => orFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for or", ex.Message);
        }

        [Fact]
        public void AddFilterTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
"<or>" + 
    "<lessthanorequalto>" +
        "<field>RECORDNO</field>" +
        "<value>1</value>" +
    "</lessthanorequalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>100</value>" +
    "</equalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>1000</value>" +
    "</equalto>" +
    "<equalto>" +
        "<field>RECORDNO</field>" +
        "<value>10000</value>" +
    "</equalto>" +
"</or>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            OrOperator orFilter = new OrOperator(null);
            orFilter.AddFilter((new Filter("RECORDNO")).SetLessThanOrEqualTo("1"));
            orFilter.AddFilter((new Filter("RECORDNO")).SetEqualTo("100"));
            orFilter.AddFilter((new Filter("RECORDNO")).SetEqualTo("1000"));
            orFilter.AddFilter((new Filter("RECORDNO")).SetEqualTo("10000"));
            orFilter.WriteXml(ref xml);
            
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
            
            OrOperator orFilter = new OrOperator(null);
            orFilter.AddFilter(null);

            var ex = Record.Exception(() => orFilter.WriteXml(ref xml));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two or more IFilter objects required for or", ex.Message);
        }
    }
}