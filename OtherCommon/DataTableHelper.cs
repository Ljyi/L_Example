using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtherCommon
{
    public class DataTableHelper
    {
        public static DataTable InitBatchShopeeOrderMainTable()
        {
            var dtOrderMain = new DataTable();
            dtOrderMain.Columns.Add("AccountId", typeof(int));
            dtOrderMain.Columns.Add("AccountName", typeof(string));
            dtOrderMain.Columns.Add("SalesPlatformID", typeof(int));
            dtOrderMain.Columns.Add("SiteID", typeof(int));
            dtOrderMain.Columns.Add("OrderID", typeof(string));
            dtOrderMain.Columns.Add("TrackingNo", typeof(string));
            dtOrderMain.Columns.Add("UpdateTime", typeof(DateTime));
            dtOrderMain.Columns.Add("EstimatedShippingFee", typeof(decimal));
            dtOrderMain.Columns.Add("ActualShippingCost", typeof(decimal));
            dtOrderMain.Columns.Add("ShippingCarrier", typeof(string));
            dtOrderMain.Columns.Add("PaymentMethod", typeof(string));
            dtOrderMain.Columns.Add("Country", typeof(string));
            dtOrderMain.Columns.Add("EscrowAmount", typeof(decimal));
            dtOrderMain.Columns.Add("DaysToShip", typeof(int));
            dtOrderMain.Columns.Add("Currency", typeof(string));
            dtOrderMain.Columns.Add("CreateTime", typeof(DateTime));
            dtOrderMain.Columns.Add("Cod", typeof(bool));
            dtOrderMain.Columns.Add("OrderStatus", typeof(string));
            dtOrderMain.Columns.Add("MessageToSeller", typeof(string));
            dtOrderMain.Columns.Add("RecTown", typeof(string));
            dtOrderMain.Columns.Add("RecCity", typeof(string));
            dtOrderMain.Columns.Add("RecName", typeof(string));
            dtOrderMain.Columns.Add("RecDistrict", typeof(string));
            dtOrderMain.Columns.Add("RecCountry", typeof(string));
            dtOrderMain.Columns.Add("RecZipcode", typeof(string));
            dtOrderMain.Columns.Add("RecFullAddress", typeof(string));
            dtOrderMain.Columns.Add("RecPhone", typeof(string));
            dtOrderMain.Columns.Add("RecState", typeof(string));
            dtOrderMain.Columns.Add("GoodsToDeclare", typeof(bool));
            dtOrderMain.Columns.Add("TotalAmount", typeof(decimal));
            dtOrderMain.Columns.Add("CreateDate", typeof(DateTime));
            dtOrderMain.Columns.Add("CreateUser", typeof(string));
            dtOrderMain.Columns.Add("UpdateDate", typeof(DateTime));
            dtOrderMain.Columns.Add("UpdateUser", typeof(string));
            dtOrderMain.Columns.Add("SiteName", typeof(string));
            dtOrderMain.Columns.Add("ShopeeName", typeof(string));
            dtOrderMain.Columns.Add("LocalCurrency", typeof(string));
            dtOrderMain.Columns.Add("Coin", typeof(decimal));
            dtOrderMain.Columns.Add("Voucher", typeof(decimal));
            dtOrderMain.Columns.Add("VoucherSeller", typeof(decimal));
            dtOrderMain.Columns.Add("SellerRebate", typeof(decimal));
            dtOrderMain.Columns.Add("ShippingFeeRebate", typeof(decimal));
            dtOrderMain.Columns.Add("CommissionFee", typeof(decimal));
            dtOrderMain.Columns.Add("VoucherCode", typeof(string));
            dtOrderMain.Columns.Add("VoucherName", typeof(string));
            dtOrderMain.Columns.Add("CrossBorderTax", typeof(decimal));
            dtOrderMain.Columns.Add("CreditCardPromotion", typeof(decimal));
            dtOrderMain.Columns.Add("CreditCardTransactionFee", typeof(decimal));
            dtOrderMain.Columns.Add("EscrowCurrency", typeof(string));
            dtOrderMain.Columns.Add("ExchangeRate", typeof(decimal));
            dtOrderMain.Columns.Add("EscrowChannel", typeof(string));
            dtOrderMain.Columns.Add("PayeeId", typeof(string));
            dtOrderMain.Columns.Add("PayTime", typeof(long));
            dtOrderMain.Columns.Add("PayLocalDate", typeof(DateTime));
            return dtOrderMain;

        }

        public static DataTable InitBatchShopeeOrderDetailTable()
        {
            var dtOrderDetail = new DataTable();
            dtOrderDetail.Columns.Add("OrderID", typeof(string));
            dtOrderDetail.Columns.Add("ItemID", typeof(string));
            dtOrderDetail.Columns.Add("ItemName", typeof(string));
            dtOrderDetail.Columns.Add("ItemSku", typeof(string));
            dtOrderDetail.Columns.Add("VariationID", typeof(long));
            dtOrderDetail.Columns.Add("VariationName", typeof(string));
            dtOrderDetail.Columns.Add("VariationSku", typeof(string));
            dtOrderDetail.Columns.Add("VariationQuantityPurchased", typeof(int));
            dtOrderDetail.Columns.Add("VariationOriginalPrice", typeof(decimal));
            dtOrderDetail.Columns.Add("VariationDiscountedPrice", typeof(decimal));
            dtOrderDetail.Columns.Add("UniqueKey", typeof(string));
            dtOrderDetail.Columns.Add("isUpToTemp", typeof(bool));
            dtOrderDetail.Columns.Add("CreateDate", typeof(DateTime));
            dtOrderDetail.Columns.Add("CreateUser", typeof(string));
            dtOrderDetail.Columns.Add("UpdateDate", typeof(DateTime));
            dtOrderDetail.Columns.Add("UpdateUser", typeof(string));
            dtOrderDetail.Columns.Add("SKUShippingCost", typeof(decimal));
            dtOrderDetail.Columns.Add("EDSKU", typeof(string));
            dtOrderDetail.Columns.Add("FinalFee", typeof(decimal));
            dtOrderDetail.Columns.Add("SKUTotalPrice", typeof(decimal));
            dtOrderDetail.Columns.Add("DiscountFromCoin", typeof(decimal));
            dtOrderDetail.Columns.Add("DiscountFromVoucher", typeof(decimal));
            dtOrderDetail.Columns.Add("DiscountFromVoucherSeller", typeof(decimal));
            dtOrderDetail.Columns.Add("SellerRebate", typeof(decimal));
            dtOrderDetail.Columns.Add("DealPrice", typeof(decimal));
            dtOrderDetail.Columns.Add("CreditCardPromotion", typeof(decimal));
            dtOrderDetail.Columns.Add("skuOtherPrice", typeof(decimal));
            dtOrderDetail.Columns.Add("IsProcessRefund", typeof(bool));
            return dtOrderDetail;
        }

        public static DataTable InitBatchData()
        {
            var orderDetailDT = InitBatchShopeeOrderDetailTable();
            var drDetail = orderDetailDT.NewRow();
            drDetail["OrderID"] = "OP1231312321";
            drDetail["ItemID"] = "12131231";
            drDetail["ItemName"] = "产品123123";
            drDetail["ItemSku"] = "YU123123";
            drDetail["VariationID"] = 0;
            drDetail["VariationName"] = "123123";
            drDetail["VariationSku"] = "asedasd";
            drDetail["VariationQuantityPurchased"] = 0;
            drDetail["VariationOriginalPrice"] = 0;
            drDetail["VariationDiscountedPrice"] = 0;
            drDetail["UniqueKey"] = "GUI1213213";
            drDetail["isUpToTemp"] = false;
            drDetail["CreateDate"] = DateTime.Now;
            drDetail["CreateUser"] = "admin";
            drDetail["UpdateDate"] = DateTime.Now;
            drDetail["UpdateUser"] = "admin";
            drDetail["SKUShippingCost"] = 0;
            drDetail["EDSKU"] = "EDPOS";
            drDetail["FinalFee"] = 0;
            drDetail["SKUTotalPrice"] = 0;
            drDetail["DiscountFromCoin"] = 0;
            drDetail["DiscountFromVoucher"] = 0;
            drDetail["DiscountFromVoucherSeller"] = 0;
            drDetail["SellerRebate"] = 0;
            drDetail["DealPrice"] = 0;
            drDetail["CreditCardPromotion"] = 0;
            drDetail["skuOtherPrice"] = 0;
            drDetail["IsProcessRefund"] = true;
            orderDetailDT.Rows.Add(drDetail);
            return orderDetailDT;
        }
    }
}
