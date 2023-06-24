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
                NotificarInformacao("Nenhum evento encontrado");

            return eventos;
        }

        public async Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                NotificarInformacao($"Eventos com tema {tema} não encontrados");

            return eventos;
        }

        public async Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                NotificarInformacao($"Evento com id {id} não encontrado");

            return evento;
        }

        public async Task<Evento> UpdateAsync(int id, Evento modelEvento)
        {
            if (modelEvento == null)
            {
                NotificarInformacao("Evento não pode ser nulo.");
                return null;
            }

            var evento = await _repository.GetEventosByIdAsync(id, false);

            if (evento == null)
            {
                NotificarInformacao("Evento não encontrado.");
                return null;
            }

            modelEvento.Id = evento.Id;

            _repository.UpdateAsync(modelEvento);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao("Ocorreu um erro ao atualizar evento.");
                return null;
            }

            return await _repository.GetEventosByIdAsync(modelEvento.Id, false);
            
        }
    }
}
