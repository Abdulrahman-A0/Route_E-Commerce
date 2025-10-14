using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction.Contracts;

namespace Service.Implementations
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => productService.Value;
    }
}
