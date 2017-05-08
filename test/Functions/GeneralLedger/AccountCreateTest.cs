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
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using System.Collections.Generic;
using Intacct.Sdk.Functions.GeneralLedger;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.GeneralLedger
{

    [TestClass]
    public class AccountCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <GLACCOUNT>
            <ACCOUNTNO>1010</ACCOUNTNO>
            <TITLE>hello world</TITLE>
            <ACCOUNTTYPE>balancesheet</ACCOUNTTYPE>
            <NORMALBALANCE>debit</NORMALBALANCE>
            <CLOSINGTYPE>non-closing account</CLOSINGTYPE>
        </GLACCOUNT>
    </create>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            AccountCreate record = new AccountCreate("unittest");
            record.AccountNo = "1010";
            record.Title = "hello world";
            record.AccountType = "balancesheet";
            record.NormalBalance = "debit";
            record.ClosingType = "non-closing account";

            record.WriteXml(ref xml);

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
