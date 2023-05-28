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
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        {
            var listEvents = await _eventRepository.GetAll();

            if(listEvents == null || listEvents.Any())
            {
                return NotFound("Nenhum evento encontrado");
            }

            return Ok(listEvents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            var evento = await _eventRepository.GetByIdAsync(id);

            if(evento == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event evento)
        {
            if (evento == null)
            {
                return BadRequest("Evento não pode ser nulo");
            }

            await _eventRepository.InsertAsync(evento);

            return CreatedAtAction(nameof(GetById), new { id = evento.Id }, evento);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Event evento)
        {
            if (evento == null)
            {
                return BadRequest("Evento não pode ser nulo");
            }

            var existingEvent = await _eventRepository.GetByIdAsync(evento.Id);
            if (existingEvent == null)
            {
                return NotFound($"Evento com id {evento.Id} não encontrado");
            }

            await _eventRepository.UpdateAsync(evento);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);

            if (existingEvent == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }

            await _eventRepository.DeleteAsync(id);

            return NoContent(); 
        }
    }
}
 