using Shared;
using Shared.DTOs;

namespace ServiceAbstraction.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
        Task<ProductResultDTO> GetProductByIdAsync(int id);

    }
}
