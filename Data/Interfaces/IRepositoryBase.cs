namespace Data.Intefaces
{
    public interface IRepositoryBase<TEntity, TEntityDto> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntityDto entityDto);
        Task UpdateAsync(TEntity entity, TEntityDto entityDto);
        Task DeleteAsync(TEntity entity);
    }
}
