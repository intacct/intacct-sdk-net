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
using System.Text;

namespace Intacct.SDK
{
    public class RequestConfig
    {
        public string ControlId;

        public Encoding Encoding;

        private int _maxRetries;

        public int MaxRetries
        {
            get => this._maxRetries;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Max Retries must be zero or greater");
                }
                this._maxRetries = value;
            }
        }

        public TimeSpan MaxTimeout;

        private int[] _noRetryServerErrorCodes;

        public int[] NoRetryServerErrorCodes
        {
            get => this._noRetryServerErrorCodes;
            set
            {
                foreach (int errorCode in value)
                {
                    if (errorCode < 500 || errorCode > 599)
                    {
                        throw new ArgumentException("No Retry Server Error Codes must be between 500-599");
                    }
                }
                this._noRetryServerErrorCodes = value;
            }
        }

        public string PolicyId;

        public bool Transaction;

        public bool UniqueId;

        public RequestConfig()
        {
            this.ControlId = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
            this.Encoding = Encoding.GetEncoding("UTF-8");
            this.MaxRetries = 5;
            this.MaxTimeout = TimeSpan.FromSeconds(300);
            this.NoRetryServerErrorCodes = new int[] { 524 };
            this.PolicyId = "";
            this.Transaction = false;
            this.UniqueId = false;
        }
    }
}