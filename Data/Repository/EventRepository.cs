using Data.DataContext;
using Data.Intefaces;
using Data.Repository.Base;
using Domain.Models;
using Domain.Dtos;

namespace Data.Repository
{
    public class EventRepository : RepositoryBase<Event, EventDto>, IEventRepository
    {
        public EventRepository(IServiceProvider service) : base(service)
        {
        }
    }
}
