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

using Intacct.Sdk.Xml.Request;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Intacct.Sdk.Xml
{

    public class RequestBlock
    {
        
        /// <summary>
        /// The Request's XML encoding
        /// </summary>
        protected Encoding encoding;

        /// <summary>
        /// Gets the ControlBlock of the request
        /// <seealso cref="ControlBlock"/>
        /// </summary>
        protected ControlBlock control;

        /// <summary>
        /// Gets the OperationBlock of the request
        /// <seealso cref="OperationBlock"/>
        /// </summary>
        protected OperationBlock operation;

        /// <summary>
        /// Constructs the RequestBlock object with the supplied config and content
        /// </summary>
        /// <param name="config"></param>
        /// <param name="content"></param>
        public RequestBlock(SdkConfig config, Content content)
        {
            if (!String.IsNullOrWhiteSpace(config.Encoding))
            {
                var encodingInfo = Encoding.GetEncodings().FirstOrDefault(info => info.Name == config.Encoding);
                if (encodingInfo != null)
                {
                    Encoding encoding = encodingInfo.GetEncoding();
                }
                else
                {
                    throw new ArgumentException("Requested encoding is not supported");
                }
            }
            else
            {
                encoding = Encoding.GetEncoding("UTF-8");
            }
            
            control = new ControlBlock(config);
            operation = new OperationBlock(config, content);
        }

        /// <summary>
        /// Returns a Stream of the XML request
        /// </summary>
        public Stream WriteXml()
        {
            Stream stream = new MemoryStream();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = encoding;

            IaXmlWriter xml = new IaXmlWriter(stream, xmlSettings);
            xml.WriteStartElement("request");

            control.WriteXml(ref xml); // Create control block

            operation.WriteXml(ref xml); // Create operation block

            xml.WriteEndElement(); // request

            xml.Flush();

            return stream;
        }
        
    }
}