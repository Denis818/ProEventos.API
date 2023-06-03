using Domain.Dtos;
using Domain.Models;

namespace Data.Intefaces
{
    public interface IEventRepository : IRepositoryBase<Event, EventDto>
    {
    }
}
