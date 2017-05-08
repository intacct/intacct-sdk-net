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

using Intacct.Sdk.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Intacct.Sdk.Functions.Common
{

    public class ReadReport : AbstractFunction
    {

        private static readonly IList<string> ReturnFormats = new ReadOnlyCollection<string>(
            new List<string> {
                "xml",
            }
        );

        const string DefaultReturnFormat = "xml";

        const int MinPageSize = 1;

        const int MaxPageSize = 1000;

        const int DefaultPageSize = 1000;

        const int MinWaitTime = 0;

        const int MaxWaitTime = 30;

        const int DefaultWaitTime = 0;

        private string reportName;
        public string ReportName
        {
            get { return reportName; }
            set
            {
                reportName = value;
            }
        }

        // TODO: report arguments

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value < MinPageSize)
                {
                    throw new ArgumentException("Page Size cannot be less than " + MinPageSize.ToString());
                }
                else if (value > MaxPageSize)
                {
                    throw new ArgumentException("Page Size cannot be greater than " + MaxPageSize.ToString());
                }
                pageSize = value;
            }
        }

        private string returnFormat;
        public string ReturnFormat
        {
            get { return returnFormat; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = DefaultReturnFormat;
                }

                if (!ReturnFormats.Contains(value))
                {
                    throw new ArgumentException("Return Format is not a valid format");
                }

                returnFormat = value;
            }
        }

        private int waitTime;
        public int WaitTime
        {
            get { return waitTime; }
            set
            {
                if (value < MinWaitTime)
                {
                    throw new ArgumentException("Wait Time cannot be less than " + MinWaitTime.ToString());
                }
                else if (value > MaxWaitTime)
                {
                    throw new ArgumentException("Wait Time cannot be greater than " + MaxWaitTime.ToString());
                }
                waitTime = value;
            }
        }

        private string listSeparator;
        public string ListSeparator
        {
            get { return listSeparator; }
            set
            {
                listSeparator = value;
            }
        }

        public bool ReturnDef;

        public ReadReport(string controlId = null) : base(controlId)
        {
            PageSize = DefaultPageSize;
            WaitTime = DefaultWaitTime;
            ReturnFormat = DefaultReturnFormat;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("readReport");

            if (String.IsNullOrWhiteSpace(ReportName))
            {
                throw new ArgumentException("Report Name is required for read report");
            }

            if (ReturnDef == true)
            {
                xml.WriteAttribute("returnDef", true);
                xml.WriteElement("report", ReportName, true);
            }
            else
            {
                xml.WriteElement("report", ReportName, true);
                // TODO write arguments
                xml.WriteElement("waitTime", WaitTime);
                xml.WriteElement("pagesize", PageSize);
                xml.WriteElement("returnFormat", ReturnFormat);
                xml.WriteElement("listSeparator", ListSeparator);
            }
            
            xml.WriteEndElement(); //readReport

            xml.WriteEndElement(); //function
        }

    }

}