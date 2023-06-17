using Application.Interfaces.Services;
using Application.Services.Base;
using Data.Intefaces;
using Data.Repository;
using Domain.Models;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, IEventoRepository>, IEventoService
    {
        public EventoService(IServiceProvider service) : base(service)
        {
        }

        public async Task<Evento> InsertAsync(Evento evento)
        {
            await _repository.InsertAsync(evento);

            if (await _repository.SaveChangesAsync())
            {
                return await _repository.GetAllEventosByIdAsync(evento.Id, false);
            }

            return null;
        }

        public async Task<Evento> UpdateAsync(int id, Evento modelEvento)
        {
            var evento = await _repository.GetAllEventosByIdAsync(id, false);

            if (evento == null)
            {
                NotificarError("Teste Erro Deu bão");
                NotificarError("Eroooooo bão");
                NotificarError("1234 eroes  Deu bão");

                NotificarError("Laiola ewrros denis Erro Deu bão");
                return null;
            }
            NotificarError("Eroooooo bão");
            NotificarError("1234 eroes  Deu bão");

            NotificarError("Laiola ewrros denis Erro Deu bão");

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
