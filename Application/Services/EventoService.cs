using Application.Interfaces.Services;
using Application.Services.Base;
using Data.Intefaces;
using Data.Repository;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, IEventoRepository>, IEventoService
    {
        public EventoService(IServiceProvider service) : base(service)
        {
        }

        public async Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosAsync(includePalestrantes);

            if (eventos.IsNullOrEmpty())
                NotificarError("Não foram encontrados nunhum evento");

            return eventos;
        }

        public async Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                NotificarError("Não foram encontrados nunhum evento");

            return eventos;
        }

        public async Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                NotificarError("Não foram encontrados nunhum evento");

            return evento;
        }

        public async Task<Evento> InsertAsync(Evento evento)
        {
            if (evento == null)
            {
                NotificarError("Evento não pode ser nulo.");
                return null;
            }

            await _repository.InsertAsync(evento);

            if (await _repository.SaveChangesAsync())
            {
                return await _repository.GetEventosByIdAsync(evento.Id, false);
            }

            return null;
        }

        public async Task<Evento> UpdateAsync(int id, Evento modelEvento)
        {
            var evento = await _repository.GetEventosByIdAsync(id, false);

            if (evento == null)
            {
                NotificarError("Evento não encontrado.");
                return null;
            }

            modelEvento.Id = evento.Id;

            _repository.UpdateAsync(modelEvento);

            if (await _repository.SaveChangesAsync())
            {
                return await _repository.GetEventosByIdAsync(modelEvento.Id, false);
            }

            return null;
        }
    }
}
