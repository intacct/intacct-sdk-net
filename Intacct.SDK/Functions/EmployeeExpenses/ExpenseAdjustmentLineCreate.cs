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

using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.EmployeeExpenses
{
    public class ExpenseAdjustmentLineCreate : AbstractExpenseAdjustmentLine
    {

        public ExpenseAdjustmentLineCreate()
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("expenseadjustment");
            
            if (!string.IsNullOrWhiteSpace(ExpenseType))
            {
                xml.WriteElement("expensetype", ExpenseType, true);
            } else
            {
                xml.WriteElement("glaccountno", GlAccountNumber, true);
            }

            xml.WriteElement("amount", ReimbursementAmount, true);
            xml.WriteElement("currency", TransactionCurrency);
            xml.WriteElement("trx_amount", TransactionAmount);

            if (ExchangeRateDate.HasValue)
            {
                xml.WriteStartElement("exchratedate");
                xml.WriteDateSplitElements(ExchangeRateDate.Value, true);
                xml.WriteEndElement(); //exchratedate
            }

            if (!string.IsNullOrWhiteSpace(ExchangeRateType))
            {
                xml.WriteElement("exchratetype", ExchangeRateType);
            } else if (ExchangeRateValue.HasValue)
            {
                xml.WriteElement("exchrate", ExchangeRateValue.Value);
            } else if(!string.IsNullOrWhiteSpace(TransactionCurrency) || TransactionAmount.HasValue)
            {
                xml.WriteElement("exchratetype", ExchangeRateType, true);
            }

            if (ExpenseDate.HasValue)
            {
                xml.WriteStartElement("expensedate");
                xml.WriteDateSplitElements(ExpenseDate.Value, true);
                xml.WriteEndElement();  //expensedate
            }

            xml.WriteElement("memo", Memo);
            xml.WriteElement("locationid", LocationId);
            xml.WriteElement("departmentid", DepartmentId);
            xml.WriteElement("projectid", ProjectId);
            xml.WriteElement("customerid", CustomerId);
            xml.WriteElement("vendorid", VendorId);
            xml.WriteElement("employeeid", EmployeeId);
            xml.WriteElement("itemid", ItemId);
            xml.WriteElement("classid", ClassId);
            xml.WriteElement("contractid", ContractId);
            xml.WriteElement("warehouseid", WarehouseId);
            xml.WriteElement("billable", Billable);
            xml.WriteElement("exppmttype", PaymentTypeName);
            xml.WriteElement("quantity", Quantity);
            xml.WriteElement("rate", UnitRate);

            xml.WriteCustomFieldsExplicit(CustomFields);

            xml.WriteEndElement(); //expenseadjustment
        }

    }
}