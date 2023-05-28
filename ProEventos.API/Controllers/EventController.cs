using Data.Intefaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _eventRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Event> GetById(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        /*[HttpPost]
        public async*/
    }
}
 