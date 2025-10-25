using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Products;
using Domain.Exceptions;
using Service.Specifications;
using ServiceAbstraction.Contracts;
using Shared;
using Shared.DTOs.ProductModule;

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

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var productRepo = unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var products = await productRepo.GetAllAsync(specifications);
            var productsResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);

            var pageSize = productsResult.Count();
            var countSpecifications = new ProductCountSpecifications(parameters);
            var totalCount = await productRepo.CountAsync(countSpecifications);
            return new PaginatedResult<ProductResultDTO>(parameters.PageIndex, pageSize, totalCount, productsResult);
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
            return product is null ? throw new ProductNotFoundException(id) : mapper.Map<ProductResultDTO>(product);
        }
    }
}
