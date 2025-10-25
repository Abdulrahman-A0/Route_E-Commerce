using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}
