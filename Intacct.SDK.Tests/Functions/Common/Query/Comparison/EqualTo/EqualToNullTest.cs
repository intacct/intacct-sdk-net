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

using Intacct.SDK.Functions.Common.Query.Comparison.EqualTo;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Comparison.EqualTo
{
    public class EqualToNullTest
    {
        [Fact]
        public void ToStringTest()
        {
            EqualToNull condition = new EqualToNull()
            {
                Field = "VENDORTYPE",
            };
            
            Assert.Equal("VENDORTYPE IS NULL", condition.ToString());
        }
        
        [Fact]
        public void ToStringNotTest()
        {
            EqualToNull condition = new EqualToNull()
            {
                Negate = true,
                Field = "VENDORTYPE",
            };
            
            Assert.Equal("VENDORTYPE IS NOT NULL", condition.ToString());
        }
    }
}