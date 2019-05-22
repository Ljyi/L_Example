using Newtonsoft.Json;
using Shopee.Requests;
using Shopee.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopee
{
    public static class ShopeeTest
    {
        #region 
        public static string jsonResult = @"{ 'order': {
                'activity': [{
			'discounted_price': '8.68',
        			'original_price': '9.64',
        			'activity_id': 10754,
        			'items': [{
				'item_id': 1392740274,
        				'original_price': '4.82',
        				'quantity_purchased': 2,
        				'variation_id': 1923100523}],'activity_type': 'BUNDLE'}, {
			'discounted_price': '8.68',
			'original_price': '9.64',
			'activity_id': 10754,
			'items': [{
				'item_id': 1392740274,
				'original_price': '4.82',
				'quantity_purchased': 2,
				'variation_id': 1923100523
			}],
			'activity_type': 'BUNDLE'
		}, {
			'discounted_price': '8.68',
			'original_price': '9.64',
			'activity_id': 10754,
			'items': [{
				'item_id': 1392740274,
				'original_price': '4.82',
				'quantity_purchased': 2,
				'variation_id': 1923100523
			}],
			'activity_type': 'BUNDLE'
		}, {
			'discounted_price': '8.68',
			'original_price': '9.64',
			'activity_id': 10754,
			'items': [{
				'item_id': 1392740274,
				'original_price': '4.82',
				'quantity_purchased': 2,
				'variation_id': 1923100523
			}],
			'activity_type': 'BUNDLE'
		}],
		'payee_id': '',
		'shipping_carrier': 'Standard Express',
		'exchange_rate': '1',
		'bank_account': {},
		'income_details': {
			'local_currency': 'MYR',
			'total_amount': '40.21',
			'actual_shipping_cost': '0.00',
			'seller_rebate': '0.00',
			'voucher_seller': '0.00',
			'credit_card_promotion': 0,
			'voucher_name': '',
			'escrow_amount': '38.47',
			'shipping_fee_rebate': '0.00',
			'commission_fee': '1.74',
			'credit_card_transaction_fee': 0,
			'voucher': '0.00',
			'voucher_code': '',
			'coin': '0.00',
			'cross_border_tax': 0
		},
		'country': 'MY',
		'escrow_currency': 'MYR',
		'escrow_channel': '',
		'items': [{
			'original_price': '22.00',
			'quantity_purchased': 2,
			'discounted_price': 0,
			'discount_from_voucher': 0,
			'variation_id': 1923100523,
			'variation_name': 'L',
			'item_id': 1392740274,
			'deal_price': 0,
			'credit_card_promotion': 0,
			'item_name': 'Premium White Large Plain Canvas Shopping Shoulder Top Tote Shopper Bag',
			'discount_from_coin': 0,
			'item_sku': '143516',
			'seller_rebate': 0,
			'variation_sku': '143516_3',
			'discount_from_voucher_seller': 0
		}, {
			'original_price': '22.00',
			'quantity_purchased': 2,
			'discounted_price': 0,
			'discount_from_voucher': 0,
			'variation_id': 1923100523,
			'variation_name': 'L',
			'item_id': 1392740274,
			'deal_price': 0,
			'credit_card_promotion': 0,
			'item_name': 'Premium White Large Plain Canvas Shopping Shoulder Top Tote Shopper Bag',
			'discount_from_coin': 0,
			'item_sku': '143516',
			'seller_rebate': 0,
			'variation_sku': '143516_3',
			'discount_from_voucher_seller': 0}, {'original_price': '22.00','quantity_purchased': 2,'discounted_price': 0,'discount_from_voucher': 0,'variation_id': 1923100523,'variation_name': 'L','item_id': 1392740274,'deal_price': 0,'credit_card_promotion': 0,'item_name': 'Premium White Large Plain Canvas Shopping Shoulder Top Tote Shopper Bag','discount_from_coin': 0,'item_sku': '143516','seller_rebate': 0,'variation_sku': '143516_3','discount_from_voucher_seller': 0}, {'original_price': '22.00','quantity_purchased': 2,'discounted_price': 0,'discount_from_voucher': 0,'variation_id': 1923100523,'variation_name': 'L','item_id': 1392740274,'deal_price': 0,'credit_card_promotion': 0,'item_name': 'Premium White Large Plain Canvas Shopping Shoulder Top Tote Shopper Bag','discount_from_coin': 0,'item_sku': '143516','seller_rebate': 0,'variation_sku': '143516_3','discount_from_voucher_seller': 0}],'ordersn': '18101814040SKFF'},'request_id': 'OQO8zz1mxdvz8fy5IWMIW'}";

        #endregion
        public static void ConvertJsonToModel(string jsonResult)
        {
            try
            {
                GetEscrowDetailsResponse escrowDetails = JsonConvert.DeserializeObject<GetEscrowDetailsResponse>(jsonResult);
                if (escrowDetails != null && escrowDetails.order != null)
                {
                    var activity = escrowDetails.order.activity;
                    if (activity.Count > 0 && activity.Exists(p => p.activity_type == "BUNDLE"))
                    {
                        string price = (activity.FirstOrDefault().discounted_price / activity.FirstOrDefault().original_price).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public static string GetEscrowDetails()
        {
            try
            {
                string url = "https://partner.shopeemobile.com/api/v1/orders/my_income";
                string SecretKey = "fd1a3697b76d97c49cd0354767cd541f0242e9d7da6208eb5cf9597d09531e6b";
                GetEscrowDetails esPOST = new GetEscrowDetails();
                esPOST.ordersn = "18042512105VPR5";
                esPOST.partner_id = 10011;
                esPOST.shopid = 27641585;
                esPOST.timestamp = Common.GetTimeStamp(DateTime.UtcNow);
                string postData = JsonConvert.SerializeObject(esPOST);
                string singure = url + "|" + postData;
                string authSign = Common.HMACSHA256Encrypt(SecretKey, singure);
                jsonResult = ShopeeRequest.PostRequest(url, postData, authSign);
                ConvertJsonToModel(jsonResult);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return jsonResult;
        }
        public static string GetOrderDetails()
        {
            try
            {
                string url = "https://partner.shopeemobile.com/api/v1/orders/detail";
                string SecretKey = "fd1a3697b76d97c49cd0354767cd541f0242e9d7da6208eb5cf9597d09531e6b";
                GetOrderDetails orderDetails = new GetOrderDetails();
                orderDetails.ordersn_list = new List<string>() { "18042512105VPR5" };
                orderDetails.partner_id = 10011;
                orderDetails.shopid = 27641585;
                orderDetails.timestamp = Common.GetTimeStamp(DateTime.UtcNow);
                string postData = JsonConvert.SerializeObject(orderDetails);
                string singure = url + "|" + postData;
                string authSign = Common.HMACSHA256Encrypt(SecretKey, singure);
                jsonResult = ShopeeRequest.PostRequest(url, postData, authSign);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return jsonResult;
        }
    }
}
