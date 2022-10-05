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
using Intacct.SDK.Functions.Common.Query.Comparison.GreaterThan;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Comparison.GreaterThan
{
    public class GreaterThanDateTest
    {
        [Fact]
        public void ToStringTest()
        {
            GreaterThanDate condition = new GreaterThanDate()
            {
                Field = "CUSTOMDATE",
                Value = new DateTime(2016, 12, 31),
            };
            
            Assert.Equal("CUSTOMDATE > '12/31/2016'", condition.ToString());
        }
        
        [Fact]
        public void ToStringNotTest()
        {
            GreaterThanDate condition = new GreaterThanDate()
            {
                Negate = true,
                Field = "CUSTOMDATE",
                Value = new DateTime(2016, 12, 31),
            };
            
            Assert.Equal("NOT CUSTOMDATE > '12/31/2016'", condition.ToString());
        }
    }
}