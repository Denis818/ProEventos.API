using AutoMapper;
using Data.DataContext;
using Data.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repository.Base
{
    public abstract class RepositoryBase<TEntity, TEntityDto> : IRepositoryBase<TEntity, TEntityDto> where TEntity  : class, new()
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _autoMapper;

        protected RepositoryBase(IServiceProvider service)
        {
            _context = service.GetRequiredService<AppDbContext>();
            _autoMapper = service.GetRequiredService<IMapper>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
