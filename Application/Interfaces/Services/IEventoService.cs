using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IEventoService
    {
        Task<Evento> InsertAsync(Evento evento);
        Task<Evento> UpdateAsync(int id, Evento evento);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}