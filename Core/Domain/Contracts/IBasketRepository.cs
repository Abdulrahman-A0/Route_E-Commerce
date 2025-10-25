using Domain.Entities.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string id);
    }
}
