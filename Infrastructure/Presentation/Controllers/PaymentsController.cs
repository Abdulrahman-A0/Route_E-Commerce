using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared.DTOs.BasketModule;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
            => Ok(await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];
            await serviceManager.PaymentService.UpdatePaymentStatusAsync(json, signatureHeader);
            return new EmptyResult();
        }
    }
}
