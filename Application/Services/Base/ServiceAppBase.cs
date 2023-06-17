using Application.Interfaces.Utility;
using Application.Utilities;
using Data.Interfaces.Base;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ServiceAppBase<TEntity, TIRepository>
        where TEntity : class, new()
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        protected readonly TIRepository _repository;
        protected readonly INotificador _notificador;

        protected ServiceAppBase(IServiceProvider service)
        {
            _repository = service.GetRequiredService<TIRepository>();
            _notificador = service.GetRequiredService<INotificador>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                NotificarError("Entidade não pode ser nula.");
                return null;
            }

            await _repository.InsertAsync(entity);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarError("Ocorreu um erro ao adicionar");
                return null;
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                NotificarError($"Id {id} não foi encontrado.");
                return false;
            }
                
            _repository.DeleteAsync(entity);

            return await _repository.SaveChangesAsync();
        }

        public void NotificarError(string message) =>
            _notificador.Add(new Notificacao(EnumTipoNotificacao.Error, message));

        public virtual bool OperacaoValida() =>
            !(_notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.Error).Count() > 0);
    }
}
