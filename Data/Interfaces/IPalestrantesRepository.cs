﻿using Data.Interfaces.IBase;
using Domain.Dtos;
using Domain.Models;

namespace Data.Interfaces
{
    public interface IPalestrantesRepository : IRepositoryBase<Palestrante, PalestranteDto>
    {
        Task<IEnumerable<Palestrante>> GetAllEventosByNameAsync(string nome, bool includePalestrantes);
        Task<IEnumerable<Palestrante>> GetAllEventosAsync(string tema, bool includePalestrantes);
        Task<Palestrante> GetAllEventosByIdAsync(int id, bool includePalestrantes);
    }
}
