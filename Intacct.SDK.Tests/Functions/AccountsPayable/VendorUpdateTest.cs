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
using Intacct.SDK.Functions.AccountsPayable;
using Intacct.SDK.Functions.Company;
using Intacct.SDK.Tests.Xml;
using Xunit;

namespace Intacct.SDK.Tests.Functions.AccountsPayable
{
    public class VendorUpdateTest : XmlObjectTestHelper
    {
        [Fact]
        public void GetXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update>
        <VENDOR>
            <VENDORID>V1234</VENDORID>
            <DISPLAYCONTACT />
        </VENDOR>
    </update>
</function>";

            VendorUpdate record = new VendorUpdate("unittest")
            {
                VendorId = "V1234",
            };
            this.CompareXml(expected, record);
        }
        
        [Fact]
        public void GetAllXmlTest()
        {
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<function controlid=""unittest"">
    <update>
        <VENDOR>
            <VENDORID>V1234</VENDORID>
            <NAME>Intacct Corp</NAME>
            <DISPLAYCONTACT>
                <PRINTAS>Intacct Corporation</PRINTAS>
                <COMPANYNAME>Intacct Corp.</COMPANYNAME>
                <TAXABLE>true</TAXABLE>
                <TAXGROUP>CA</TAXGROUP>
                <PREFIX>Mr</PREFIX>
                <FIRSTNAME>Bill</FIRSTNAME>
                <LASTNAME>Smith</LASTNAME>
                <INITIAL>G</INITIAL>
                <PHONE1>12</PHONE1>
                <PHONE2>34</PHONE2>
                <CELLPHONE>56</CELLPHONE>
                <PAGER>78</PAGER>
                <FAX>90</FAX>
                <EMAIL1>noreply@intacct.com</EMAIL1>
                <EMAIL2>noreplyagain@intacct.com</EMAIL2>
                <URL1>www.intacct.com</URL1>
                <URL2>us.intacct.com</URL2>
                <MAILADDRESS>
                    <ADDRESS1>300 Park Ave</ADDRESS1>
                    <ADDRESS2>Ste 1400</ADDRESS2>
                    <CITY>San Jose</CITY>
                    <STATE>CA</STATE>
                    <ZIP>95110</ZIP>
                    <COUNTRY>United States</COUNTRY>
                    <ISOCOUNTRYCODE>USA</ISOCOUNTRYCODE>
                </MAILADDRESS>
            </DISPLAYCONTACT>
            <ONETIME>false</ONETIME>
            <STATUS>active</STATUS>
            <HIDEDISPLAYCONTACT>false</HIDEDISPLAYCONTACT>
            <VENDTYPE>SaaS</VENDTYPE>
            <PARENTID>V5678</PARENTID>
            <GLGROUP>Group</GLGROUP>
            <TAXID>12-3456789</TAXID>
            <NAME1099>INTACCT CORP</NAME1099>
            <FORM1099TYPE>MISC</FORM1099TYPE>
            <FORM1099BOX>3</FORM1099BOX>
            <SUPDOCID>A1234</SUPDOCID>
            <APACCOUNT>2000</APACCOUNT>
            <CREDITLIMIT>1234.56</CREDITLIMIT>
            <ONHOLD>false</ONHOLD>
            <DONOTCUTCHECK>false</DONOTCUTCHECK>
            <COMMENTS>my comment</COMMENTS>
            <CURRENCY>USD</CURRENCY>
            <CONTACTINFO>
                <CONTACTNAME>primary</CONTACTNAME>
            </CONTACTINFO>
            <PAYTO>
                <CONTACTNAME>pay to</CONTACTNAME>
            </PAYTO>
            <RETURNTO>
                <CONTACTNAME>return to</CONTACTNAME>
            </RETURNTO>
            <CONTACT_LIST_INFO>
                <CATEGORYNAME>Primary Contact</CATEGORYNAME>
                <CONTACT>
                    <NAME>primary</NAME>
                </CONTACT>
            </CONTACT_LIST_INFO>
            <CONTACT_LIST_INFO>
                <CATEGORYNAME>Billing Contact</CATEGORYNAME>
                <CONTACT>
                    <NAME>bill to</NAME>
                </CONTACT>
            </CONTACT_LIST_INFO>
            <CONTACT_LIST_INFO>
                <CATEGORYNAME>Shipping Contact</CATEGORYNAME>
                <CONTACT>
                    <NAME>ship to</NAME>
                </CONTACT>
            </CONTACT_LIST_INFO>
            <PAYMETHODKEY>Printed Check</PAYMETHODKEY>
            <MERGEPAYMENTREQ>true</MERGEPAYMENTREQ>
            <PAYMENTNOTIFY>true</PAYMENTNOTIFY>
            <BILLINGTYPE>openitem</BILLINGTYPE>
            <PAYMENTPRIORITY>Normal</PAYMENTPRIORITY>
            <TERMNAME>N30</TERMNAME>
            <DISPLAYTERMDISCOUNT>false</DISPLAYTERMDISCOUNT>
            <ACHENABLED>true</ACHENABLED>
            <ACHBANKROUTINGNUMBER>123456789</ACHBANKROUTINGNUMBER>
            <ACHACCOUNTNUMBER>1111222233334444</ACHACCOUNTNUMBER>
            <ACHACCOUNTTYPE>Checking Account</ACHACCOUNTTYPE>
            <ACHREMITTANCETYPE>CTX</ACHREMITTANCETYPE>
            <VENDORACCOUNTNO>9999999</VENDORACCOUNTNO>
            <DISPLAYACCTNOCHECK>false</DISPLAYACCTNOCHECK>
            <OBJECTRESTRICTION>Restricted</OBJECTRESTRICTION>
            <RESTRICTEDLOCATIONS>100#~#200</RESTRICTEDLOCATIONS>
            <RESTRICTEDDEPARTMENTS>D100#~#D200</RESTRICTEDDEPARTMENTS>
            <CUSTOMFIELD1>Hello</CUSTOMFIELD1>
        </VENDOR>
    </update>
</function>";

            VendorUpdate record = new VendorUpdate("unittest") {
                VendorId = "V1234",
                VendorName = "Intacct Corp",
                PrintAs = "Intacct Corporation",
                CompanyName = "Intacct Corp.",
                Taxable = true,
                TaxId = "12-3456789",
                ContactTaxGroupName = "CA",
                Prefix = "Mr",
                FirstName = "Bill",
                LastName = "Smith",
                MiddleName = "G",
                PrimaryPhoneNo = "12",
                SecondaryPhoneNo = "34",
                CellularPhoneNo = "56",
                PagerNo = "78",
                FaxNo = "90",
                PrimaryEmailAddress = "noreply@intacct.com",
                SecondaryEmailAddress = "noreplyagain@intacct.com",
                PrimaryUrl = "www.intacct.com",
                SecondaryUrl = "us.intacct.com",
                AddressLine1 = "300 Park Ave",
                AddressLine2 = "Ste 1400",
                City = "San Jose",
                StateProvince = "CA",
                ZipPostalCode = "95110",
                Country = "United States",
                IsoCountryCode = "USA",
                OneTime = false,
                Active = true,
                ExcludedFromContactList = false,
                VendorTypeId = "SaaS",
                ParentVendorId = "V5678",
                GlGroupName = "Group",
                Form1099Name = "INTACCT CORP",
                Form1099Type = "MISC",
                Form1099Box = "3",
                AttachmentsId = "A1234",
                DefaultExpenseGlAccountNo = "2000",
                CreditLimit = 1234.56M,
                OnHold = false,
                DoNotPay = false,
                Comments = "my comment",
                DefaultCurrency = "USD",
                PrimaryContactName = "primary",
                PayToContactName = "pay to",
                ReturnToContactName = "return to",
                ContactList = new List<ContactListInfo>
                {
                    new ContactListInfo { CategoryName = "Primary Contact", Contact = "primary" },
                    new ContactListInfo { CategoryName = "Billing Contact", Contact = "bill to" },
                    new ContactListInfo { CategoryName = "Shipping Contact", Contact = "ship to" },
                },
                PreferredPaymentMethod = "Printed Check",
                MergePaymentRequests = true,
                SendAutomaticPaymentNotification = true,
                VendorBillingType = "openitem",
                PaymentPriority = "Normal",
                PaymentTerm = "N30",
                TermDiscountDisplayedOnCheckStub = false,
                AchEnabled = true,
                AchBankRoutingNo = "123456789",
                AchBankAccountNo = "1111222233334444",
                AchBankAccountType = "Checking Account",
                AchBankAccountClass = "CTX",
                VendorAccountNo = "9999999",
                LocationAssignedAccountNoDisplayedOnCheckStub = false,
                RestrictionType = "Restricted",
                RestrictedLocations = new List<string>()
                {
                    "100",
                    "200",
                },
                RestrictedDepartments = new List<string>()
                {
                    "D100",
                    "D200",
                },
                CustomFields = new Dictionary<string, dynamic>
                {
                    { "CUSTOMFIELD1", "Hello" }
                }
            };
            this.CompareXml(expected, record);
        }
    }
}