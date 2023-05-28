using Data.DataContext;
using Data.Intefaces;
using Data.Repository.Base;
using Domain.Models;

namespace Data.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }
    }
}
