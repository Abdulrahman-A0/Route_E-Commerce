using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Exceptions;
using ServiceAbstraction.Contracts;
using Shared.DTOs.BasketModule;
using System.Linq.Expressions;

namespace Service.Implementations
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basketDTO)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDTO);
            var createdOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return createdOrUpdatedBasket is null ? throw new Exception("Can not create or update the basket")
                : _mapper.Map<BasketDTO>(createdOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
            => await _basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDTO>(basket);
        }
    }
}
