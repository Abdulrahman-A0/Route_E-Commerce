using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared.DTOs.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest order)
            => Ok(await _serviceManager.OrderService.CreateOrderAsync(order, GetUserEmail()));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersAsync()
            => Ok(await _serviceManager.OrderService.GetAllOrdersAsync(GetUserEmail()));

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
            => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
            => Ok(await _serviceManager.OrderService.GetDeliveryMethodsAsync());
    }
}
