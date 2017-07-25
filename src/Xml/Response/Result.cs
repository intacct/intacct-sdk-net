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

using Intacct.Sdk.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Intacct.Sdk.Xml.Response
{

    public class Result
    {

        private string status;
        public string Status
        {
            get { return status; }
            private set
            {
                status = value;
            }
        }

        private string function;
        public string Function
        {
            get { return function; }
            private set
            {
                function = value;
            }
        }

        private string controlId;
        public string ControlId
        {
            get { return controlId; }
            private set
            {
                controlId = value;
            }
        }

        private List<XElement> data;
        public List<XElement> Data
        {
            get { return data;  }
            private set
            {
                data = value;
            }
        }

        private string listType;
        public string ListType
        {
            get { return listType; }
            private set
            {
                listType = value;
            }
        }

        private int count;
        public int Count
        {
            get { return count; }
            private set
            {
                count = value;
            }
        }

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            private set
            {
                totalCount = value;
            }
        }

        private int numRemaining;
        public int NumRemaining
        {
            get { return numRemaining; }
            private set
            {
                numRemaining = value;
            }
        }

        private string resultId;
        public string ResultId
        {
            get { return resultId; }
            private set
            {
                resultId = value;
            }
        }

        private string key;
        public string Key
        {
            get { return key; }
            private set
            {
                key = value;
            }
        }

        private int start;
        public int Start
        {
            get { return start; }
            private set
            {
                start = value;
            }
        }

        private int end;
        public int End
        {
            get { return end; }
            private set
            {
                end = value;
            }
        }

        private List<string> errors;
        public List<string> Errors
        {
            get { return errors; }
            private set
            {
                errors = value;
            }
        }

        public Result(XElement result)
        {
            if (result.Element("status") == null)
            {
                throw new IntacctException("Result block is missing status element");
            }
            if (result.Element("function") == null)
            {
                throw new IntacctException("Result block is missing function element");
            }
            if (result.Element("controlid") == null)
            {
                throw new IntacctException("Result block is missing controlid element");
            }

            this.Status = result.Element("status").Value;
            this.Function = result.Element("function").Value;
            this.ControlId = result.Element("controlid").Value;

            if (Status != "success")
            {
                ErrorMessage errorMessage = new ErrorMessage(result.Element("errormessage").Elements("error"));

                this.Errors = errorMessage.Errors;
            }
            else
            {
                if (result.Element("key") != null)
                {
                    this.Key = result.Element("key").Value;
                }
                else if (result.Element("listtype") != null) {
                    this.ListType = result.Element("listtype").Value;
                    this.TotalCount = int.Parse(result.Element("listtype").Attribute("total").Value);
                    this.Start = int.Parse(result.Element("listtype").Attribute("start").Value);
                    this.End = int.Parse(result.Element("listtype").Attribute("end").Value);
                }
                else if (result.Element("data").Attribute("listtype") != null)
                {
                    this.ListType = result.Element("data").Attribute("listtype").Value;
                    this.TotalCount = int.Parse(result.Element("data").Attribute("totalcount").Value);
                    this.Count = int.Parse(result.Element("data").Attribute("count").Value);
                    this.NumRemaining = int.Parse(result.Element("data").Attribute("numremaining").Value);
                    this.ResultId = result.Element("data").Attribute("resultId").Value;
                }

                if (result.Element("data") != null)
                {
                    this.Data = new List<XElement>();
                    foreach (XElement child in result.Element("data").Elements())
                    {
                        this.Data.Add(child);
                    }
                }
            }
        }

        /// <summary>
        /// Ensure the result status is success
        /// </summary>
        public void EnsureStatusSuccess()
        {
            if (Status != "success")
            {
                throw new ResultException("Result status: " + Status + " for Control ID: " + ControlId, Errors);
            }
        }

        /// <summary>
        /// Ensure the result status is not failure (result status will be success or aborted)
        /// </summary>
        public void EnsureStatusNotFailure()
        {
            if (Status == "failure")
            {
                throw new ResultException("Result status: " + Status + " for Control ID: " + ControlId, Errors);
            }
        }

    }

}