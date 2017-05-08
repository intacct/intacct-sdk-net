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
 
using System.Collections.Generic;

namespace Intacct.Sdk.Functions.Company
{

    abstract public class AbstractAttachments : AbstractFunction
    {

        public string AttachmentsId;
        
        public string AttachmentsName;
        
        public string AttachmentFolderName;
        
        public string Description;
        
        public List<IAttachment> Files = new List<IAttachment>();

        public AbstractAttachments(string controlId = null) : base(controlId)
        {
        }

    }
}