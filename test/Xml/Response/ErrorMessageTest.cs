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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Intacct.Sdk.Xml.Response;
using Intacct.Sdk.Tests.Helpers;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using Org.XmlUnit.Diff;
using Org.XmlUnit.Builder;

namespace Intacct.Sdk.Tests.Xml.Response
{

    [TestClass]
    public class ErrorMessageTest
    {

        [TestMethod()]
        public void GetErrorsTest()
        {
            string errorString = @"<?xml version=""1.0"" encoding=""utf-8""?>
<errormessage>
    <error>
          <errorno>1234</errorno>
          <description>description</description>
          <description2>Object definition &#39;BADOBJECT&#39; not found.</description2>
          <correction>strip&lt;out&gt;these&lt;/out&gt;tags.</correction>
    </error>
    <error>
          <errorno>5678</errorno>
          <description>strip&lt;out&gt;these&lt;/out&gt;tags.</description>
          <description2>Object definition &#39;BADOBJECT&#39; not found.</description2>
          <correction>correct.</correction>
    </error>
</errormessage>";

            Stream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(errorString);
            streamWriter.Flush();

            stream.Position = 0;

            XDocument xml = XDocument.Load(stream);
            
            ErrorMessage errorMessage = new ErrorMessage(xml.Element("errormessage").Elements("error"));
            
            Assert.IsInstanceOfType(errorMessage.Errors, typeof(List<string>));

            Assert.AreEqual("1234 description Object definition 'BADOBJECT' not found. stripthesetags.", errorMessage.Errors[0]);
            Assert.AreEqual("5678 stripthesetags. Object definition 'BADOBJECT' not found. correct.", errorMessage.Errors[1]);
        }

    }

}
