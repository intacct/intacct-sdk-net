/*
 * Copyright 2022 Sage Intacct, Inc.
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
using System.IO;
using Intacct.SDK.Xml;
using Microsoft.Extensions.FileProviders;

namespace Intacct.SDK.Functions.Company
{
    public class AttachmentFile : IAttachment
    {

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrWhiteSpace(this.FileName))
                {
                    FileName = Path.GetFileNameWithoutExtension(value);
                }

                if (string.IsNullOrWhiteSpace(this.Extension))
                {
                    var ext = Path.GetExtension(value);
                    if (ext.StartsWith("."))
                    {
                        ext = ext.Substring(1);
                    }
                    Extension = ext;
                }

                _filePath = value;
            }
        }

        public string Extension;

        public string FileName;

        public AttachmentFile()
        {
        }

        public void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("attachment");

            // The file name without a period or extension - Ex: Invoice21244
            // Needs to be unique from other files in attachment record
            xml.WriteElement("attachmentname", FileName, true);

            // The file extension without a period - Ex: pdf
            xml.WriteElement("attachmenttype", Extension, true);

            PhysicalFileProvider fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(FilePath));
            IFileInfo fileInfo = fileProvider.GetFileInfo(Path.GetFileName(FilePath));
            if (fileInfo.Exists)
            {
                using (Stream fileStream = fileInfo.CreateReadStream())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        xml.WriteElement("attachmentdata", Convert.ToBase64String(memoryStream.ToArray()), true);
                    }
                }
            }

            xml.WriteEndElement(); //attachment
            
        }

    }
}