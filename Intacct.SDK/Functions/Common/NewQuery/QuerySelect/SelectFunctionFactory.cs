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

namespace Intacct.SDK.Functions.Common.NewQuery.QuerySelect
{
    public class SelectFunctionFactory
    {
        public AbstractSelectFunction Create(string functionName, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("Field name for " + functionName +  " cannot be empty or null. Provide a field name for the builder.");
            }

            AbstractSelectFunction selectFunction;

            switch (functionName)
            {
                case AbstractSelectFunction.Average:
                    selectFunction = new Average(fieldName);
                    break;
                case AbstractSelectFunction.Count:
                    selectFunction = new Count(fieldName);
                    break;
                case AbstractSelectFunction.Minimum:
                    selectFunction = new Minimum(fieldName);
                    break;
                case AbstractSelectFunction.Maximum:
                    selectFunction = new Maximum(fieldName);
                    break;
                case AbstractSelectFunction.Sum:
                    selectFunction = new Sum(fieldName);
                    break;
                default:
                    throw new ArgumentException(functionName + " function doesn't exist.");
            }

            return selectFunction;
        }
    }
}