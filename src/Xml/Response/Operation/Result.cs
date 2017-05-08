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

namespace Intacct.Sdk.Xml.Response.Operation
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

        private XElement data;
        public XElement Data
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

            Status = result.Element("status").Value;
            Function = result.Element("function").Value;
            ControlId = result.Element("controlid").Value;

            if (Status != "success")
            {
                ErrorMessage errorMessage = new ErrorMessage(result.Element("errormessage").Elements("error"));

                errors = errorMessage.Errors;
            }

            if (result.Element("data") != null)
            {
                Data = result.Element("data");

                if (Data.Attribute("listtype") != null)
                {
                    ListType = Data.Attribute("listtype").Value;
                }

                if (Data.Attribute("count") != null)
                {
                    Count = int.Parse(Data.Attribute("count").Value);
                }

                if (Data.Attribute("totalcount") != null)
                {
                    TotalCount = int.Parse(Data.Attribute("totalcount").Value);
                }

                if (Data.Attribute("numremaining") != null)
                {
                    NumRemaining = int.Parse(Data.Attribute("numremaining").Value);
                }

                if (Data.Attribute("resultId") != null)
                {
                    ResultId = Data.Attribute("resultId").Value;
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
                throw new ResultException("Result status: " + Status, Errors);
            }
        }

        /// <summary>
        /// Ensure the result status is not failure (result status will be success or aborted)
        /// </summary>
        public void EnsureStatusNotFailure()
        {
            if (Status == "failure")
            {
                throw new ResultException("Result status: " + Status, Errors);
            }
        }

    }

}