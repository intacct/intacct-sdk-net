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
using Intacct.SDK.Functions.Common.Query;
using Intacct.SDK.Functions.Common.Query.Comparison.EqualTo;
using Intacct.SDK.Functions.Common.Query.Logical;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Logical
{
    public class OrConditionTest
    {
        [Fact]
        public void TestToString()
        {
            EqualToString condition1 = new EqualToString()
            {
                Field = "VENDORID",
                Value = "V1234",
            };
            EqualToString condition2 = new EqualToString()
            {
                Field = "STATUS",
                Value = "T",
            };

            OrCondition or = new OrCondition()
            {
                Conditions = new List<ICondition>
                {
                    condition1,
                    condition2
                }
            };
            
            Assert.Equal("(VENDORID = 'V1234' OR STATUS = 'T')", or.ToString());
        }
        
        [Fact]
        public void TestToStringNot()
        {
            EqualToString condition1 = new EqualToString()
            {
                Field = "VENDORID",
                Value = "V1234",
            };
            EqualToString condition2 = new EqualToString()
            {
                Field = "STATUS",
                Value = "T",
            };

            OrCondition or = new OrCondition()
            {
                Negate = true,
                Conditions = new List<ICondition>
                {
                    condition1,
                    condition2
                }
            };
            
            Assert.Equal("NOT (VENDORID = 'V1234' OR STATUS = 'T')", or.ToString());
        }
        
        [Fact]
        public void TestNestConditionsToString()
        {
            EqualToString condition1 = new EqualToString()
            {
                Field = "VENDORTYPE",
                Value = "Software",
            };
            EqualToString condition2 = new EqualToString()
            {
                Field = "VENDORID",
                Value = "V1234",
            };
            EqualToString condition3 = new EqualToString()
            {
                Field = "VENDORID",
                Value = "V5678",
            };

            OrCondition nested = new OrCondition()
            {
                Conditions = new List<ICondition>
                {
                    condition2,
                    condition3,
                }
            };

            OrCondition or = new OrCondition()
            {
                Conditions = new List<ICondition>
                {
                    condition1,
                    nested
                }
            };
            
            Assert.Equal("(VENDORTYPE = 'Software' OR (VENDORID = 'V1234' OR VENDORID = 'V5678'))", or.ToString());
        }
    }
}