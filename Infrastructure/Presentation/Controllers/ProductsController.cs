using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared;
using Shared.DTOs.ProductModule;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDTO>>> GetAllProductsAsync([FromQuery] ProductSpecificationParameters parameters)
            => Ok(await serviceManager.ProductService.GetAllProductsAsync(parameters));

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrandsAsync()
            => Ok(await serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypesAsync()
            => Ok(await serviceManager.ProductService.GetAllTypesAsync());

        [ProducesResponseType(typeof(ProductResultDTO), StatusCodes.Status200OK)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDTO>> GetProductByIdAsync(int id)
            => Ok(await serviceManager.ProductService.GetProductByIdAsync(id));

    }
}
