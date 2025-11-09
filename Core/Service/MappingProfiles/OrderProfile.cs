using AutoMapper;
using Domain.Entities.Order;
using Shared.DTOs.OrderModule;
using IdentityAddress = Domain.Entities.Identity.Address;
namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<IdentityAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>()
                .ForMember(dest => dest.Cost, options => options.MapFrom(src => src.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl));

            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.Status, options => options.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, options => options.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));
        }
    }
}
