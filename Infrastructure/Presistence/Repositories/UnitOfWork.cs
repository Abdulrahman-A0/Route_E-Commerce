using Domain.Contracts;
using Domain.Entities;
using Presistence.Data.Contexts;
using System.Collections.Concurrent;

namespace Presistence.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepository<TEntity, TKey>)repositories
            .GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(context));

        //var key = typeof(TEntity).Name;

        //if (!repositories.ContainsKey(key))
        //    repositories[key] = new GenericRepository<TEntity, TKey>(context);

        //return (IGenericRepository<TEntity, TKey>) repositories[key];


        public async Task<int> SaveChangesAsync()
        => await context.SaveChangesAsync();
    }
}
