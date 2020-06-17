using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories.GenericRepository
{
    public class GenericRepository<TEntity>
            : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MainContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Save() => await _dbContext.SaveChangesAsync();

        public async Task<ICollection<TEntity>> GetAll(int currentPage = 0)
        {
            return await _dbSet.Skip((currentPage - 1) * currentPage).Take(20).ToListAsync();
        }
    }
}
