using Data.Interfaces.IBase;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ServiceAppBase<TEntity, TRepository>
        where TEntity : class, new()
        where TRepository : class, IRepositoryBase<TEntity>
    {
        protected readonly TRepository _repository;

        protected ServiceAppBase(IServiceProvider service)
        {
            _repository = service.GetRequiredService<TRepository>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id) ??
                throw new Exception("Id não foi encontrado.");

            _repository.DeleteAsync(entity);

            return await _repository.SaveChangesAsync();
        }
    }
}
