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

using Intacct.SDK.Functions.Common.NewQuery.QueryFilter;
using Intacct.SDK.Functions.Common.NewQuery.QueryOrderBy;
using Intacct.SDK.Functions.Common.NewQuery.QuerySelect;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.Common.NewQuery
{
    public interface IQueryFunction : IFunction
    {
        
        ISelect[] SelectFields { get; set; }
        
        string FromObject { get; set; }

        string DocParId { get; set; }
        
        IFilter Filter { get; set; }
        
        IOrder[] OrderBy { get; set; }

        bool? CaseInsensitive { get; set; }
        
        int? PageSize { get; set; }
        
        int? Offset { get; set; }
        
        new void WriteXml(ref IaXmlWriter xml);
    }
}