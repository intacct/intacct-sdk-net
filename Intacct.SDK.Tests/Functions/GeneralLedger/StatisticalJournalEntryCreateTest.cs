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
using Intacct.SDK.Functions.GeneralLedger;
using Intacct.SDK.Functions;
using Intacct.SDK.Tests.Xml;
using Intacct.SDK.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.GeneralLedger
{
    public class StatisticalJournalEntryCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <GLBATCH>
            <JOURNAL />
            <BATCH_DATE />
            <BATCH_TITLE />
            <ENTRIES>
                <GLENTRY>
                    <ACCOUNTNO />
                    <TR_TYPE>1</TR_TYPE>
                    <TRX_AMOUNT />
                </GLENTRY>
            </ENTRIES>
        </GLBATCH>
    </create>
</function>";

            StatisticalJournalEntryCreate record = new StatisticalJournalEntryCreate("unittest");

            StatisticalJournalEntryLineCreate line1 = new StatisticalJournalEntryLineCreate();

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <GLBATCH>
            <JOURNAL>SJ</JOURNAL>
            <BATCH_DATE>06/30/2016</BATCH_DATE>
            <REVERSEDATE>07/01/2016</REVERSEDATE>
            <BATCH_TITLE>My desc</BATCH_TITLE>
            <HISTORY_COMMENT>comment!</HISTORY_COMMENT>
            <REFERENCENO>123</REFERENCENO>
            <SUPDOCID>AT001</SUPDOCID>
            <STATE>Posted</STATE>
            <CUSTOMFIELD01>test01</CUSTOMFIELD01>
            <ENTRIES>
                <GLENTRY>
                    <ACCOUNTNO />
                    <TR_TYPE>1</TR_TYPE>
                    <TRX_AMOUNT />
                </GLENTRY>
            </ENTRIES>
        </GLBATCH>
    </create>
</function>";

            StatisticalJournalEntryCreate record = new StatisticalJournalEntryCreate("unittest")
            {
                JournalSymbol = "SJ",
                PostingDate = new DateTime(2016, 06, 30),
                ReverseDate = new DateTime(2016, 07, 01),
                Description = "My desc",
                HistoryComment = "comment!",
                ReferenceNumber = "123",
                AttachmentsId = "AT001",
                Action = "Posted",
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "CUSTOMFIELD01", "test01" }
                },
            };

            StatisticalJournalEntryLineCreate line1 = new StatisticalJournalEntryLineCreate();

            record.Lines.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
}