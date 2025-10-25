using Domain.Entities.Products;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters) :
           base(p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
                        (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
                        (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {

        }
    }
}
