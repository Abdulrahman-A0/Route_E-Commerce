using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceAbstraction.Contracts;
using Shared.Common;

namespace Service.Implementations
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepo, UserManager<User> userManager, IOptions<JwtOptions> options) : IServiceManager
    {
        private readonly Lazy<IProductService> productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> basketService = new Lazy<IBasketService>(() => new BasketService(basketRepo, mapper));
        private readonly Lazy<IAuthenticationService> authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, options));

        public IProductService ProductService => productService.Value;
        public IBasketService BasketService => basketService.Value;
        public IAuthenticationService AuthenticationService => authService.Value;
    }
}
