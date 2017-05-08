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
 
namespace Intacct.Sdk.Functions.AccountsReceivable.PaymentServices
{

    abstract public class AbstractCustomerChargeCard : AbstractFunction
    {
        
        public static string CardTypeVisa = "Visa";
        
        public static string CardTypeMastercard = "Mastercard";
        
        public static string CardTypeDiscover = "Discover";
        
        public static string CardTypeAMEX = "American Express";
        
        public static string CardTypeDinersClub = "Diners Club";
        
        public static string CardTypeOther = "Other Charge Card";

        public int? RecordNo;

        public string CustomerId;
        
        public string CardNumber;
        
        public string CardType;
        
        public string ExpirationMonth;
        
        public string ExpirationYear;
        
        public bool? Active;
        
        public string Description;
        
        public bool? DefaultCard;
        
        public bool? BillToContactAddressUsedForVerification;
        
        public string AddressLine1;
        
        public string AddressLine2;
        
        public string City;
        
        public string StateProvince;
        
        public string ZipPostalCode;
        
        public string Country;

        public AbstractCustomerChargeCard(string controlId = null) : base(controlId)
        {
        }
    }
}