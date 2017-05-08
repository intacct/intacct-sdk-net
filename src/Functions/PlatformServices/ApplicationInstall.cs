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

using Intacct.Sdk.Xml;
using System.IO.Abstractions;

namespace Intacct.Sdk.Functions.PlatformServices
{

    public class ApplicationInstall : AbstractFunction
    {
        public string XmlFilename;

        readonly IFileSystem fileSystem;

        /// <summary>
        /// Constructs the ApplicationInstall object
        /// </summary>
        /// <param name="controlId"></param>
        public ApplicationInstall(string controlId = null) : this(
                        new FileSystem(), //use default implementation which calls System.IO
                        controlId)
        {
        }

        /// <summary>
        /// Constructs the ApplicationInstall object based on the fileSystem param for unit testing.
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="controlId"></param>
        public ApplicationInstall(IFileSystem fileSystem, string controlId = null) : base(controlId)
        {
            this.fileSystem = fileSystem;
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("installApp");
            xml.WriteStartElement("appxml");

            if (fileSystem.File.Exists(XmlFilename))
            {
                string fileContents = fileSystem.File.ReadAllText(XmlFilename);

                xml.WriteCData(fileContents);
            }
            
            xml.WriteEndElement(); //appxml
            xml.WriteEndElement(); //installApp

            xml.WriteEndElement(); //function
        }
    }
}