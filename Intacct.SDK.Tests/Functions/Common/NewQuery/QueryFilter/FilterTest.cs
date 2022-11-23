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
    public class FilterTest
    {
        
        [Fact]
        public void EqualToTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<equalto>" + 
                                "<field>CUSTOMERID</field>" +
                                "<value>10</value>" +
                              "</equalto>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("CUSTOMERID")).SetEqualTo("10");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }

        [Fact]
        public void NotEqualToTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<notequalto>" +
                              "<field>CUSTOMERID</field>" +
                              "<value>10</value>" +
                              "</notequalto>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("CUSTOMERID")).SetNotEqualTo("10");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void LessThanTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<lessthan>" + 
                              "<field>RECORDNO</field>" +
                              "<value>100</value>" +
                              "</lessthan>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("RECORDNO")).SetLessThan("100");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void LessThanOrEqualToTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<lessthanorequalto>" + 
                              "<field>CUSTOMERID</field>" +
                              "<value>100</value>" +
                              "</lessthanorequalto>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("CUSTOMERID")).SetLessThanOrEqualTo("100");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void GreaterThanTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<greaterthan>" + 
                              "<field>RECORDNO</field>" +
                              "<value>100</value>" +
                              "</greaterthan>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("RECORDNO")).SetGreaterThan("100");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void GreaterThanOrEqualToTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<greaterthanorequalto>" + 
                              "<field>RECORDNO</field>" +
                              "<value>100</value>" +
                              "</greaterthanorequalto>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("RECORDNO")).SetGreaterThanOrEqualTo("100");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void BetweenTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<between>" + 
                              "<field>WHENDUE</field>" +
                              "<value>10/01/2019</value>" +
                              "<value>12/31/2019</value>" +
                              "</between>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("WHENDUE")).SetBetween(new List<string>(){"10/01/2019","12/31/2019"});
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void OneBetweenTest()
        {
            var ex = Record.Exception(() =>  (new Filter("RECORDNO")).SetBetween(new List<string>(){"10/01/2019"}));
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Two strings expected for between filter", ex.Message);
        }
        
        [Fact]
        public void InTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<in>" + 
                              "<field>RECORDNO</field>" +
                              "<value>04</value>" +
                              "<value>05</value>" +
                              "<value>06</value>" +
                              "<value>07</value>" +
                              "</in>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("RECORDNO")).SetIn(new List<string>(){"04","05","06","07"});
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NotInTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<notin>" + 
                              "<field>RECORDNO</field>" +
                              "<value>04</value>" +
                              "<value>05</value>" +
                              "<value>06</value>" +
                              "<value>07</value>" +
                              "</notin>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("RECORDNO")).SetNotIn(new List<string>(){"04","05","06","07"});
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void LikeTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<like>" + 
                              "<field>VENDORNAME</field>" +
                              "<value>B%</value>" +
                              "</like>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("VENDORNAME")).SetLike("B%");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void NotLikeTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<notlike>" + 
                              "<field>VENDORNAME</field>" +
                              "<value>ACME%</value>" +
                              "</notlike>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("VENDORNAME")).SetNotLike("ACME%");
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void IsNullTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<isnull>" + 
                              "<field>DESCRIPTION</field>" +
                              "</isnull>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("DESCRIPTION")).SetIsNull();
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
        
        [Fact]
        public void IsNotNullTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                              "<isnotnull>" + 
                              "<field>DESCRIPTION</field>" +
                              "</isnotnull>";
            
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IFilter filter = (new Filter("DESCRIPTION")).SetIsNotNull();
            filter.WriteXml(ref xml);
            
            xml.Flush();
            
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            
            Assert.Equal(expected, reader.ReadToEnd());
        }
    }
}