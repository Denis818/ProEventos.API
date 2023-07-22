using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Data.Interfaces.Base;
using Domain.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ServiceAppBase<TEntity, TIRepository>
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

        public void NotificarInformacao(string message) => _notificador.Add(new Notificacao(message));
    }
}
