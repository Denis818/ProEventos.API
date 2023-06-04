using Data.DataContext;
using Data.Intefaces;
using Data.Repository.Base;
using Domain.Models;
using Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository
{
    public class EventoRepository : RepositoryBase<Evento, EventoDto>, IEventoRepository
    {
        public EventoRepository(IServiceProvider service) : base(service)
        {
        }

        public async Task<IEnumerable<Evento>> GetAllEventosAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Get().Include(evento => evento.Lote)
                                            .Include(evento => evento.RedesSociais);

            if (includePalestrantes)
                query = query.Include(evento => evento.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Palestrantes);

            return await query.OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<IEnumerable<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Get(evento => evento.Tema.ToLower().Contains(tema.ToLower()))
                                      .Include(evento => evento.Lote)
                                      .Include(evento => evento.RedesSociais);
                    
            if (includePalestrantes)
                query = query.Include(evento => evento.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Palestrantes);

            return await query.OrderBy(evento => evento.Id).ToListAsync();
        }

        public async Task<Evento> GetAllEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Get(evento => evento.Id == id)
                                      .Include(evento => evento.Lote)
                                      .Include(evento => evento.RedesSociais);

            if (includePalestrantes)
                query = query.Include(evento => evento.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Palestrantes);

            return await query.SingleOrDefaultAsync();
        }
    }
}
