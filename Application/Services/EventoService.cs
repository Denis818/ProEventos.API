using Application.Interfaces;
using Application.Services.Base;
using Data.Repository;
using Domain.Models;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, EventoRepository>, IEventoService
    {
        public EventoService(IServiceProvider service) : base(service)
        {
        }

        public async Task<Evento> InsertEvento(Evento evento)
        {

            await _repository.InsertAsync(evento);
            if (await _repository.SaveChangesAsync())
            {
                return await _repository.GetAllEventosByIdAsync(evento.Id, false);
            }

            return null;
        }

        public async Task<Evento> UpdateEvento(int id, Evento modelEvento)
        {
            var evento = await _repository.GetAllEventosByIdAsync(id, false);

            if (evento == null) return null;

            modelEvento.Id = evento.Id;

            _repository.UpdateAsync(modelEvento);

            if (await _repository.SaveChangesAsync())
            {
                return await _repository.GetAllEventosByIdAsync(modelEvento.Id, false);
            }

            return null;
        }

        public Task<IEnumerable<Evento>> GetAllEventosAsync(string tema, bool includePalestrantes = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            throw new NotImplementedException();
        }

        public Task<Evento> GetAllEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            throw new NotImplementedException();
        }
    }
}
