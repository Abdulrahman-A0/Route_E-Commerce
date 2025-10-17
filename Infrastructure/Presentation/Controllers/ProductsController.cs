using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllProductsAsync([FromQuery] ProductSpecificationParameters parameters)
            => Ok(await serviceManager.ProductService.GetAllProductsAsync(parameters));

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrandsAsync()
            => Ok(await serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypesAsync()
            => Ok(await serviceManager.ProductService.GetAllTypesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDTO>> GetProductByIdAsync(int id)
            => Ok(await serviceManager.ProductService.GetProductByIdAsync(id));

    }
}
