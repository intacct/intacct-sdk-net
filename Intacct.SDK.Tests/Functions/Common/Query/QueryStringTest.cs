﻿/*
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

using Intacct.SDK.Functions.Common.Query;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query
{
    public class QueryStringTest
    {
        [Fact]
        public void TestConstructorToString()
        {
            string expected = "VENDORID = 'V1234'";
            QueryString condition = new QueryString(expected);
            Assert.Equal(expected, condition.ToString());
        }
        
        [Fact]
        public void TestSetterToString()
        {
            string expected = "VENDORID = 'V1234'";
            QueryString condition = new QueryString
            {
                Query = expected
            };

            Assert.Equal(expected, condition.ToString());
        }
    }
}