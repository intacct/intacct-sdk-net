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
using Xunit;
using Intacct.SDK.Functions.Common.List;

namespace Intacct.SDK.Tests.Functions.Common.List
{
    public class LogicalFilterTests
    {
        [Fact]
        public void ParameterLessConstructor()
        {
            // arrange/act
            LogicalFilter logicalFilter = new LogicalFilter();

            // assert
            Assert.Null(logicalFilter.LogicalOperator);
            Assert.Empty(logicalFilter.ExpressionFilterList);
            Assert.Empty(logicalFilter.LogicalFilterList);
        }

        [Fact]
        public void FieldOperatorValueProvided()
        {
            ExpressionFilter expressionFilter = new ExpressionFilter()
            {
                Field = "field1",
                Operator = "=",
                Value = "field2"
            };
            // arrange/act
            LogicalFilter logicalFilter = new LogicalFilter()
            {
                LogicalOperator = "and",
                ExpressionFilterList = new List<ExpressionFilter>()
                {
                    expressionFilter
                }
            };
            
            LogicalFilter logicalFilter2 = new LogicalFilter()
            {
                LogicalOperator = "and",
                ExpressionFilterList = new List<ExpressionFilter>()
                {
                    expressionFilter
                },
                LogicalFilterList = new List<LogicalFilter>()
                {
                    logicalFilter
                }
            };

            // assert
            Assert.Equal("and", logicalFilter2.LogicalOperator);
            Assert.Equal(expressionFilter.Field, logicalFilter2.ExpressionFilterList[0].Field);
            Assert.Equal(expressionFilter.Operator, logicalFilter2.ExpressionFilterList[0].Operator);
            Assert.Equal(expressionFilter.Value, logicalFilter2.ExpressionFilterList[0].Value);
            Assert.Equal(logicalFilter.LogicalOperator, logicalFilter2.LogicalFilterList[0].LogicalOperator);
            Assert.Equal(logicalFilter.ExpressionFilterList, logicalFilter2.LogicalFilterList[0].ExpressionFilterList);
            Assert.Equal(logicalFilter.LogicalFilterList, logicalFilter2.LogicalFilterList[0].LogicalFilterList);
        }
    }

}