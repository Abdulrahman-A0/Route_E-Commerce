using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Entities.Order;
using Domain.Entities.Products;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using ServiceAbstraction.Contracts;
using Shared.DTOs.BasketModule;
using Stripe;
using Product = Domain.Entities.Products.Product;

namespace Service.Implementations
{
    public class PaymentService(IConfiguration _configuration, IBasketRepository _basketRepository
        , IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var basket = await GetBasketAsync(basketId);

            await ValidateBasketAsync(basket);

            var amount = CalculateTotalAsync(basket);

            await CreationOrUpdatePaymentIntentAsync(basket, amount);

            await _basketRepository.CreateOrUpdateBasketAsync(basket);

            return _mapper.Map<BasketDTO>(basket);
        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            var stripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount
                };
                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        private long CalculateTotalAsync(CustomerBasket basket)
        {
            return (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;
        }

        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            if (!basket.DeliveryMethodId.HasValue) throw new Exception("Np Delivery Method Selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value) ??
                throw new DeliveryNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
        }

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);
        }

        public async Task UpdatePaymentStatusAsync(string json, string signatureHeader)
        {
            string endpointSecret = _configuration["StripeSettings:EndPointSecret"];

            var stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch: false);

            stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusReceivedAsync(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailedAsync(paymentIntent.Id);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }

        private async Task UpdatePaymentStatusFailedAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            if (order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdatePaymentStatusReceivedAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            if (order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
