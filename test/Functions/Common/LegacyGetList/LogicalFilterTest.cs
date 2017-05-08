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
using Intacct.Sdk.Functions.Common.LegacyGetList;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.Common.LegacyGetList
{

    [TestClass]
    public class LogicalnFilterTest
    {

        [TestMethod()]
        public void DefaultParamsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<logical logical_operator=""and"">
    <expression>
        <field>recordno</field>
        <operator>&gt;=</operator>
        <value>1234</value>
    </expression>
    <logical logical_operator=""or"">
        <expression>
            <field>ownerobject</field>
            <operator>=</operator>
            <value>PROJECT</value>
        </expression>
        <expression>
            <field>ownerobject</field>
            <operator>=</operator>
            <value>CUSTOMER</value>
        </expression>
    </logical>
</logical>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            ExpressionFilter exp1 = new ExpressionFilter();
            exp1.FieldName = "recordno";
            exp1.Operator = ExpressionFilter.OperatorGreaterThanOrEqualTo;
            exp1.Value = "1234";

            ExpressionFilter exp2 = new ExpressionFilter();
            exp2.FieldName = "ownerobject";
            exp2.Operator = ExpressionFilter.OperatorEqualTo;
            exp2.Value = "PROJECT";

            ExpressionFilter exp3 = new ExpressionFilter();
            exp3.FieldName = "ownerobject";
            exp3.Operator = ExpressionFilter.OperatorEqualTo;
            exp3.Value = "CUSTOMER";
            
            LogicalFilter logical2 = new LogicalFilter();
            logical2.Operator = LogicalFilter.OperatorOr;
            logical2.Filters.Add(exp2);
            logical2.Filters.Add(exp3);

            LogicalFilter logical1 = new LogicalFilter();
            logical1.Operator = LogicalFilter.OperatorAnd;
            logical1.Filters.Add(exp1);
            logical1.Filters.Add(logical2);

            logical1.WriteXml(ref xml);

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
