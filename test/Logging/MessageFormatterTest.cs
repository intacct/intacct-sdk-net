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
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using Intacct.Sdk.Functions;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Tests.Helpers;
using Intacct.Sdk.Logging;

namespace Intacct.Sdk.Tests.Logging
{
    [TestClass]
    public class MessageFormatterTest
    {

        [TestMethod]
        public void RequestAndResponseRemovalTest()
        {
            SdkConfig config = new SdkConfig()
            {
                ControlId = "unittest",
                SenderId = "testsenderid",
                SenderPassword = "pass123!",
                CompanyId = "testcompany",
                UserId = "testuser",
                UserPassword = "P@ssW0rd!123",
            };
            
            Content content = new Content()
            {
                new ApiSessionCreate(),
            };

            RequestBlock xmlRequest = new RequestBlock(config, content);

            string xmlResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
    <control>
        <status>success</status>
        <senderid>testsenderid</senderid>
        <controlid>testControl</controlid>
        <uniqueid>false</uniqueid>
        <dtdversion>3.0</dtdversion>
    </control>
    <operation>
        <authentication>
            <status>success</status>
            <userid>testuser</userid>
            <companyid>testcompany</companyid>
            <sessiontimestamp>2016-08-22T10:58:43-07:00</sessiontimestamp>
        </authentication>
        <result>
            <status>success</status>
            <function>get_list</function>
            <controlid>test1</controlid>
            <listtype start=""0"" end=""0"" total=""1"">vendor</listtype>
            <data>
                <vendor>
                    <recordno>4</recordno>
                    <vendorid>V0004</vendorid>
                    <name>Vendor 4</name>
                    <taxid>99-9999999</taxid>
                    <achenabled>true</achenabled>
                    <achaccountnumber>1111222233334444</achaccountnumber>
                    <achaccounttype>Checking Account</achaccounttype>
                </vendor>
            </data>
        </result>
        <result>
            <status>success</status>
            <function>readByQuery</function>
            <controlid>test2</controlid>
            <data listtype=""vendor"" count=""1"" totalcount=""1"" numremaining=""0"" resultId="""">
                <vendor>
                    <RECORDNO>4</RECORDNO>
                    <VENDORID>V0004</VENDORID>
                    <NAME>Vendor 4</NAME>
                    <TAXID>99-9999999</TAXID>
                    <ACHENABLED>true</ACHENABLED>
                    <ACHACCOUNTNUMBER>1111222233334444</ACHACCOUNTNUMBER>
                    <ACHACCOUNTTYPE>Checking Account</ACHACCOUNTTYPE>
                </vendor>
            </data>
        </result>
    </operation>
</response>";


            Stream requestStream = xmlRequest.WriteXml();
            requestStream.Position = 0;
            
            HttpRequestMessage mockRequest = new HttpRequestMessage(HttpMethod.Post, "https://unittest.intacct.com")
            {
                Content = new StreamContent(requestStream),
            };
            HttpResponseMessage mockResponse = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(xmlResponse)
            };

            MessageFormatter formatter = new MessageFormatter();
            string message = formatter.Format(mockRequest, mockResponse);

            Assert.IsFalse(message.Contains("<password>pass123!</password>"));
            Assert.IsFalse(message.Contains("<password>P@ssW0rd!123</password>"));
            Assert.IsTrue(message.Contains("<password>REDACTED</password>"));

            Assert.IsFalse(message.Contains("<taxid>99-9999999</taxid>"));
            Assert.IsTrue(message.Contains("<taxid>REDACTED</taxid>"));

            Assert.IsFalse(message.Contains("<TAXID>99-9999999</TAXID>"));
            Assert.IsTrue(message.Contains("<TAXID>REDACTED</TAXID>"));

            Assert.IsFalse(message.Contains("<achaccountnumber>1111222233334444</achaccountnumber>"));
            Assert.IsTrue(message.Contains("<achaccountnumber>REDACTED</achaccountnumber>"));

            Assert.IsFalse(message.Contains("<ACHACCOUNTNUMBER>1111222233334444</ACHACCOUNTNUMBER>"));
            Assert.IsTrue(message.Contains("<ACHACCOUNTNUMBER>REDACTED</ACHACCOUNTNUMBER>"));
        }

    }
}
