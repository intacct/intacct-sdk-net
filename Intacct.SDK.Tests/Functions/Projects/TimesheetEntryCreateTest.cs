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
    public class TimesheetEntryCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TIMESHEETENTRY>
    <ENTRYDATE>06/30/2016</ENTRYDATE>
    <QTY />
</TIMESHEETENTRY>";

            TimesheetEntryCreate record = new TimesheetEntryCreate
            {
                EntryDate = new DateTime(2016, 06, 30)
            };

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TIMESHEETENTRY>
    <ENTRYDATE>06/30/2016</ENTRYDATE>
    <QTY>1.75</QTY>
    <DESCRIPTION>desc</DESCRIPTION>
    <NOTES>my note</NOTES>
    <TASKKEY>1234</TASKKEY>
    <TIMETYPE>Salary</TIMETYPE>
    <BILLABLE>true</BILLABLE>
    <EXTBILLRATE>200</EXTBILLRATE>
    <EXTCOSTRATE>175</EXTCOSTRATE>
    <DEPARTMENTID>ADM</DEPARTMENTID>
    <LOCATIONID>100</LOCATIONID>
    <PROJECTID>P100</PROJECTID>
    <CUSTOMERID>C100</CUSTOMERID>
    <VENDORID>V100</VENDORID>
    <ITEMID>I100</ITEMID>
    <CLASSID>C200</CLASSID>
    <CONTRACTID>C300</CONTRACTID>
    <WAREHOUSEID>W100</WAREHOUSEID>
    <customfield1>customvalue1</customfield1>
</TIMESHEETENTRY>";

            TimesheetEntryCreate record = new TimesheetEntryCreate
            {
                EntryDate = new DateTime(2016, 06, 30),
                Quantity = 1.75M,
                Description = "desc",
                Notes = "my note",
                TaskRecordNo = 1234,
                TimeTypeName = "Salary",
                Billable = true,
                OverrideBillingRate = 200,
                OverrideLaborCostRate = 175,
                DepartmentId = "ADM",
                LocationId = "100",
                ProjectId = "P100",
                CustomerId = "C100",
                VendorId = "V100",
                ItemId = "I100",
                ClassId = "C200",
                ContractId = "C300",
                WarehouseId = "W100",
                CustomFields = new Dictionary<string, dynamic>
                {
                    {"customfield1", "customvalue1"}
                }
            };

            this.CompareXml(expected, record);
        }
    }
}