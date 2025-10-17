using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(AppDbContext context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        => await context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
        => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        => asNoTracking ? await context.Set<TEntity>().AsNoTracking().ToListAsync()
            : await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
        => await context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        => context.Set<TEntity>().Update(entity);

        #region Specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        #endregion
    }
}
