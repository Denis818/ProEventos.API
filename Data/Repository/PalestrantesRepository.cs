using Data.Interfaces;
using Data.Repository.Base;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PalestrantesRepository : RepositoryBase<Palestrante, PalestranteDto>, IPalestrantesRepository
    {
        public PalestrantesRepository(IServiceProvider service) : base(service)
        {
        }

        public async Task<IEnumerable<Palestrante>> GetAllEventosAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Palestrante> query = Get().Include(evento => evento.RedesSociais);

            if (includePalestrantes)
                query = query.Include(palestrante => palestrante.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Evento);

            return await query.OrderBy(palestrante => palestrante.Id).ToListAsync();
        }
        public async Task<IEnumerable<Palestrante>> GetAllEventosByNameAsync(string nome, bool includePalestrantes = false)
        {
            IQueryable<Palestrante> query = Get(palestrantes => palestrantes.Nome.ToLower().Contains(nome.ToLower()))
                                           .Include(palestrantes => palestrantes.RedesSociais);

            if (includePalestrantes)
                query = query.Include(evento => evento.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Palestrante);

            return await query.OrderBy(palestrante => palestrante.Id).ToListAsync();
        }

        public async Task<Palestrante> GetAllEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Palestrante> query = Get(palestrante => palestrante.Id == id)
                               .Include(palestrante => palestrante.RedesSociais);

            if (includePalestrantes)
                query = query.Include(palestrante => palestrante.PalestrantesEventos)
                             .ThenInclude(eventPalest => eventPalest.Palestrante);

            return await query.SingleOrDefaultAsync();
        }

     
    }
}
