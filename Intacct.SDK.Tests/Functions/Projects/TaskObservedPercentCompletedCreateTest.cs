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
    public class TaskObservedPercentCompletedCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <OBSPCTCOMPLETED>
            <TYPE>Task</TYPE>
            <TASKKEY>1</TASKKEY>
            <ASOFDATE>02/22/2018</ASOFDATE>
            <PERCENT>33.33</PERCENT>
            <NOTE>One third complete</NOTE>
        </OBSPCTCOMPLETED>
    </create>
</function>";

            TaskObservedPercentCompletedCreate record = new TaskObservedPercentCompletedCreate("unittest")
            {
                TaskRecordNo = 1,
                AsOfDate = new DateTime(2018, 2, 22),
                ObservedPercentComplete = 33.33M,
                Description = "One third complete",
            };
            this.CompareXml(expected, record);
        }
    }
}