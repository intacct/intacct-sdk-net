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

namespace Intacct.SDK.Functions.InventoryControl
{
    public class ItemUpdate : AbstractItem
    {
        
        public ItemUpdate(string controlId = null) : base(controlId)
        {
        }

        public override void WriteXml(ref IaXmlWriter xml)
        {
            xml.WriteStartElement("function");
            xml.WriteAttribute("controlid", ControlId, true);

            xml.WriteStartElement("update");
            xml.WriteStartElement("ITEM");

            xml.WriteElement("ITEMID", ItemId, true);

            xml.WriteElement("NAME", ItemName);
            
            if (Active == true)
            {
                xml.WriteElement("STATUS", "active");
            }
            else if (Active == false)
            {
                xml.WriteElement("STATUS", "inactive");
            }

            xml.WriteElement("PRODUCTLINEID", ProductLineId);
            xml.WriteElement("COST_METHOD", CostMethod);
            xml.WriteElement("EXTENDED_DESCRIPTION", ExtendedDescription);
            xml.WriteElement("PODESCRIPTION", PurchasingDescription);
            xml.WriteElement("SODESCRIPTION", SalesDescription);
            xml.WriteElement("UOMGRP", UnitOfMeasure);
            xml.WriteElement("NOTE", Note);
            xml.WriteElement("SHIP_WEIGHT", ShippingWeight);
            xml.WriteElement("GLGROUP", ItemGlGroupName);
            xml.WriteElement("STANDARD_COST", StandardCost);
            xml.WriteElement("BASEPRICE", BasePrice);
            xml.WriteElement("TAXABLE", Taxable);
            xml.WriteElement("TAXGROUP", ItemTaxGroupName);
            xml.WriteElement("DEFAULTREVRECTEMPLKEY", DefaultRevRecTemplateId);
            xml.WriteElement("INCOMEACCTKEY", RevenueGlAccountNo);
            xml.WriteElement("INVACCTKEY", InventoryGlAccountNo);
            xml.WriteElement("EXPENSEACCTKEY", ExpenseGlAccountNo);
            xml.WriteElement("COGSACCTKEY", CogsGlAccountNo);
            xml.WriteElement("OFFSETOEGLACCOUNTKEY", ArGlAccountNo);
            xml.WriteElement("OFFSETPOGLACCOUNTKEY", ApGlAccountNo);
            xml.WriteElement("DEFERREDREVACCTKEY", DeferredRevGlAccountNo);
            xml.WriteElement("VSOECATEGORY", VsoeCategory);
            xml.WriteElement("VSOEDLVRSTATUS", VsoeDefaultDeliveryStatus);
            xml.WriteElement("VSOEREVDEFSTATUS", VsoeDefaultDeferralStatus);
            xml.WriteElement("REVPOSTING", KitRevenuePosting);
            xml.WriteElement("REVPRINTING", KitPrintFormat);
            xml.WriteElement("SUBSTITUTEID", SubstituteItemId);
            xml.WriteElement("ENABLE_SERIALNO", SerialTrackingEnabled);
            xml.WriteElement("SERIAL_MASKKEY", SerialNumberMask);
            xml.WriteElement("ENABLE_LOT_CATEGORY", LotTrackingEnabled);
            xml.WriteElement("LOT_CATEGORYKEY", LotCategory);
            xml.WriteElement("ENABLE_BINS", BinTrackingEnabled);
            xml.WriteElement("ENABLE_EXPIRATION", ExpTrackingEnabled);
            xml.WriteElement("UPC", Upc);
            xml.WriteElement("INV_PRECISION", UnitCostPrecisionInventory);
            xml.WriteElement("SO_PRECISION", UnitCostPrecisionSales);
            xml.WriteElement("PO_PRECISION", UnitCostPrecisionPurchasing);
            xml.WriteElement("HASSTARTENDDATES", ItemStartEndDateEnabled);
            xml.WriteElement("TERMPERIOD", PeriodsMeasuredIn);
            xml.WriteElement("TOTALPERIODS", NumberOfPeriods);
            xml.WriteElement("COMPUTEFORSHORTTERM", ProratePriceAllowed);
            xml.WriteElement("RENEWALMACROID", DefaultRenewalMacroId);

            xml.WriteCustomFieldsImplicit(CustomFields);

            xml.WriteEndElement(); //ITEM
            xml.WriteEndElement(); //update

            xml.WriteEndElement(); //function
        }

    }
}