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
    public class TimesheetEntryCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TIMESHEETENTRY>
    <ENTRYDATE>06/30/2016</ENTRYDATE>
    <QTY />
</TIMESHEETENTRY>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            TimesheetEntryCreate entry = new TimesheetEntryCreate();
            entry.EntryDate = new DateTime(2016, 06, 30);
            
            entry.WriteXml(ref xml);

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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            
            TimesheetEntryCreate entry = new TimesheetEntryCreate();
            entry.EntryDate = new DateTime(2016, 06, 30);
            entry.Quantity = 1.75M;
            entry.Description = "desc";
            entry.Notes = "my note";
            entry.TaskRecordNo = 1234;
            entry.TimeTypeName = "Salary";
            entry.Billable = true;
            entry.OverrideBillingRate = 200;
            entry.OverrideLaborCostRate = 175;
            entry.DepartmentId = "ADM";
            entry.LocationId = "100";
            entry.ProjectId = "P100";
            entry.CustomerId = "C100";
            entry.VendorId = "V100";
            entry.ItemId = "I100";
            entry.ClassId = "C200";
            entry.ContractId = "C300";
            entry.WarehouseId = "W100";
            entry.CustomFields = new Dictionary<string, dynamic>
                {
                    { "customfield1", "customvalue1" }
                };

            entry.WriteXml(ref xml);

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
