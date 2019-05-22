using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class CreateOrderResponse : BaseResponse
    {
        public CreateOrdersResult result { get; set; }
    }
    public class CreateOrdersResult
    {
        /// <summary>
        /// 订单总金额（单位分），一次创建多个订单时，该字段为空
        /// </summary>
        public long totalSuccessAmount { get; set; }
        /// <summary>
        /// 订单ID，一次创建多个订单时，该字段为空
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 账期信息
        /// </summary>
        public AccountPeriod accountPeriod { get; set; }
        /// <summary>
        /// 失败商品信息
        /// </summary>
        public List<FailedOfferList> failedOfferList { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public long postFee { get; set; }
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<OrderList> orderList { get; set; }
    }
    public class AccountPeriod
    {
        public int tapType { get; set; }
        public int tapDate { get; set; }
        public int tapOverdue { get; set; }
    }
    public class FailedOfferList
    {
        public string offerId { get; set; }
        public string specId { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderList
    {
        public long postFee { get; set; }
        public long orderAmmount { get; set; }
        public string message { get; set; }
        public string resultCode { get; set; }
        public bool success { get; set; }

        public string orderId { get; set; }

    }
}
