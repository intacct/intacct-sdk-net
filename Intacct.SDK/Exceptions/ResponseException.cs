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

namespace Intacct.SDK.Exceptions
{
    public class ResponseException : Exception
    {

        public List<string> Errors { get; protected set; }
        
        public ResponseException()
        {
        }

        public ResponseException(string message)
            : base(message)
        {
        }

        public ResponseException(string message, List<string> errors)
            : base(ImplodeErrorsToMessage(message, errors))
        {
            Errors = errors;
        }

        public ResponseException(string message, List<string> errors, Exception innerException)
            : base(ImplodeErrorsToMessage(message, errors), innerException)
        {
        }

        private static string ImplodeErrorsToMessage(string message, List<string> errors)
        {
            if (errors.Count > 0)
            {
                message = message + " - " + string.Join(" - ", errors);
            }

            return message;
        }
    }
}