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

        public virtual async Task<Evento> InsertEvento(TEntity entity)
        {
            try
            {
                await _repository.InsertAsync(entity);

                if (await _repository.SaveChangesAsync())
                {
                    
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
