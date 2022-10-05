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
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.DataDeliveryService
{
    public class DdsJobCreate : AbstractFunction
    {
        public static string[] JobTypes = { "all", "change" };
        
        public static string JobTypeAll = "all";
        
        public static string JobTypeChange = "change";
        
        public static string[] FileFormats = { "unix", "windows", "mac" };
        
        public static string FileFormatUnix = "unix";
        
        public static string FileFormatWindows = "windows";
        
        public static string FileFormatMac = "mac";
        
        public static int MinSplitSize = 10000;
        
        public static int MaxSplitSize = 100000;
        
        public static int DefaultSplitSize = 100000;
        
        public string ObjectName;
            
        public string CloudDeliveryName;
        
        public string JobType;
        
        public DateTime? Timestamp;
        
        public string Delimiter;
        
        public string Enclosure;
        
        public bool? IncludeHeaders;
        
        public string FileFormat;
        
        public int? SplitSize;
        
        public bool? Compressed;
            
        public DdsJobCreate(string controlId = null) : base(controlId)
        {
        }
        
        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId);

            xml.WriteStartElement("runDdsJob");

            xml.WriteElement("object", ObjectName, true);
            xml.WriteElement("cloudDelivery", CloudDeliveryName, true);
            xml.WriteElement("jobType", JobType, true);

            if (Timestamp.HasValue) {
                xml.WriteElement("timeStamp", Timestamp.Value.ToString("s")); //s format = 2002-09-24T06:00:00
            }

            xml.WriteStartElement("fileConfiguration");

            xml.WriteElement("delimiter", Delimiter);
            xml.WriteElement("enclosure", Enclosure);
            xml.WriteElement("includeHeaders", IncludeHeaders);
            xml.WriteElement("fileFormat", FileFormat);
            xml.WriteElement("splitSize", SplitSize);
            xml.WriteElement("compress", Compressed);

            xml.WriteEndElement(); //fileConfiguration

            xml.WriteEndElement(); //runDdsJob

            xml.WriteEndElement(); //function        
        }
        
    }
}