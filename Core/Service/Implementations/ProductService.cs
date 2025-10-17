using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Products;
using Service.Specifications;
using ServiceAbstraction.Contracts;
using Shared;
using Shared.DTOs;

namespace Service.Implementations
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsResult = mapper.Map<IEnumerable<BrandResultDTO>>(brands);
            return brandsResult;
        }

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            var productsResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);
            return productsResult;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = mapper.Map<IEnumerable<TypeResultDTO>>(types);
            return typesResult;
        }

        public async Task<ProductResultDTO> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            var productResult = mapper.Map<ProductResultDTO>(product);
            return productResult;
        }
    }
}
