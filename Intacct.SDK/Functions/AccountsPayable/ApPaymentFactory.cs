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

namespace Intacct.SDK.Functions.AccountsPayable
{
    public class ApPaymentFactory
    {
        public static AbstractApPaymentFunction Create(string type, int recordno, string controlId)
        {
            AbstractApPaymentFunction apPaymentFunction;

            switch (type)
            {
                case AbstractApPaymentFunction.Delete:
                    apPaymentFunction = new ApPaymentDelete(recordno, controlId);
                    break;
                case AbstractApPaymentFunction.Decline:
                    apPaymentFunction = new ApPaymentDecline(recordno, controlId);
                    break;
                case AbstractApPaymentFunction.Confirm:
                    apPaymentFunction = new ApPaymentConfirm(recordno, controlId);
                    break;
                case AbstractApPaymentFunction.Approve:
                    apPaymentFunction = new ApPaymentApprove(recordno, controlId);
                    break;
                case AbstractApPaymentFunction.Send:
                    apPaymentFunction = new ApPaymentSend(recordno, controlId);
                    break;
                case AbstractApPaymentFunction.Void:
                    apPaymentFunction = new ApPaymentVoid(recordno, controlId);
                    break;
                default:
                    throw new ArgumentException("Cannot generate" + type + ".");
            }

            return apPaymentFunction;
        }
    }
}