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
using Intacct.SDK.Exceptions;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery.QueryFilter
{
    public class Filter : IFilter
    {
        protected enum Operation
        {
            EqualTo,
            NotEqualTo,
            LessThan,
            LessThanOrEqualTo,
            GreaterThan,
            GreaterThanOrEqualTo,
            Between,
            In,
            NotIn,
            Like,
            NotLike,
            IsNull,
            IsNotNull
        }

        private readonly string _fieldName;

        private string _value;

        private List<string> _values;

        private Operation _operation;

        public Filter(string fieldName)
        {
            _fieldName = fieldName;
        }

        public IFilter EqualTo(string value)
        {
            _value = value;
            _operation = Operation.EqualTo;
            return this;
        }

        public IFilter NotEqualTo(string value)
        {
            _value = value;
            _operation = Operation.NotEqualTo;
            return this;
        }
        
        public IFilter LessThan(string value)
        {
            _value = value;
            _operation = Operation.LessThan;
            return this;
        }

        public IFilter LessThanOrEqualTo(string value)
        {
            _value = value;
            _operation = Operation.LessThanOrEqualTo;
            return this;
        }

        public IFilter GreaterThan(string value)
        {
            _value = value;
            _operation = Operation.GreaterThan;
            return this;
        }

        public IFilter GreaterThanOrEqualTo(string value)
        {
            _value = value;
            _operation = Operation.GreaterThanOrEqualTo;
            return this;
        }

        public IFilter Between(List<string> values)
        {
            if (values != null && values.Count == 2)
            {
                _values = values;
                _operation = Operation.Between;
                return this;
            }
            else
            {
                throw new IntacctException("Two strings expected for between filter");
            }
        }

        public IFilter In(List<string> values)
        {
            _values = values;
            _operation = Operation.In;
            return this;
        }

        public IFilter NotIn(List<string> values)
        {
            _values = values;
            _operation = Operation.NotIn;
            return this;
        }

        public IFilter Like(string value)
        {
            _value = value;
            _operation = Operation.Like;
            return this;
        }

        public IFilter NotLike(string value)
        {
            _value = value;
            _operation = Operation.NotLike;
            return this;
        }
        
        public IFilter IsNull()
        {
            _operation = Operation.IsNull;
            return this;
        }
        
        public IFilter IsNotNull()
        {
            _operation = Operation.IsNotNull;
            return this;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement(_operation.ToString().ToLower());
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