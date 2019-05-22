using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopee.Response
{
    public class GetEscrowDetailsResponse
    {
        public string request_id { get; set; }
        public OrderEscrowDetail order { get; set; }
    }
    public class OrderEscrowDetail
    {
        public List<Activity> activity { get; set; }
        public string payee_id { get; set; }
        public string shipping_carrier { get; set; }
        public string exchange_rate { get; set; }
        public IncomeDetails income_details { get; set; }
        public string country { get; set; }
        public string escrow_currency { get; set; }
        public string escrow_channel { get; set; }
        public List<Item> items { get; set; }
        public string ordersn { get; set; }
    }
    public class Activity
    {
        public decimal discounted_price { get; set; }
        public decimal original_price { get; set; }
        public long activity_id { get; set; }
        public List<Item> items { get; set; }
        public string activity_type { get; set; }
    }
    public class Item
    {
        public long item_id { get; set; }
        public decimal original_price { get; set; }
        public int quantity_purchased { get; set; }
        public long variation_id { get; set; }
    }
    public class IncomeDetails
    {
        public string local_currency { get; set; }
        public string total_amount { get; set; }
        public string actual_shipping_cost { get; set; }
        public string seller_rebate { get; set; }
        public string voucher_seller { get; set; }
        public string credit_card_promotion { get; set; }
        public string voucher_name { get; set; }
        public string escrow_amount { get; set; }
        public string shipping_fee_rebate { get; set; }
        public string commission_fee { get; set; }
        public string credit_card_transaction_fee { get; set; }
        public string voucher { get; set; }
        public string voucher_code { get; set; }
        public string coin { get; set; }
        public string cross_border_tax { get; set; }
    }
}
