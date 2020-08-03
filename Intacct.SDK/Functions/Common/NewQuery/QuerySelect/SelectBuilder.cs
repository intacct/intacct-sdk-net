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

namespace Intacct.SDK.Functions.Common.NewQuery.QuerySelect
{
    public class SelectBuilder
    {
        private readonly List<ISelect> _selects;

        private readonly SelectFunctionFactory _factory;

        public SelectBuilder()
        {
            _selects = new List<ISelect>();

            _factory = new SelectFunctionFactory();
        }

        public SelectBuilder Field(string fieldName)
        {
            _selects.Add(new Field(fieldName));
            return this;
        }

        public SelectBuilder Fields(string[] fieldNames)
        {
            foreach (var fieldName in fieldNames)
            {
                _selects.Add(new Field(fieldName));
            }

            return this;
        }

        public SelectBuilder Average(string fieldName)
        {
            _selects.Add(_factory.Create(AbstractSelectFunction.Aggregate.Avg, fieldName));
            return this;
        }

        public SelectBuilder Count(string fieldName)
        {
            _selects.Add(_factory.Create(AbstractSelectFunction.Aggregate.Count, fieldName));
            return this;
        }

        public SelectBuilder Minimum(string fieldName)
        {
            _selects.Add(_factory.Create(AbstractSelectFunction.Aggregate.Min, fieldName));
            return this;
        }

        public SelectBuilder Maximum(string fieldName)
        {
            _selects.Add(_factory.Create(AbstractSelectFunction.Aggregate.Max, fieldName));
            return this;
        }

        public SelectBuilder Sum(string fieldName)
        {
            _selects.Add(_factory.Create(AbstractSelectFunction.Aggregate.Sum, fieldName));
            return this;
        }
        
        public ISelect[] GetFields()
        {
            return _selects.ToArray();
        }
    }
}