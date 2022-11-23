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
using System.IO;
using System.Text;
using System.Xml;
using Intacct.SDK.Functions.GlobalConsolidations;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.GlobalConsolidations
{
    public class ConsolidationCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <consolidate>
        <bookid>USD Books</bookid>
        <reportingperiodname>Month Ended June 2016</reportingperiodname>
    </consolidate>
</function>";

            ConsolidationCreate record = new ConsolidationCreate("unittest")
            {
                ReportingBookId = "USD Books",
                ReportingPeriodName = "Month Ended June 2016"
            };
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <consolidate>
        <bookid>USD Books</bookid>
        <reportingperiodname>Month Ended June 2016</reportingperiodname>
        <offline>T</offline>
        <updatesucceedingperiods>F</updatesucceedingperiods>
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

            ConsolidationCreate record = new ConsolidationCreate("unittest")
            {
                ReportingBookId = "USD Books",
                ReportingPeriodName = "Month Ended June 2016",
                ProcessOffline = true,
                ChangesOnly = true,
                UpdateSucceedingPeriods = false,
                NotificationEmail = "noreply@intacct.com",
            };

            ConsolidationEntity entity = new ConsolidationEntity
            {
                EntityId = "VT",
                EndingSpotRate = 0.0000483500M,
                WeightedAverageRate = 0.0000485851M
            };

            record.Entities.Add(entity);
            
            this.CompareXml(expected, record);
        }
    }
}