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

using Intacct.SDK.Functions.Common.Query.Comparison.LessThanOrEqualTo;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Comparison.LessThanOrEqualTo
{
    public class LessThanOrEqualToDecimalTest
    {
        [Fact]
        public void ToStringTest()
        {
            LessThanOrEqualToDecimal condition = new LessThanOrEqualToDecimal()
            {
                Field = "AMOUNTDUE",
                Value = 123.45M,
            };
            
            Assert.Equal("AMOUNTDUE <= 123.45", condition.ToString());
        }
        
        [Fact]
        public void ToStringNotTest()
        {
            LessThanOrEqualToDecimal condition = new LessThanOrEqualToDecimal()
            {
                Negate = true,
                Field = "AMOUNTDUE",
                Value = 123.45M,
            };
            
            Assert.Equal("NOT AMOUNTDUE <= 123.45", condition.ToString());
        }
    }
}