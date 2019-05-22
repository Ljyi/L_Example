using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Console.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateShipmentRequstParameter
    {
        public string[] order_item_ids { get; set; }
        public string tracking_number { get; set; }
        public string shipping_description { get; set; }
        public string weight { get; set; }
        public string shipper_id { get; set; }
        public string tracking_url { get; set; }
    }
    public class CreateShipmentRequstResponse
    {
        public string res { get; set; }
        public string msg { get; set; }
        public FulfillmentDetails fulfillment_details { get; set; }
        public string fulfillment_id { get; set; }
    }
    public class FulfillmentDetails
    {
        public string order_id { get; set; }
        public string[] order_item_ids { get; set; }
        public string status { get; set; }
        public string tracking_url { get; set; }
        public string shipping_description { get; set; }
        public string merchant_id { get; set; }
        public string shipper_id { get; set; }
        public string post_actions { get; set; }
        public string fulfillment_service_id { get; set; }
        public string created_at { get; set; }
        public string merchant_track_id { get; set; }
        public string fulfillment_response { get; set; }
        public string tracking_number { get; set; }
    }

    public class BulkmarkShippedResponse
    {
        public int changedRows { get; set; }
        public string[] error { get; set; }
        public List<BulkmarkShippedSuccess> success { get; set; }
    }
    public class BulkmarkShippedSuccess
    {
        public string ffid { get; set; }
        public string item_id { get; set; }
        public string message { get; set; }
    }
}
