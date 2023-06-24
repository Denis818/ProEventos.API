using Data.Interfaces;
using Data.Repository.Base;
using Domain.Models;

namespace Data.Repository
{
    public class LoteRepository : RepositoryBase<Lote>, ILoteRepository
    {
        public LoteRepository(IServiceProvider service) : base(service)
        {
        }
    }
}
