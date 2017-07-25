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
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Xml;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;
using Intacct.Sdk.Xml.Request;

namespace Intacct.Sdk.Tests.Xml.Request
{

    [TestClass]
    public class LoginAuthenticationTest
    {

        [TestMethod()]
        public void WriteXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <login>
        <userid>testuser</userid>
        <companyid>testcompany</companyid>
        <password>testpass</password>
    </login>
</authentication>";

            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "    ";

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);

            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "testcompany", "testpass");
            loginAuth.WriteXml(ref xml);

            xml.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            Diff xmlDiff = DiffBuilder.Compare(expected).WithTest(reader.ReadToEnd())
                .WithDifferenceEvaluator(DifferenceEvaluators.Default)
                .Build();
            Assert.IsFalse(xmlDiff.HasDifferences(), xmlDiff.ToString());
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "Company ID is required and cannot be blank")]
        public void InvalidCompanyIdTest()
        {
            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "", "testpass");
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "User ID is required and cannot be blank")]
        public void InvalidUserIdTest()
        {
            LoginAuthentication loginAuth = new LoginAuthentication("", "testcompany", "testpass");
        }

        [TestMethod()]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "User Password is required and cannot be blank")]
        public void InvalidUserPasswordTest()
        {
            LoginAuthentication loginAuth = new LoginAuthentication("testuser", "testcompany", "");
        }

    }

}
