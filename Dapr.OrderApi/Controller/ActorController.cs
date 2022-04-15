using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.OrderApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapr.OrderApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorProxyFactory _actorProxyFactory;
        public ActorController(IActorProxyFactory actorProxyFactory)
        {
            _actorProxyFactory = actorProxyFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("paid/{orderId}")]
        public async Task<ActionResult> PaidAsync(string orderId)
        {
            var actorId = new ActorId("myid-" + orderId);
            var proxy = ActorProxy.Create<IOrderStatusActorService>(actorId, "OrderStatusActor");
            var result = await proxy.Paid(orderId);
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("get/{orderId}")]
        public async Task<ActionResult> GetAsync(string orderId)
        {
            var proxy = _actorProxyFactory.CreateActorProxy<IOrderStatusActorService>(
                new ActorId("myid-" + orderId),
                "OrderStatusActor");

            return Ok(await proxy.GetStatus(orderId));
        }
    }
}
