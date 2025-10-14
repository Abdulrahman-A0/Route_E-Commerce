using AutoMapper;
using Domain.Entities.Products;
using Shared.DTOs;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductType, TypeResultDTO>();

            CreateMap<ProductBrand, BrandResultDTO>();

            CreateMap<Product, ProductResultDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureURL, opt => opt.MapFrom<PictureURLResolver>());
        }
    }
}
