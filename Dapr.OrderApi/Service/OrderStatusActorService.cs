using Dapr.Actors.Runtime;

namespace Dapr.OrderApi.Service
{
    public class OrderStatusActorService : Actor, IOrderStatusActorService
    {
        public OrderStatusActorService(ActorHost host) : base(host)
        {
        }
        public async Task<string> Paid(string orderId)
        {
            // change order status to paid
            await StateManager.AddOrUpdateStateAsync(orderId, "init", (key, currentStatus) => "paid");
            return orderId;
        }

        public async Task<string> GetStatus(string orderId)
        {
            return await StateManager.GetStateAsync<string>(orderId);
        }

    }
}
