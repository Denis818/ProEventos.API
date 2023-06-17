using AutoMapper;
using Data.DataContext;
using Data.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        protected readonly AppDbContext _context;
        protected DbSet<TEntity> _dbSet { get; }

        protected RepositoryBase(IServiceProvider service)
        {
            _context = service.GetRequiredService<AppDbContext>();
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression != null)
                return _dbSet.Where(expression);

            return _dbSet;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRangeAsync(TEntity[] entityArray)
        {
            _context.Set<TEntity>().RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
