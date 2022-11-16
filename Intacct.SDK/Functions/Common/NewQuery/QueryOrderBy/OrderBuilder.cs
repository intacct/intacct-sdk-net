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
using System.Collections.Generic;

namespace Intacct.SDK.Functions.Common.NewQuery.QueryOrderBy
{
    public class OrderBuilder
    {
        private readonly List<IOrder> _orders;

        public OrderBuilder()
        {
            this._orders = new List<IOrder>();
        }

        public OrderBuilder Ascending(string fieldName)
        {
            this.ValidateFieldName(fieldName);
            OrderAscending orderAscending = new OrderAscending(fieldName);
            this._orders.Add(orderAscending);

            return this;
        }
        
        public OrderBuilder Descending(string fieldName)
        {
            this.ValidateFieldName(fieldName);
            OrderDescending orderDescending = new OrderDescending(fieldName);
            this._orders.Add(orderDescending);

            return this;
        }

        private void ValidateFieldName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("Field name for field cannot be empty or null. Provide a field for the builder.");
            }
        }

        public IOrder[] GetOrders()
        {
            return this._orders.ToArray();
        }
    }
}