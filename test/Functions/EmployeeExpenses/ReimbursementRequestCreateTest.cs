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
using Intacct.Sdk.Functions.EmployeeExpenses;
using System;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.EmployeeExpenses
{

    [TestClass]
    public class ReimbursementRequestCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create_reimbursementrequest>
        <bankaccountid>BA1143</bankaccountid>
        <employeeid>E0001</employeeid>
        <paymentmethod>Printed Check</paymentmethod>
        <paymentdate>
            <year>2015</year>
            <month>06</month>
            <day>30</day>
        </paymentdate>
        <eppaymentrequestitems>
            <eppaymentrequestitem>
                <key>123</key>
                <paymentamount>100.12</paymentamount>
            </eppaymentrequestitem>
        </eppaymentrequestitems>
    </create_reimbursementrequest>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ReimbursementRequestCreate payment = new ReimbursementRequestCreate("unittest");
            payment.BankAccountId = "BA1143";
            payment.EmployeeId = "E0001";
            payment.PaymentMethod = AbstractReimbursementRequest.PaymentMethodCheck;
            payment.PaymentDate = new DateTime(2015, 06, 30);

            ReimbursementRequestItem line1 = new ReimbursementRequestItem();
            line1.ApplyToRecordId = 123;
            line1.AmountToApply = 100.12M;
            payment.ApplyToTransactions.Add(line1);

            payment.WriteXml(ref xml);

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
