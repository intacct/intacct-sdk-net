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
using Intacct.Sdk.Functions.DataDeliveryServices;

namespace Intacct.Sdk.Tests.Functions.DataDeliveryServices
{

    [TestClass]
    public class DdsJobCreateTest
    {

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            DdsJobCreate runJob = new DdsJobCreate("unittest");
            runJob.ObjectName = "GLACCOUNT";
            runJob.CloudDeliveryName = "My Cloud Bucket";
            runJob.JobType = "all";

            runJob.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

        [TestMethod()]
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

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            DdsJobCreate runJob = new DdsJobCreate("unittest");
            runJob.ObjectName = "GLACCOUNT";
            runJob.CloudDeliveryName = "My Cloud Bucket";
            runJob.JobType = "change";
            runJob.Timestamp = new DateTime(2002, 09, 24, 06, 0, 0);
            runJob.Delimiter = ",";
            runJob.Enclosure = "\"";
            runJob.IncludeHeaders = true;
            runJob.FileFormat = "unix";
            runJob.SplitSize = 10000;
            runJob.Compressed = false;
            
            runJob.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Assert.AreEqual(expected, reader.ReadToEnd());
        }

    }

}
