using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult GetAsync()
        {
            var resultContent = string.Format("result is {0}", "order 信息");
            return Ok(resultContent);
        }
    }
}
