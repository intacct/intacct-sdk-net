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
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery.QueryFilter
{
    public class Filter : IFilter
    {
        private const string EqualTo = "equalto";
        private const string NotEqualTo = "notequalto";
        private const string LessThan = "lessthan";
        private const string LessThanOrEqualTo = "lessthanorequalto";
        private const string GreaterThan = "greaterthan";
        private const string GreaterThanOrEqualTo = "greaterthanorequalto";
        private const string Between = "between";
        private const string In = "in";
        private const string NotIn = "notin";
        private const string Like = "like";
        private const string NotLike = "notlike";
        private const string IsNull = "isnull";
        private const string IsNotNull = "isnotnull";

        private readonly string _fieldName;

        private string _value;

        private List<string> _values;

        private string _operation;

        public Filter(string fieldName)
        {
            _fieldName = fieldName;
        }

        public IFilter SetEqualTo(string value)
        {
            _value = value;
            _operation = EqualTo;
            return this;
        }

        public IFilter SetNotEqualTo(string value)
        {
            _value = value;
            _operation = NotEqualTo;
            return this;
        }
        
        public IFilter SetLessThan(string value)
        {
            _value = value;
            _operation = LessThan;
            return this;
        }

        public IFilter SetLessThanOrEqualTo(string value)
        {
            _value = value;
            _operation = LessThanOrEqualTo;
            return this;
        }

        public IFilter SetGreaterThan(string value)
        {
            _value = value;
            _operation = GreaterThan;
            return this;
        }

        public IFilter SetGreaterThanOrEqualTo(string value)
        {
            _value = value;
            _operation = GreaterThanOrEqualTo;
            return this;
        }

        public IFilter SetBetween(List<string> values)
        {
            if (values != null && values.Count == 2)
            {
                _values = values;
                _operation = Between;
                return this;
            }
            else
            {
                throw new IntacctException("Two strings expected for between filter");
            }
        }

        public IFilter SetIn(List<string> values)
        {
            _values = values;
            _operation = In;
            return this;
        }

        public IFilter SetNotIn(List<string> values)
        {
            _values = values;
            _operation = NotIn;
            return this;
        }

        public IFilter SetLike(string value)
        {
            _value = value;
            _operation = Like;
            return this;
        }

        public IFilter SetNotLike(string value)
        {
            _value = value;
            _operation = NotLike;
            return this;
        }
        
        public IFilter SetIsNull()
        {
            _operation = IsNull;
            return this;
        }
        
        public IFilter SetIsNotNull()
        {
            _operation = IsNotNull;
            return this;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement(_operation);
            xml.WriteElement("field", _fieldName);
            if (_value != null)
            {
                xml.WriteElement("value", _value);
            } else if (_values != null)
            {
                foreach (var value in _values)
                {
                    xml.WriteElement("value", value);
                }
            }
            xml.WriteEndElement(); //operation
        }
    }
}