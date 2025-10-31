namespace ServiceAbstraction.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
