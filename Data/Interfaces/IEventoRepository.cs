using Data.Interfaces.IBase;
using Domain.Dtos;
using Domain.Models;

namespace Data.Intefaces
{
    public interface IEventoRepository : IRepositoryBase<Evento, EventoDto>
    {
        Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<IEnumerable<Evento>> GetAllEventosAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetAllEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}
