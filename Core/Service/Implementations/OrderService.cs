using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Entities.Order;
using Domain.Entities.Products;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Specifications;
using ServiceAbstraction.Contracts;
using Shared.DTOs.OrderModule;

namespace Service.Implementations
{
    public class OrderService(IMapper _mapper,
        IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            var address = _mapper.Map<Address>(orderRequest.ShipToAddress);
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>()
                    .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(product, item));
            }
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryNotFoundException(orderRequest.DeliveryMethodId);
            var orderExist = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if (orderExist != null)
            {
                orderRepo.Delete(orderExist);

            }
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);
            await orderRepo.AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResult>(orderToCreate);
        }

        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(productInOrderItem, product.Price, item.Quantity);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderSpecification(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(id);
            return _mapper.Map<OrderResult>(order);
        }

        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderResult>> GetAllOrdersAsync(string email)
        {
            var specs = new OrderSpecification(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
