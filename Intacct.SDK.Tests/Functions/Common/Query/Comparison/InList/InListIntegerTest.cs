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
using Intacct.SDK.Functions.Common.Query.Comparison.InList;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.Query.Comparison.InList
{
    public class InListIntegerTest
    {
        [Fact]
        public void ToStringTest()
        {
            InListInteger condition = new InListInteger()
            {
                Field = "RECORDNO",
                ValuesList = new List<int>()
                {
                    1234,
                    5678,
                    9012,
                }
            };
            
            Assert.Equal("RECORDNO IN (1234,5678,9012)", condition.ToString());
        }
        
        [Fact]
        public void ToStringNotTest()
        {
            InListInteger condition = new InListInteger()
            {
                Negate = true,
                Field = "RECORDNO",
                ValuesList = new List<int>()
                {
                    1234,
                    5678,
                    9012,
                }
            };
            
            Assert.Equal("RECORDNO NOT IN (1234,5678,9012)", condition.ToString());
        }
        
        [Fact]
        public void OneToStringTest()
        {
            InListInteger condition = new InListInteger()
            {
                Field = "RECORDNO",
                ValuesList = new List<int>()
                {
                    1234,
                }
            };
            
            Assert.Equal("RECORDNO IN (1234)", condition.ToString());
        }
    }
}