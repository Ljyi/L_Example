using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapr.UserApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DaprClient _daprClient;

        public UserController(ILogger<UserController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("orderinfo")]
        public async Task<ActionResult> GetOrderInfo()
        {
            var result = await _daprClient.InvokeMethodAsync<Model.Order>(HttpMethod.Get, "DaprOrderApi", "api/order/orderInfoVo?orderId=123");
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("orderinfo1")]
        public async Task<ActionResult> GetAsync()
        {
            using var httpClient = DaprClient.CreateInvokeHttpClient();
            var result = await httpClient.GetAsync("http://daprorderapi/api/order/orderInfoVo?orderId=123");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }
        [HttpGet("orderinfo2")]
        public async Task<ActionResult> Get2Async()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodAsync<Model.Order>(HttpMethod.Get, "DaprOrderApi", "api/order/orderInfoVo?orderId=123");
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("product")]
        public async Task<ActionResult> GetProductAsync()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodAsync<List<string>>(HttpMethod.Get, "DaprProductApi", "api/product");
            return Ok(result);
        }
        // 通过DaprClient调用BackEnd
        [HttpGet("product1")]
        public async Task<ActionResult> GetProduct1Async()
        {
            using var httpClient = DaprClient.CreateInvokeHttpClient();
            var result = await httpClient.GetAsync("http://daprproductapi/api/product");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }
        // 获取一个值
        [HttpGet]
        [HttpGet("statestore")]
        public async Task<ActionResult> GetAsync1()
        {
            var result = await _daprClient.GetStateAsync<string>("statestore", "guid");
            return Ok(result);
        }
        //保存一个值
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            await _daprClient.SaveStateAsync<string>("statestore", "guid", Guid.NewGuid().ToString(), new StateOptions() { Consistency = ConsistencyMode.Strong });
            return Ok("done");
        }

        //删除一个值
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            await _daprClient.DeleteStateAsync("statestore", "guid");
            return Ok("done");
        }

        //通过tag防止并发冲突，保存一个值
        [HttpPost(" withtag")]
        public async Task<ActionResult> PostWithTagAsync()
        {
            var (value, etag) = await _daprClient.GetStateAndETagAsync<string>("statestore", "guid");
            await _daprClient.TrySaveStateAsync<string>("statestore", "guid", Guid.NewGuid().ToString(), etag);
            return Ok("done");
        }

        //通过tag防止并发冲突，删除一个值
        [HttpDelete("withtag")]
        public async Task<ActionResult> DeleteWithTagAsync()
        {
            var (value, etag) = await _daprClient.GetStateAndETagAsync<string>("statestore", "guid");
            return Ok(await _daprClient.TryDeleteStateAsync("statestore", "guid", etag));
        }


        // 从绑定获取一个值，健值name从路由模板获取
        [HttpGet("frombinding/{name}")]
        public async Task<ActionResult> GetFromBindingAsync([FromState("statestore", "name")] StateEntry<string> state)
        {
            return Ok(state.Value);
        }


        // 根据绑定获取并修改值，健值name从路由模板获取
        [HttpPost("withbinding/{name}")]
        public async Task<ActionResult> PostWithBindingAsync([FromState("statestore", "name")] StateEntry<string> state)
        {
            state.Value = Guid.NewGuid().ToString();
            return Ok(await state.TrySaveAsync());
        }


        // 获取多个个值
        [HttpGet("list")]
        public async Task<ActionResult> GetListAsync()
        {
            var result = await _daprClient.GetBulkStateAsync("statestore", new List<string> { "guid" }, 10);
            return Ok(result);
        }

        // 删除多个个值
        [HttpDelete("list")]
        public async Task<ActionResult> DeleteListAsync()
        {
            var data = await _daprClient.GetBulkStateAsync("statestore", new List<string> { "guid" }, 10);
            var removeList = new List<BulkDeleteStateItem>();
            foreach (var item in data)
            {
                removeList.Add(new BulkDeleteStateItem(item.Key, item.ETag));
            }
            await _daprClient.DeleteBulkStateAsync("statestore", removeList);
            return Ok("done");
        }
    }
}
