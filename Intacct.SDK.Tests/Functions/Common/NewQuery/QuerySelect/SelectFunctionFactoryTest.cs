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

using Intacct.SDK.Functions.Common.NewQuery.QuerySelect;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Common.NewQuery.QuerySelect
{
    public class SelectFunctionFactoryTest : XmlObjectTestHelper
    {
        [Fact]
        public void CreateAvgTest()
        {
            AbstractSelectFunction avg = (new SelectFunctionFactory()).Create(AbstractSelectFunction.Average, "CUSTOMERID");
            
            Assert.IsType<Average>(avg);
        }
        
        [Fact]
        public void CreateMinTest()
        {
            AbstractSelectFunction min = (new SelectFunctionFactory()).Create(AbstractSelectFunction.Minimum, "CUSTOMERID");
            
            Assert.IsType<Minimum>(min);
        }
        
        [Fact]
        public void CreateMaxTest()
        {
            AbstractSelectFunction max = (new SelectFunctionFactory()).Create(AbstractSelectFunction.Maximum, "CUSTOMERID");
            
            Assert.IsType<Maximum>(max);
        }
        
        [Fact]
        public void CreateCountTest()
        {
            AbstractSelectFunction count = (new SelectFunctionFactory()).Create(AbstractSelectFunction.Count, "CUSTOMERID");
            
            Assert.IsType<Count>(count);
        }
        
        [Fact]
        public void CreateSumTest()
        {
            AbstractSelectFunction sum = (new SelectFunctionFactory()).Create(AbstractSelectFunction.Sum, "CUSTOMERID");
            
            Assert.IsType<Sum>(sum);
        }
    }
}