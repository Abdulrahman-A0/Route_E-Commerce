using AutoMapper;
using Domain.Entities.Basket;
using Shared.DTOs.BasketModule;

namespace Service.MappingProfiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
