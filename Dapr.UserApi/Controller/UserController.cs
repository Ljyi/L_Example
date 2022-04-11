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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        // 通过HttpClient调用BackEnd
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            using var httpClient = DaprClient.CreateInvokeHttpClient();
            var result = await httpClient.GetAsync("http://localhost:5000/api/Order");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }
        // 通过DaprClient调用BackEnd
        [HttpGet("get2")]
        public async Task<ActionResult> Get2Async()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodAsync<string>(HttpMethod.Get, "DaprOrderApi", "api/order");
            return Ok(result);
        } 
        // 通过DaprClient调用BackEnd
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
            var result = await httpClient.GetAsync("http://127.0.0.1:5000/api/product");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }

    }
}
