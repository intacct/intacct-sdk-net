/*
 * Copyright 2017 Intacct Corporation.
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

namespace Intacct.Sdk.Exceptions
{
    public class ResultException : OperationException
    {
        private object p;
        private string v;

        public new List<string> Errors { get; }

        public ResultException()
        {
        }

        public ResultException(string message) : base(message)
        {
        }

        public ResultException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        public ResultException(string message, List<string> errors, Exception innerException) : base(message, innerException)
        {
        }

        public ResultException(string v, object p)
        {
            this.v = v;
            this.p = p;
        }
    }
}