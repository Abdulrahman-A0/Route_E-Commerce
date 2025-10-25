using Shared;
using Shared.DTOs.ProductModule;

namespace ServiceAbstraction.Contracts
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
        Task<ProductResultDTO> GetProductByIdAsync(int id);

    }
}
