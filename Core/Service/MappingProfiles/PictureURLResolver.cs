using AutoMapper;
using Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace Service.MappingProfiles
{
    internal class PictureURLResolver(IConfiguration configurations) : IValueResolver<Product, ProductResultDTO, string>
    {
        public string Resolve(Product source, ProductResultDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            return $"{configurations.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
