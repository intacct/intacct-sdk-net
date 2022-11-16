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

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.Common.NewQuery.QuerySelect;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.NewQuery.QuerySelect
{
    public class SelectBuilderTest : XmlObjectTestHelper
    {
        [Fact]
        public void FieldTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<field>CUSTOMERID</field>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NullFieldTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Field(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
        
        [Fact]
        public void EmptyFieldTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Field(""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
        
        [Fact]
        public void CountFieldsTest()
        {
            string[] fields = {"CUSTOMERID", "TOTALDUE", "WHENDUE", "TOTALENTERED", "TOTALDUE", "RECORDNO"};
            ISelect[] fieldList = (new SelectBuilder()).Fields(fields).GetFields();
            
            Assert.Equal(fields.Count(), fieldList.Length);
        }
        
        [Fact]
        public void EmptyFieldsTest()
        {
            string[] fields = {""};
            var ex = Record.Exception(() => (new SelectBuilder()).Fields(fields));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }

        [Fact]
        public void AverageTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<avg>PRICE</avg>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Average("PRICE").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }

        [Fact]
        public void NullAverageTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Average(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for avg cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }

        [Fact]
        public void CountTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<count>PRICE</count>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Count("PRICE").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NullCountTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Count(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for count cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
        
        [Fact]
        public void MinimumTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<min>PRICE</min>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Minimum("PRICE").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NullMinimumTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Minimum(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for min cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
        
        [Fact]
        public void MaximumTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<max>PRICE</max>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Maximum("PRICE").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NullMaximumTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Maximum(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for max cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
        
        [Fact]
        public void SumTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<sum>PRICE</sum>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Sum("PRICE").GetFields();

            foreach (var field in fields)
            {
                field.WriteXml(ref xml);
            }
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NullSumTest()
        {
            var ex = Record.Exception(() => (new SelectBuilder()).Sum(null));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for sum cannot be empty or null. Provide a field name for the builder.", ex.Message);
        }
    }
}