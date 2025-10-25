using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared.DTOs.BasketModule;

namespace Presentation.Controllers
{

    public class BasketController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasketAsync(string id)
            => Ok(await serviceManager.BasketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basketDTO)
            => Ok(await serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDTO));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
