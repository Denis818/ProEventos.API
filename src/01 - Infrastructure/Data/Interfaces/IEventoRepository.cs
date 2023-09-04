using Data.Interfaces.Base;
using Domain.Dtos;
using Domain.Models;

namespace Data.Intefaces
{
    public interface IEventoRepository : IRepositoryBase<Evento>
    {
        Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<IEnumerable<Evento>> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}
