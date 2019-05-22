using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    /// <summary>
    /// 跨境专用订单创建。创建订单最多允许100个SKU，
    /// 且必须为同一个供应商的商品。超过50个SKU或者一些特殊情况会一次创建多个个订单并返回多个订单号。 支持大市场及分销两个场景。
    /// </summary>
    public class CreateOrderParam : BaseRequest
    {
        public CreateOrderParam(string userName) : base(userName)
        {
            NamespaceValue = "com.alibaba.trade";
            Name = "alibaba.trade.createCrossOrder";
            RequestType = "CreateOrder";
        }
        /// <summary>
        /// 发票信息
        /// </summary>
        public string invoiceParam { get; set; }
        /// <summary>
        /// 收货地址信息
        /// </summary>
        public string addressParam { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string cargoParamList { get; set; }
        /// <summary>
        /// 流程
        /// </summary>
        public string flow = "general";
        /// <summary>
        /// 买家留言
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 交易方式
        /// </summary>
        public string tradeType { get; set; }
        /// <summary>
        /// 店铺优惠ID
        /// </summary>
        public string shopPromotionId { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("addressParam", addressParam);
            dictParameters.Add("cargoParamList", cargoParamList);
            dictParameters.Add("flow", flow);
            if (!string.IsNullOrEmpty(invoiceParam))
            {
                dictParameters.Add("invoiceParam", invoiceParam);
            }
            if (!string.IsNullOrEmpty(tradeType))
            {
                dictParameters.Add("tradeType", tradeType);
            }
            if (!string.IsNullOrEmpty(shopPromotionId))
            {
                dictParameters.Add("shopPromotionId", shopPromotionId);
            }
            if (!string.IsNullOrEmpty(message))
            {
                // message = System.Web.HttpUtility.HtmlEncode(message);
                message = message.Replace("+", "");
                message = message.Replace(">", "");
                message = message.Replace("#", "");
                message = message.Replace("/", "");
                message = message.Replace("-", "");
                dictParameters.Add("message", message);
            }
        }
    }
}
