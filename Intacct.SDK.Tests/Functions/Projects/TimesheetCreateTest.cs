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
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.Projects;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.Projects
{
    public class TimesheetCreateTest : XmlObjectTestHelper
    {
        [Fact]
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

            TimesheetCreate record = new TimesheetCreate("unittest")
            {
                EmployeeId = "E1234",
                BeginDate = new DateTime(2016, 06, 30)
            };

            TimesheetEntryCreate entry = new TimesheetEntryCreate
            {
                EntryDate = new DateTime(2016, 06, 30),
                Quantity = 1.75M
            };

            record.Entries.Add(entry);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
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

            TimesheetCreate record = new TimesheetCreate("unittest")
            {
                EmployeeId = "E1234",
                BeginDate = new DateTime(2016, 06, 30),
                Description = "desc",
                AttachmentsId = "A1234",
                Action = "Submitted",
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            TimesheetEntryCreate entry = new TimesheetEntryCreate
            {
                EntryDate = new DateTime(2016, 06, 30),
                Quantity = 1.75M
            };

            record.Entries.Add(entry);
            
            this.CompareXml(expected, record);
        }
    }
}