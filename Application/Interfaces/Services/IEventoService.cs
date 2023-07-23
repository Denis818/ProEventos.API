using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IEventoService
    {
        Task<EventoDto> InsertAsync(EventoDto evento);
        Task<EventoDto> UpdateAsync(int id, EventoDto evento);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteRangerAsync(int[] ids);
        Task<IEnumerable<EventoDto>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<IEnumerable<EventoDto>> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false);
    }
}