using AutoMapper;
using Data.DataContext;
using Data.Interfaces.IBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Repository.Base
{
    public abstract class RepositoryBase<TEntity, TEntityDto> : IRepositoryBase<TEntity, TEntityDto> 
        where TEntity  : class, new()
        where TEntityDto : class, new()
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _autoMapper;
        protected DbSet<TEntity> DbSet { get; }

        protected RepositoryBase(IServiceProvider service)
        {
            _context = service.GetRequiredService<AppDbContext>();
            _autoMapper = service.GetRequiredService<IMapper>();
            DbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            if(expression != null)
                return DbSet.Where(expression);

            return DbSet;
        }

        public async Task InsertAsync(TEntityDto entityDto)
        {
            var entity = _autoMapper.Map<TEntity>(entityDto);

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, TEntityDto entityDto)
        {
            _autoMapper.Map(entityDto, entity);

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(TEntity[] entityArray)
        {
            _context.Set<TEntity>().RemoveRange(entityArray);
            await _context.SaveChangesAsync();
        }
    }
}
