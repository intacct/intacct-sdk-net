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

using Intacct.SDK.Functions.Common.Query.Comparison.GreaterThan;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Comparison.GreaterThan
{
    public class GreaterThanDecimalTest
    {
        [Fact]
        public void ToStringTest()
        {
            GreaterThanDecimal condition = new GreaterThanDecimal()
            {
                Field = "AMOUNTDUE",
                Value = 123.45M,
            };
            
            Assert.Equal("AMOUNTDUE > 123.45", condition.ToString());
        }
        
        [Fact]
        public void ToStringNotTest()
        {
            GreaterThanDecimal condition = new GreaterThanDecimal()
            {
                Negate = true,
                Field = "AMOUNTDUE",
                Value = 123.45M,
            };
            
            Assert.Equal("NOT AMOUNTDUE > 123.45", condition.ToString());
        }
    }
}