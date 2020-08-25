/*
 * Copyright 2020 Sage Intacct, Inc.
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
using Intacct.SDK.Xml.Request;
using Xunit;

namespace Intacct.SDK.Tests.Xml.Request
{
    public class SessionAuthenticationTest : XmlObjectTestHelper
    {
        [Fact]
        public void WriteXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<authentication>
    <sessionid>testsessionid..</sessionid>
</authentication>";

            SessionAuthentication sessionAuth = new SessionAuthentication("testsessionid..");
            
            this.CompareXml(expected, sessionAuth);
        }

        [Fact]
        public void InvalidSessionIdTest()
        {
            var ex = Record.Exception(() => new SessionAuthentication(""));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal("Session ID is required and cannot be blank", ex.Message);
        }
    }
}