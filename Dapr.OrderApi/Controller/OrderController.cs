using Dapr.OrderApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dapr.OrderApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("orderInfo")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult GetAsync(string orderId)
        {
            var resultContent = string.Format("result is orderId:{0}", orderId);
            return Ok(resultContent);
        }
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("orderInfovo")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult GetOrderAsync(string orderId)
        {
            Order order = new Order()
            {
                orderitems = new List<Orderitem>() {
                    new Orderitem()
                    {
                        pictureurl="http://file.neware.com.cn/product/P0001043/%E9%A6%96%E5%9B%BE.jpg",
                        productname="纽扣电池壳CR2025、CR2032、CR2016",
                        unitprice=0.68,
                        units=600
                    }
                },
                ordernumber = 1,
                status = "确认收货",
                country = "中国",
                city = "江苏省苏州市",
                street = "苏州大学独墅湖校区一期仁爱路199号",
                total = 408,
                zipcode = "",
                date = DateTime.Now,
                description = "哈哈哈哈",
            };
            return Ok(order);
        }
    }
}
