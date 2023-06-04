using Data.Interfaces;
using Data.Repository.Base;
using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class LoteRepository : RepositoryBase<Lote>, ILoteRepository
    {
        public LoteRepository(IServiceProvider service) : base(service)
        {
        }
    }
}
