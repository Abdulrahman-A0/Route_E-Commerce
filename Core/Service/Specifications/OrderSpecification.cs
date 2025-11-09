using Domain.Entities.Order;

namespace Service.Specifications
{
    internal class OrderSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderSpecification(string email) : base(o => o.UserEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }

        public OrderSpecification(Guid id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
    }
}
