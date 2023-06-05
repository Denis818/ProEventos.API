using Domain.Models;

namespace Application.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> InsertAsync(Evento evento);
        Task<Evento> UpdateAsync(int id, Evento evento);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<IEnumerable<Evento>> GetAllEventosAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetAllEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}