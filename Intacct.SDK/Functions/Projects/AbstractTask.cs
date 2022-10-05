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
using System.Collections.Generic;

namespace Intacct.SDK.Functions.Projects
{
    public abstract class AbstractTask : AbstractFunction
    {

        public int RecordNo;

        public string TaskName;

        public string ProjectId;
        
        public DateTime? PlannedBeginDate;

        public DateTime? PlannedEndDate;

        public string ClassId;

        public string ItemId;

        public bool? Billable;

        public string Description;

        public bool? Milestone;

        public bool? Utilized;

        public string Priority;

        public string WbsCode;

        public string TaskStatus;

        public string TimeType;

        public int? ParentTaskRecordNo;

        public string AttachmentsId;

        [ObsoleteAttribute("The ObservedPercentComplete property is deprecated in the API. Use TaskObservedPercentCompleted classes instead.", false)]
        public decimal? ObservedPercentCompleted;

        public decimal? PlannedDuration;

        public decimal? EstimatedDuration;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractTask(string controlId = null) : base(controlId)
        {
        }
        
    }
}