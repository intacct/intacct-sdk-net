/*
 * Copyright 2020 Sage Intacct, Inc.
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.Common.NewQuery;
using Intacct.SDK.Functions;
using Intacct.SDK.Functions.Common.NewQuery.QueryOrderBy;
using Intacct.SDK.Functions.Common.NewQuery.QuerySelect;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.NewQuery
{
    public class QueryTest : XmlObjectTestHelper
    {
        [Fact]
        public void TestDefaultParams()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
        </select>
        <object>CUSTOMER</object>
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();
            
            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName =  "CUSTOMER",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }

        [Fact]
        public void TestALLParams()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <field>RECORDNO</field>
        </select>
        <object>CUSTOMER</object>
        <docparid>REPORT</docparid>
        <options>
            <caseinsensitive>true</caseinsensitive>
        </options>
        <pagesize>10</pagesize>
        <offset>5</offset>
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").Field("RECORDNO").GetFields();
            
            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName =  "CUSTOMER",
                DocParId = "REPORT",
                SelectFields =  fields,
                CaseInsensitive = true,
                PageSize = 10,
                Offset = 5
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void TestEmptySelect()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            QueryFunction query = new QueryFunction("unittest")
            {
                SelectFields = new List<ISelect>()
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Select fields are required for query; set through method SelectFields setter.", ex.Message);
        }
        
        [Fact]
        public void TestMissingObject()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                SelectFields =  fields
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Object Name is required for query; set through method from setter.", ex.Message);
        }
        
        [Fact]
        public void TestNotSelectFields()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName = "CUSTOMER"
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Select fields are required for query; set through method SelectFields setter.", ex.Message);
        }
        
        [Fact]
        public void TestEmptyObjectName()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName = "",
                SelectFields =  fields
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Object Name is required for query; set through method from setter.", ex.Message);
        }
        
        [Fact]
        public void TestNegativeOffset()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName = "CUSTOMER",
                SelectFields =  fields,
            };
            
            var ex = Record.Exception(() => query.Offset = -1);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Offset cannot be negative. Set Offset to zero or greater than zero.", ex.Message);
        }
        
        [Fact]
        public void TestNegativePagesize()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName = "CUSTOMER",
                SelectFields =  fields,
            };
            
            var ex = Record.Exception(() => query.PageSize = -1);
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("PageSize cannot be negative. Set PageSize greater than zero.", ex.Message);
        }
        
        [Fact]
        public void TestFields()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <field>TOTALDUE</field>
            <field>WHENDUE</field>
            <field>TOTALENTERED</field>
            <field>TOTALDUE</field>
            <field>RECORDNO</field>
        </select>
        <object>ARINVOICE</object>
    </query>
</function>";

            string[] fieldNames = { "CUSTOMERID",
                "TOTALDUE",
                "WHENDUE",
                "TOTALENTERED",
                "TOTALDUE",
                "RECORDNO" };
            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Fields(fieldNames).GetFields();
            
            
            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName =  "ARINVOICE",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }
        [Fact]
        public void TestAggregateFunctions()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <avg>TOTALDUE</avg>
            <min>WHENDUE</min>
            <max>TOTALENTERED</max>
            <sum>TOTALDUE</sum>
            <count>RECORDNO</count>
        </select>
        <object>ARINVOICE</object>
    </query>
</function>";

            string[] fieldNames = { "CUSTOMERID",
                "TOTALDUE",
                "WHENDUE",
                "TOTALENTERED",
                "TOTALDUE",
                "RECORDNO" };
            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").
                    Average("TOTALDUE").
                    Minimum("WHENDUE").
                    Maximum("TOTALENTERED").
                    Sum("TOTALDUE").
                    Count("RECORDNO").
                    GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName =  "ARINVOICE",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void TestOrderBy()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
        </select>
        <object>CLASS</object>
        <orderby>
            <order>
                <field>TOTALDUE</field>
                <ascending />
            </order>
            <order>
                <field>RECORDNO</field>
                <descending />
            </order>
        </orderby>
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            List<ISelect> fields = builder.Field("CUSTOMERID").GetFields();

            List<IOrder> orderBy = (new OrderBuilder()).Ascending("TOTALDUE").Descending("RECORDNO").getOrders();
            
            QueryFunction query = new QueryFunction("unittest")
            {
                ObjectName =  "CLASS",
                SelectFields = fields,
                OrderBy = orderBy
            };
            
            this.CompareXml(expected, query);
        }
    }
}