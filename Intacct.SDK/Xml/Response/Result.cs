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
using System.Xml.Linq;
using Intacct.SDK.Exceptions;

namespace Intacct.SDK.Xml.Response
{
    public class Result
    {
        public string Status { get; }

        public string Function { get; }

        public string ControlId { get; }

        public List<XElement> Data { get; }

        public string ListType { get; }

        public int Count { get; }

        public int TotalCount { get; }

        public int NumRemaining { get; }

        public string ResultId { get; }

        public string Key { get; }

        public int Start { get; }

        public int End { get; }

        public List<string> Errors { get; }

        public Result(XElement result)
        {
            var status = result.Element("status");
            if (status == null)
            {
                throw new IntacctException("Result block is missing status element");
            }
            this.Status = status.Value;
            var apiFunction = result.Element("function");
            if (apiFunction == null)
            {
                throw new IntacctException("Result block is missing function element");
            }
            this.Function = apiFunction.Value;

            var controlId = result.Element("controlid");
            if (controlId == null)
            {
                throw new IntacctException("Result block is missing controlid element");
            }
            this.ControlId = controlId.Value;

            
            if (this.Status != "success")
            {
                var errorElement = result.Element("errormessage");
                if (errorElement != null)
                {
                    ErrorMessage errorMessage = new ErrorMessage(errorElement.Elements("error"));
                    
                    this.Errors = errorMessage.Errors;
                }
            }
            else
            {
                var key = result.Element("key");
                var listType = result.Element("listtype");
                var data = result.Element("data");
                
                if (key != null)
                {
                    this.Key = key.Value;
                }
                else if (listType != null) {
                    this.ListType = listType.Value;
                    var listTotal = listType.Attribute("total");
                    if (listTotal != null)
                    {
                        this.TotalCount = int.Parse(listTotal.Value);                        
                    }
                    
                    var listStart = listType.Attribute("start");
                    if (listStart != null)
                    {
                        this.Start = int.Parse(listStart.Value);                        
                    }
                    
                    var listEnd = listType.Attribute("end");
                    if (listEnd != null)
                    {
                        this.End = int.Parse(listEnd.Value);                        
                    }
                }
                else if (data?.Attribute("listtype") != null)
                {
                    var dataListType = data.Attribute("listtype");
                    if (dataListType != null)
                    {
                        this.ListType = dataListType.Value;
                    }
                    
                    var dataTotalCount = data.Attribute("totalcount");
                    if (dataTotalCount != null)
                    {
                        this.TotalCount = int.Parse(dataTotalCount.Value);
                    }
                    
                    var dataCount = data.Attribute("count");
                    if (dataCount != null)
                    {
                        this.Count = int.Parse(dataCount.Value);                        
                    }
                    
                    var dataNumRemaining = data.Attribute("numremaining");
                    if (dataNumRemaining != null)
                    {
                        this.NumRemaining = int.Parse(dataNumRemaining.Value);
                    }
                    
                    var dataResultId = data.Attribute("resultId");
                    if (dataResultId != null)
                    {
                        this.ResultId = dataResultId.Value;
                    }
                }

                if (data != null)
                {
                    this.Data = new List<XElement>();
                    foreach (XElement child in data.Elements())
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