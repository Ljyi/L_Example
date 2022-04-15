using Dapr.Actors;

namespace Dapr.OrderApi.Service
{
    public interface IOrderStatusActorService : IActor
    {
        Task<string> Paid(string orderId);
        Task<string> GetStatus(string orderId);
    }
}
