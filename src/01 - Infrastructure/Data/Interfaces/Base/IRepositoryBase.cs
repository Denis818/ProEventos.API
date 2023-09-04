using System.Linq.Expressions;

namespace Data.Interfaces.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        void DeleteRangeAsync(TEntity[] entity);

        Task<bool> SaveChangesAsync();
    }
}
