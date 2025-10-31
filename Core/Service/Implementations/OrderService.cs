using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Entities.Order;
using Domain.Entities.Products;
using Domain.Exceptions;
using ServiceAbstraction.Contracts;
using Shared.DTOs.OrderModule;

namespace Service.Implementations
{
    public class OrderService(IMapper _mapper,
        IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            var address = _mapper.Map<Address>(orderRequest.ShippingAddress);
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.GetRepository<Product, int>()
                    .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(product, item));
            }
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryNotFoundException(orderRequest.DeliveryMethodId);
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResult>(orderToCreate);
        }

        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(productInOrderItem, product.Price, item.Quantity);
        }

        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
