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
using Intacct.SDK.Functions.DataDeliveryService;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.DataDeliveryService
{
    public class DdsJobCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <runDdsJob>
        <object>GLACCOUNT</object>
        <cloudDelivery>My Cloud Bucket</cloudDelivery>
        <jobType>all</jobType>
        <fileConfiguration />
    </runDdsJob>
</function>";

            DdsJobCreate record = new DdsJobCreate("unittest")
            {
                ObjectName = "GLACCOUNT",
                CloudDeliveryName = "My Cloud Bucket",
                JobType = "all"
            };
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <runDdsJob>
        <object>GLACCOUNT</object>
        <cloudDelivery>My Cloud Bucket</cloudDelivery>
        <jobType>change</jobType>
        <timeStamp>2002-09-24T06:00:00</timeStamp>
        <fileConfiguration>
            <delimiter>,</delimiter>
            <enclosure>""</enclosure>
            <includeHeaders>true</includeHeaders>
            <fileFormat>unix</fileFormat>
            <splitSize>10000</splitSize>
            <compress>false</compress>
        </fileConfiguration>
    </runDdsJob>
</function>";

            DdsJobCreate record = new DdsJobCreate("unittest")
            {
                ObjectName = "GLACCOUNT",
                CloudDeliveryName = "My Cloud Bucket",
                JobType = "change",
                Timestamp = new DateTime(2002, 09, 24, 06, 0, 0),
                Delimiter = ",",
                Enclosure = "\"",
                IncludeHeaders = true,
                FileFormat = "unix",
                SplitSize = 10000,
                Compressed = false
            };
            this.CompareXml(expected, record);
        }
    }
}