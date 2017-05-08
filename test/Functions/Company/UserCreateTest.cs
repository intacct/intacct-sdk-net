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
using Intacct.Sdk.Functions.Company;
using System.Collections.Generic;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Functions.Company
{

    [TestClass]
    public class UserCreateTest
    {

        [TestMethod()]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <USERINFO>
            <LOGINID>U1234</LOGINID>
            <USERTYPE>business user</USERTYPE>
            <CONTACTINFO>
                <LASTNAME>Last</LASTNAME>
                <FIRSTNAME>First</FIRSTNAME>
                <EMAIL1>noreply@intacct.com</EMAIL1>
            </CONTACTINFO>
        </USERINFO>
    </create>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            UserCreate record = new UserCreate("unittest");
            record.UserId = "U1234";
            record.UserType = "business user";
            record.LastName = "Last";
            record.FirstName = "First";
            record.PrimaryEmailAddress = "noreply@intacct.com";

            record.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        public void RestrictionsTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <USERINFO>
            <LOGINID>U1234</LOGINID>
            <USERTYPE>business user</USERTYPE>
            <CONTACTINFO>
                <LASTNAME>Last</LASTNAME>
                <FIRSTNAME>First</FIRSTNAME>
                <EMAIL1>noreply@intacct.com</EMAIL1>
            </CONTACTINFO>
            <USERLOCATIONS>
                <LOCATIONID>E100</LOCATIONID>
            </USERLOCATIONS>
            <USERLOCATIONS>
                <LOCATIONID>E200</LOCATIONID>
            </USERLOCATIONS>
            <USERDEPARTMENTS>
                <DEPARTMENTID>D100</DEPARTMENTID>
            </USERDEPARTMENTS>
            <USERDEPARTMENTS>
                <DEPARTMENTID>D200</DEPARTMENTID>
            </USERDEPARTMENTS>
        </USERINFO>
    </create>
</function>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            UserCreate record = new UserCreate("unittest");
            record.UserId = "U1234";
            record.UserType = "business user";
            record.LastName = "Last";
            record.FirstName = "First";
            record.PrimaryEmailAddress = "noreply@intacct.com";
            record.RestrictedEntities = new List<string> {
                "E100",
                "E200",
            };
            record.RestrictedDepartments = new List<string> {
                "D100",
                "D200",
            };

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
