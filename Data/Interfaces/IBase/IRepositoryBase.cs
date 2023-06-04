using System.Linq.Expressions;

namespace Data.Interfaces.IBase
{
    public interface IRepositoryBase<TEntity, TEntityDto> where TEntity : class, new()
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null);
        Task InsertAsync(TEntityDto entityDto);
        Task UpdateAsync(TEntity entity, TEntityDto entityDto);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(TEntity[] entity);
    }
}
