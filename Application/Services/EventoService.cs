using Application.Interfaces;
using Data.Intefaces;
using Domain.Models;

namespace Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<Evento> InsertEvento(Evento evento)
        {
            try
            {
                await _eventoRepository.InsertAsync(evento);
                if (await _eventoRepository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetAllEventosByIdAsync(evento.Id, false);
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
                var evento = await _eventoRepository.GetAllEventosByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _eventoRepository.UpdateAsync(model);

                if (await _eventoRepository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetAllEventosByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventosByIdAsync(id, false) ?? 
                    throw new Exception("Evento para delete não foi encontrado");

                _eventoRepository.DeleteAsync(evento);

                return await _eventoRepository.SaveChangesAsync();
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
