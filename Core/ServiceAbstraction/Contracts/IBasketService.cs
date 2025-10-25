using Shared.DTOs.BasketModule;

namespace ServiceAbstraction.Contracts
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string id);
        Task<bool> DeleteBasketAsync(string id);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basketDTO);
    }
}