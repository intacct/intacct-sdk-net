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

using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.GlobalConsolidations;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.GlobalConsolidations
{

    [TestClass]
    public class ConsolidationCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <consolidate>
        <bookid>USD Books</bookid>
        <reportingperiodname>Month Ended June 2016</reportingperiodname>
    </consolidate>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ConsolidationCreate rate = new ConsolidationCreate("unittest");
            rate.ReportingBookId = "USD Books";
            rate.ReportingPeriodName = "Month Ended June 2016";

            rate.WriteXml(ref xml);

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
    <consolidate>
        <bookid>USD Books</bookid>
        <reportingperiodname>Month Ended June 2016</reportingperiodname>
        <offline>true</offline>
        <updatesucceedingperiods>false</updatesucceedingperiods>
        <changesonly>true</changesonly>
        <email>noreply@intacct.com</email>
        <entities>
            <csnentity>
                <entityid>VT</entityid>
                <bsrate>0.0000483500</bsrate>
                <warate>0.0000485851</warate>
            </csnentity>
        </entities>
    </consolidate>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ConsolidationCreate rate = new ConsolidationCreate("unittest")
            {
                ReportingBookId = "USD Books",
                ReportingPeriodName = "Month Ended June 2016",
                ProcessOffline = true,
                ChangesOnly = true,
                UpdateSucceedingPeriods = false,
                NotificationEmail = "noreply@intacct.com",
            };


            ConsolidationEntity entity = new ConsolidationEntity();
            entity.EntityId = "VT";
            entity.EndingSpotRate = 0.0000483500M;
            entity.WeightedAverageRate = 0.0000485851M;

            rate.Entities.Add(entity);

            rate.WriteXml(ref xml);

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
