/*
 * Copyright 2020 Sage Intacct, Inc.
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
using Intacct.SDK.Functions.AccountsPayable;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsPayable
{
    public class ApPaymentCreateTest : XmlObjectTestHelper
    {
        [Fact]
        public void CreateCheckForBillGetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>123</RECORDKEY>
                    <TRX_PAYMENTAMOUNT>100.12</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 123,
                PaymentAmount = 100.12M,
            };

            record.ApPaymentDetails.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void CreateXferForBillLineGetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>EFT</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <DOCNUMBER>12345</DOCNUMBER>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>123</RECORDKEY>
                    <ENTRYKEY>456</ENTRYKEY>
                    <TRX_PAYMENTAMOUNT>100.12</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "EFT",
                PaymentDate = new DateTime(2015, 06, 30),
                DocumentNo = "12345",
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 123,
                BillLineRecordNo = 456,
                PaymentAmount = 100.12M,
            };

            record.ApPaymentDetails.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        
        
        [Fact]
        public void CreateCheckForBillDiscountGetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>123</RECORDKEY>
                    <DISCOUNTDATE>06/29/2015</DISCOUNTDATE>
                    <TRX_PAYMENTAMOUNT>294.00</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 123,
                PaymentAmount = 294.00M,
                ApplyToDiscountDate = new DateTime(2015, 06, 29),
            };

            record.ApPaymentDetails.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void CreateForCreditMemoAndUseDebitXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <POSADJKEY>2595</POSADJKEY>
                    <POSADJENTRYKEY>42962</POSADJENTRYKEY>
                    <TRX_PAYMENTAMOUNT>1.00</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
                <APPYMTDETAIL>
                    <POSADJKEY>2595</POSADJKEY>
                    <POSADJENTRYKEY>42962</POSADJENTRYKEY>
                    <ADJUSTMENTKEY>2590</ADJUSTMENTKEY>
                    <ADJUSTMENTENTRYKEY>42949</ADJUSTMENTENTRYKEY>
                    <TRX_ADJUSTMENTAMOUNT>1.01</TRX_ADJUSTMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentDetailForAdjustment line1 = new ApPaymentDetailForAdjustment()
            {
                CreditMemoRecordNo = 2595,
                CreditMemoLineRecordNo = 42962,
                PaymentAmount = 1.00M,
            };

            record.ApPaymentDetails.Add(line1);
            
            ApPaymentDetailForAdjustment line2 = new ApPaymentDetailForAdjustment()
            {
                CreditMemoRecordNo = 2595,
                CreditMemoLineRecordNo = 42962,
            };
            ApPaymentUseExistingDebitMemo useExisting2 = new ApPaymentUseExistingDebitMemo()
            {
                ExistingRecordId = 2590,
                ExistingRecordLineId = 42949,
                ExistingAmount = 1.01M,
            };
            line2.UseExistingTransaction = useExisting2;

            record.ApPaymentDetails.Add(line2);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void CreateForBillAndUseAllTheThingsXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>30</RECORDKEY>
                    <ENTRYKEY>60</ENTRYKEY>
                    <TRX_PAYMENTAMOUNT>1.00</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
                <APPYMTDETAIL>
                    <RECORDKEY>30</RECORDKEY>
                    <ENTRYKEY>60</ENTRYKEY>
                    <ADVANCEKEY>2584</ADVANCEKEY>
                    <ADVANCEENTRYKEY>42931</ADVANCEENTRYKEY>
                    <TRX_POSTEDADVANCEAMOUNT>2.49</TRX_POSTEDADVANCEAMOUNT>
                </APPYMTDETAIL>
                <APPYMTDETAIL>
                    <RECORDKEY>30</RECORDKEY>
                    <ENTRYKEY>60</ENTRYKEY>
                    <ADJUSTMENTKEY>2590</ADJUSTMENTKEY>
                    <ADJUSTMENTENTRYKEY>42949</ADJUSTMENTENTRYKEY>
                    <TRX_ADJUSTMENTAMOUNT>2.01</TRX_ADJUSTMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 30,
                BillLineRecordNo = 60,
                PaymentAmount = 1.00M,
            };
            record.ApPaymentDetails.Add(line1);
            
            ApPaymentDetailForBill line2 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 30,
                BillLineRecordNo = 60,
            };
            IApPaymentUseExistingTransaction useExisting2 = new ApPaymentUseExistingAdvance()
            {
                ExistingRecordId = 2584,
                ExistingRecordLineId = 42931,
                ExistingAmount = 2.49M,
            };
            line2.UseExistingTransaction = useExisting2;
            record.ApPaymentDetails.Add(line2);
            
            ApPaymentDetailForBill line3 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 30,
                BillLineRecordNo = 60,
            };
            ApPaymentUseExistingDebitMemo useExisting3 = new ApPaymentUseExistingDebitMemo()
            {
                ExistingRecordId = 2590,
                ExistingRecordLineId = 42949,
                ExistingAmount = 2.01M,
            };
            line3.UseExistingTransaction = useExisting3;
            record.ApPaymentDetails.Add(line3);

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void CreateForBillAndUseNegativeBillXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Cash</PAYMENTMETHOD>
            <FINANCIALENTITY>BOA</FINANCIALENTITY>
            <VENDORID>a4</VENDORID>
            <PAYMENTDATE>10/06/2020</PAYMENTDATE>
            <CURRENCY>USD</CURRENCY>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>3693</RECORDKEY>
                    <INLINEKEY>3694</INLINEKEY>
                    <TRX_INLINEAMOUNT>70</TRX_INLINEAMOUNT>
                    <TRX_PAYMENTAMOUNT>8.8</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BOA",
                VendorId = "a4",
                PaymentMethod = "Cash",
                PaymentDate = new DateTime(2020, 10, 06),
                TransactionCurrency = "USD"
            };

            ApPaymentDetailForBill line = new ApPaymentDetailForBill()
            {
                BillRecordNo = 3693,
                PaymentAmount = 8.8M,
            };
            
            IApPaymentUseExistingTransaction useExisting = new ApPaymentUseExistingNegativeBill()
            {
                ExistingRecordId = 3694,
                ExistingAmount = 70M,
            };
            line.UseExistingTransaction = useExisting;
            record.ApPaymentDetails.Add(line);

            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>123</RECORDKEY>
                    <TRX_PAYMENTAMOUNT>100.12</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                PaymentMethod = "Printed Check",
                PaymentDate = new DateTime(2015, 06, 30),
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 123,
                PaymentAmount = 100.12M,
            };

            record.ApPaymentDetails.Add(line1);
            
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <create>
        <APPYMT>
            <PAYMENTMETHOD>Printed Check</PAYMENTMETHOD>
            <FINANCIALENTITY>BA1143</FINANCIALENTITY>
            <VENDORID>V0001</VENDORID>
            <PAYMENTREQUESTMETHOD>vendorpref</PAYMENTREQUESTMETHOD>
            <GROUPPAYMENTS>true</GROUPPAYMENTS>
            <EXCH_RATE_DATE>06/30/2015</EXCH_RATE_DATE>
            <EXCH_RATE_TYPE_ID>Intacct Daily Rate</EXCH_RATE_TYPE_ID>
            <DOCNUMBER>10000</DOCNUMBER>
            <DESCRIPTION>Memo</DESCRIPTION>
            <PAYMENTCONTACT>Jim Smith</PAYMENTCONTACT>
            <PAYMENTDATE>06/30/2015</PAYMENTDATE>
            <APPYMTDETAILS>
                <APPYMTDETAIL>
                    <RECORDKEY>123</RECORDKEY>
                    <DISCOUNTTOAPPLY>1.21</DISCOUNTTOAPPLY>
                    <CREDITTOAPPLY>8.12</CREDITTOAPPLY>
                    <TRX_PAYMENTAMOUNT>100.12</TRX_PAYMENTAMOUNT>
                </APPYMTDETAIL>
            </APPYMTDETAILS>
        </APPYMT>
    </create>
</function>";

            ApPaymentCreate record = new ApPaymentCreate("unittest")
            {
                AccountId = "BA1143",
                VendorId = "V0001",
                Memo = "Memo",
                PaymentMethod = "Printed Check",
                GroupPayments = true,
                PaymentDate = new DateTime(2015, 06, 30),
                ExchangeRateDate = new DateTime(2015,06,30),
                ExchangeRateType = "Intacct Daily Rate",
                MergeOption = "vendorpref",
                DocumentNo = "10000",
                NotificationContactName = "Jim Smith",
            };

            ApPaymentDetailForBill line1 = new ApPaymentDetailForBill()
            {
                BillRecordNo = 123,
                PaymentAmount = 100.12M,
                CreditToApply = 8.12M,
                DiscountToApply = 1.21M,
            };

            record.ApPaymentDetails.Add(line1);
            
            this.CompareXml(expected, record);
        }
    }
}