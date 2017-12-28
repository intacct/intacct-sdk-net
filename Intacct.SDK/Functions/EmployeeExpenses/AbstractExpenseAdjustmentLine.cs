using System;
using System.Collections.Generic;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Functions.EmployeeExpenses
{
    public abstract class AbstractExpenseAdjustmentLine : IXmlObject
    {

        public string ExpenseType;

        public string GlAccountNumber;

        public decimal? ReimbursementAmount;

        public string PaymentTypeName;

        public bool? Form1099;

        public string Memo;

        public DateTime? ExpenseDate;

        public decimal? Quantity;

        public decimal? UnitRate;

        public string TransactionCurrency;

        public decimal? TransactionAmount;

        public DateTime? ExchangeRateDate;

        public decimal? ExchangeRateValue;

        public string ExchangeRateType;

        public bool? Billable;

        public string DepartmentId;

        public string LocationId;

        public string ProjectId;

        public string CustomerId;

        public string VendorId;

        public string EmployeeId;

        public string ItemId;

        public string ClassId;

        public string ContractId;

        public string WarehouseId;

        public Dictionary<string, dynamic> CustomFields = new Dictionary<string, dynamic>();

        protected AbstractExpenseAdjustmentLine()
        {
        }
        
        public abstract void WriteXml(ref IaXmlWriter xml);
        
    }
}