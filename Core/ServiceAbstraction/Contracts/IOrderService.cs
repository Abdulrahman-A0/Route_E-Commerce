using Shared.DTOs.OrderModule;

namespace ServiceAbstraction.Contracts
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail);
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail);
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
