using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Data.Interfaces.Base;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ServiceAppBase<TEntity, TEntityDto, TIRepository>
        where TEntity : class, new()
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        protected readonly TIRepository _repository;
        protected readonly INotificador _notificador;
        protected readonly IMapper _mapper;

        protected ServiceAppBase(IServiceProvider service)
        {
            _repository = service.GetRequiredService<TIRepository>();
            _notificador = service.GetRequiredService<INotificador>();
            _mapper = service.GetRequiredService<IMapper>();
        }

        public async Task<TEntityDto> InsertAsync(TEntityDto entityDto)
        {
            if (entityDto == null)
            {
                NotificarInformacao("Entidade não pode ser nula.");
                return default;
            }

            var entity = _mapper.Map<TEntity>(entityDto);

            await _repository.InsertAsync(entity);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao("Ocorreu um erro ao adicionar");
                return default;
            }

            return entityDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                NotificarInformacao($"Id {id} não foi encontrado.");
                return false;
            }
                
            _repository.DeleteAsync(entity);

            return await _repository.SaveChangesAsync();
        }

        public void NotificarInformacao(string message) =>
            _notificador.Add(new Notificacao(message));
    }
}
