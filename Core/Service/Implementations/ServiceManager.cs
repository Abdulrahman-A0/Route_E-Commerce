using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction.Contracts;

namespace Service.Implementations
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepo) : IServiceManager
    {
        private readonly Lazy<IProductService> productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> basketService = new Lazy<IBasketService>(() => new BasketService(basketRepo, mapper));

        public IProductService ProductService => productService.Value;
        IBasketService IServiceManager.BasketService => basketService.Value;
    }
}
