/*
 * Copyright 2020 Sage Intacct, Inc.
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

namespace Intacct.SDK.Functions.Common.List
{
    public class LogicalFilter
    {
        public string LogicalOperator;
        public string Object; // =, !=, <, <=, >=, >, like, is null
        public List<ExpressionFilter> ExpressionFilterList;
        public List<LogicalFilter> LogicalFilterList;

        public LogicalFilter() { }
        public LogicalFilter(string logicalOperator, string filterObject, List<ExpressionFilter> expressionList, List<LogicalFilter> logicalList)
        {
            this.LogicalOperator = logicalOperator;
            this.ExpressionFilterList = expressionList;
            this.LogicalFilterList = logicalList;
        }

    }
}