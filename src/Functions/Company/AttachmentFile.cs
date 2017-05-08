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
using System;
using System.IO;
using System.IO.Abstractions;

namespace Intacct.Sdk.Functions.Company
{

    public class AttachmentFile : IAttachment
    {

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (String.IsNullOrWhiteSpace(this.FileName))
                {
                    FileName = Path.GetFileNameWithoutExtension(value);
                }

                if (String.IsNullOrWhiteSpace(this.Extension))
                {
                    var ext = Path.GetExtension(value);
                    if (ext.StartsWith("."))
                    {
                        ext = ext.Substring(1);
                    }
                    Extension = ext;
                }

                filePath = value;
            }
        }

        public string Extension;

        public string FileName;

        readonly IFileSystem fileSystem;

        /// <summary>
        /// Constructs the AttachmentFile object
        /// </summary>
        public AttachmentFile() : this(new FileSystem()) //use default implementation which calls System.IO
        {
        }

        /// <summary>
        /// Constructs the AttachmentFile object based on the fileSystem param for unit testing.
        /// </summary>
        /// <param name="fileSystem"></param>
        public AttachmentFile(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("attachment");

            // The file name without a period or extension - Ex: Invoice21244
            // Needs to be unique from other files in attachment record
            xml.WriteElement("attachmentname", FileName, true);

            // The file extension without a period - Ex: pdf
            xml.WriteElement("attachmenttype", Extension, true);
            
            Byte[] bytes = fileSystem.File.ReadAllBytes(FilePath);
            xml.WriteElement("attachmentdata", Convert.ToBase64String(bytes), true);

            xml.WriteEndElement(); //attachment
            
        }

    }

}