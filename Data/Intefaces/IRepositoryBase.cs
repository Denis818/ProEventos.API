namespace Data.Intefaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
    }
}
