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
            try
            {
                await _repository.InsertAsync(evento);
                if (await _repository.SaveChangesAsync())
                {
                    return await _repository.GetAllEventosByIdAsync(evento.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _repository.GetAllEventosByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _repository.UpdateAsync(model);

                if (await _repository.SaveChangesAsync())
                {
                    return await _repository.GetAllEventosByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
