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

using System.Collections.Generic;

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentDetailBuilder
    {
        private readonly List<IApPaymentDetail> _detailList;

        public ApPaymentDetailBuilder()
        {
            _detailList = new List<IApPaymentDetail>();
        }

        public ApPaymentDetailBuilder AddApPaymentDetailBill(ApPaymentDetailBillInfo info)
        {
            _detailList.Add(new ApPaymentDetailBill(info));
            return this;
        }

        public ApPaymentDetailBuilder AddApPaymentDetailCreditMemo(ApPaymentDetailInfo info)
        {
            _detailList.Add(new ApPaymentDetailCreditMemo(info));
            return this;
        }

        public List<IApPaymentDetail> GetApPaymentDetailList()
        {
            return _detailList;
        }
    }
}