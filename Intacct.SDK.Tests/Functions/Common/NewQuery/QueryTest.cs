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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Exceptions;
using Intacct.SDK.Functions;
using Intacct.SDK.Functions.Common.NewQuery;
using Intacct.SDK.Functions.Common.NewQuery.QueryFilter;
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
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
        </select>
        <object>CUSTOMER</object>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "CUSTOMER",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }

        [Fact]
        public void AllParamsTest()
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
            <showprivate>true</showprivate>
        </options>
        <pagesize>10</pagesize>
        <offset>5</offset>
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").Field("RECORDNO").GetFields();
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "CUSTOMER",
                DocParId = "REPORT",
                SelectFields =  fields,
                CaseInsensitive = true,
                ShowPrivate = true,
                PageSize = 10,
                Offset = 5
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void EmptySelectTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                SelectFields = new ISelect[0]
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Select fields are required for query; set through method SelectFields setter.", ex.Message);
        }
        
        [Fact]
        public void MissingObjectTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            IQueryFunction query = new QueryFunction("unittest")
            {
                SelectFields =  fields
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("From Object is required for query; set through method from setter.", ex.Message);
        }
        
        [Fact]
        public void NoSelectFieldsTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject = "CUSTOMER"
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Select fields are required for query; set through method SelectFields setter.", ex.Message);
        }
        
        [Fact]
        public void EmptyObjectNameTest()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject = "",
                SelectFields =  fields
            };
            
            var ex = Record.Exception(() => query.WriteXml(ref xml));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("From Object is required for query; set through method from setter.", ex.Message);
        }
        
        [Fact]
        public void NegativeOffsetTest()
        {
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                FromObject = "CUSTOMER",
                SelectFields =  fields,
            };
            
            var ex = Record.Exception(() => query.Offset = -1);
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("Offset cannot be negative. Set Offset to zero or greater than zero.", ex.Message);
        }
        
        [Fact]
        public void NegativePagesizeTest()
        {
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            QueryFunction query = new QueryFunction("unittest")
            {
                FromObject = "CUSTOMER",
                SelectFields =  fields,
            };
            
            var ex = Record.Exception(() => query.PageSize = -1);
            Assert.IsType<IntacctException>(ex);
            Assert.Equal("PageSize cannot be negative. Set PageSize greater than zero.", ex.Message);
        }
        
        [Fact]
        public void EmptyFieldNameForAscendingTest()
        {
            var ex = Record.Exception(() => (new OrderBuilder()).Ascending(""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for field cannot be empty or null. Provide a field for the builder.", ex.Message);
        }

        [Fact]
        public void EmptyFieldNameForDescendingTest()
        {
            var ex = Record.Exception(() => (new OrderBuilder()).Descending(""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Field name for field cannot be empty or null. Provide a field for the builder.", ex.Message);
        }
        
        [Fact]
        public void FieldsTest()
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
        <options />
    </query>
</function>";

            string[] fieldNames = { "CUSTOMERID",
                "TOTALDUE",
                "WHENDUE",
                "TOTALENTERED",
                "TOTALDUE",
                "RECORDNO" };
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(fieldNames).GetFields();
            
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "ARINVOICE",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }
        [Fact]
        public void AggregateFunctionsTest()
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
        <options />
    </query>
</function>";
            
            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").
                    Average("TOTALDUE").
                    Minimum("WHENDUE").
                    Maximum("TOTALENTERED").
                    Sum("TOTALDUE").
                    Count("RECORDNO").
                    GetFields();

            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "ARINVOICE",
                SelectFields =  fields
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void OrderByTest()
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
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            IOrder[] orderBy = (new OrderBuilder()).Ascending("TOTALDUE").Descending("RECORDNO").GetOrders();
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "CLASS",
                SelectFields = fields,
                OrderBy = orderBy
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void EmptyOrderByTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
        </select>
        <object>CLASS</object>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Field("CUSTOMERID").GetFields();

            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "CLASS",
                SelectFields = fields,
                OrderBy = new IOrder[0]
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void FilterTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <field>RECORDNO</field>
        </select>
        <object>ARINVOICE</object>
        <filter>
            <lessthanorequalto>
                <field>RECORDNO</field>
                <value>10</value>
            </lessthanorequalto>
        </filter>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(new[] {"CUSTOMERID", "RECORDNO"}).GetFields();

            IFilter filter = (new Filter("RECORDNO")).SetLessThanOrEqualTo("10");
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "ARINVOICE",
                SelectFields = fields,
                Filter = filter
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void FilterAndConditionTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <field>RECORDNO</field>
        </select>
        <object>ARINVOICE</object>
        <filter>
            <and>
                <greaterthanorequalto>
                    <field>RECORDNO</field>
                    <value>1</value>
                </greaterthanorequalto>
                <lessthanorequalto>
                    <field>RECORDNO</field>
                    <value>100</value>
                </lessthanorequalto>
            </and>
        </filter>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(new[] {"CUSTOMERID","RECORDNO"}).GetFields();

            AndOperator filter = new AndOperator(new List<IFilter>());
            filter.AddFilter((new Filter("RECORDNO")).SetGreaterThanOrEqualTo("1"));
            filter.AddFilter((new Filter("RECORDNO")).SetLessThanOrEqualTo("100"));
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "ARINVOICE",
                SelectFields = fields,
                Filter = filter
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void FilterOrConditionTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>CUSTOMERID</field>
            <field>RECORDNO</field>
        </select>
        <object>ARINVOICE</object>
        <filter>
            <or>
                <lessthanorequalto>
                    <field>RECORDNO</field>
                    <value>10</value>
                </lessthanorequalto>
                <equalto>
                    <field>RECORDNO</field>
                    <value>100</value>
                </equalto>
                <equalto>
                    <field>RECORDNO</field>
                    <value>1000</value>
                </equalto>
                <equalto>
                    <field>RECORDNO</field>
                    <value>10000</value>
                </equalto>
            </or>
        </filter>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(new[] {"CUSTOMERID","RECORDNO"}).GetFields();

            OrOperator filter = new OrOperator(new List<IFilter>());
            filter.AddFilter((new Filter("RECORDNO")).SetLessThanOrEqualTo("10"));
            filter.AddFilter((new Filter("RECORDNO")).SetEqualTo("100"));
            filter.AddFilter((new Filter("RECORDNO")).SetEqualTo("1000"));
            filter.AddFilter((new Filter("RECORDNO")).SetEqualTo("10000"));
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "ARINVOICE",
                SelectFields = fields,
                Filter = filter
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void FilterOrWithAndConditionTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>BATCHNO</field>
            <field>RECORDNO</field>
            <field>STATE</field>
        </select>
        <object>GLBATCH</object>
        <filter>
            <or>
                <equalto>
                    <field>JOURNAL</field>
                    <value>APJ</value>
                </equalto>
                <and>
                    <greaterthanorequalto>
                        <field>BATCHNO</field>
                        <value>1</value>
                    </greaterthanorequalto>
                    <equalto>
                        <field>STATE</field>
                        <value>Posted</value>
                    </equalto>
                </and>
            </or>
        </filter>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(new[] {"BATCHNO","RECORDNO","STATE"}).GetFields();

            AndOperator batchnoAndState = new AndOperator(new List<IFilter>());
            batchnoAndState.AddFilter((new Filter("BATCHNO")).SetGreaterThanOrEqualTo("1"));
            batchnoAndState.AddFilter((new Filter("STATE")).SetEqualTo("Posted"));
            
            IFilter journal = new Filter("JOURNAL").SetEqualTo("APJ");
            
            IFilter filter = new OrOperator(new List<IFilter>(){journal, batchnoAndState});
            
            IQueryFunction query = new QueryFunction("unittest")
            {
                FromObject =  "GLBATCH",
                SelectFields = fields,
                Filter = filter
            };
            
            this.CompareXml(expected, query);
        }
        
        [Fact]
        public void ThreeLevelFilterTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <query>
        <select>
            <field>BATCHNO</field>
            <field>RECORDNO</field>
            <field>STATE</field>
        </select>
        <object>GLBATCH</object>
        <filter>
            <or>
                <and>
                    <equalto>
                        <field>JOURNAL</field>
                        <value>APJ</value>
                    </equalto>
                    <equalto>
                        <field>STATE</field>
                        <value>Posted</value>
                    </equalto>
                </and>
                <and>
                    <equalto>
                        <field>JOURNAL</field>
                        <value>RCPT</value>
                    </equalto>
                    <equalto>
                        <field>STATE</field>
                        <value>Posted</value>
                    </equalto>
                    <or>
                        <equalto>
                            <field>RECORDNO</field>
                            <value>168</value>
                        </equalto>
                        <equalto>
                            <field>RECORDNO</field>
                            <value>132</value>
                        </equalto>
                    </or>
                </and>
            </or>
        </filter>
        <options />
    </query>
</function>";

            SelectBuilder builder = new SelectBuilder();
            ISelect[] fields = builder.Fields(new[] {"BATCHNO","RECORDNO","STATE"}).GetFields();

            AndOperator apjAndState = new AndOperator(new List<IFilter>());
            apjAndState.AddFilter((new Filter("JOURNAL")).SetEqualTo("APJ"));
            apjAndState.AddFilter((new Filter("STATE")).SetEqualTo("Posted"));
            
            OrOperator recordnoOr = new OrOperator(new List<IFilter>());
            recordnoOr.AddFilter((new Filter("RECORDNO")).SetEqualTo("168"));
            recordnoOr.AddFilter((new Filter("RECORDNO")).SetEqualTo("132"));
            
            AndOperator rcptAndState = new AndOperator(new List<IFilter>());
            rcptAndState.AddFilter((new Filter("JOURNAL")).SetEqualTo("RCPT"));
            rcptAndState.AddFilter((new Filter("STATE")).SetEqualTo("Posted"));
            rcptAndState.AddFilter(recordnoOr);
            
            IFilter filter = new OrOperator(new List<IFilter>(){apjAndState, rcptAndState});

            IFunction query = new QueryFunction("unittest")
            {
                FromObject =  "GLBATCH",
                SelectFields = fields,
                Filter = filter
            };
            
            this.CompareXml(expected, query);
        }
    }
}