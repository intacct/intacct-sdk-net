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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.Projects;
using System.Collections.Generic;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.Projects
{

    [TestClass]
    public class TimesheetCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <TIMESHEET>
            <EMPLOYEEID>E1234</EMPLOYEEID>
            <BEGINDATE>06/30/2016</BEGINDATE>
            <TIMESHEETENTRIES>
                <TIMESHEETENTRY>
                    <ENTRYDATE>06/30/2016</ENTRYDATE>
                    <QTY>1.75</QTY>
                </TIMESHEETENTRY>
            </TIMESHEETENTRIES>
        </TIMESHEET>
    </create>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            TimesheetCreate record = new TimesheetCreate("unittest");
            record.EmployeeId = "E1234";
            record.BeginDate = new DateTime(2016, 06, 30);

            TimesheetEntryCreate entry = new TimesheetEntryCreate();
            entry.EntryDate = new DateTime(2016, 06, 30);
            entry.Quantity = 1.75M;

            record.Entries.Add(entry);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }
        
        [TestMethod()]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <TIMESHEET>
            <EMPLOYEEID>E1234</EMPLOYEEID>
            <BEGINDATE>06/30/2016</BEGINDATE>
            <DESCRIPTION>desc</DESCRIPTION>
            <SUPDOCID>A1234</SUPDOCID>
            <STATE>Submitted</STATE>
            <TIMESHEETENTRIES>
                <TIMESHEETENTRY>
                    <ENTRYDATE>06/30/2016</ENTRYDATE>
                    <QTY>1.75</QTY>
                </TIMESHEETENTRY>
            </TIMESHEETENTRIES>
            <customfield1>customvalue1</customfield1>
        </TIMESHEET>
    </create>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            TimesheetCreate record = new TimesheetCreate("unittest");
            record.EmployeeId = "E1234";
            record.BeginDate = new DateTime(2016, 06, 30);
            record.Description = "desc";
            record.AttachmentsId = "A1234";
            record.Action = "Submitted";
            record.CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                };
            
            TimesheetEntryCreate entry = new TimesheetEntryCreate();
            entry.EntryDate = new DateTime(2016, 06, 30);
            entry.Quantity = 1.75M;

            record.Entries.Add(entry);

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

    }

}
