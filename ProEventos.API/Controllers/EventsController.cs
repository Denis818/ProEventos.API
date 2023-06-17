using Application.Interfaces.Services;
using Application.Services;
using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Controllers.Base;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : MainController
    {
        private readonly IEventoRepository _eventRepository;
        private readonly IEventoService eventoService;

        public EventsController(IServiceProvider service, IEventoRepository eventRepository, IEventoService eventoService) : base(service)
        {
            _eventRepository = eventRepository;
            this.eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listEvents = await eventoService.GetAllEventosAsync();//await _eventRepository.Get().AsNoTracking().ToListAsync();

            if (listEvents == null)
            {
                return NotFound("Nenhum evento encontrado");
            }

            return CustomResponse(listEvents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById(int id)
        {
            var evento = await _eventRepository.Get(e => e.Id == id).SingleOrDefaultAsync();

            if (evento == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento eventoDto)
        {
            if (eventoDto == null)
            {
                return BadRequest("Evento não pode ser nulo");
            }

            await _eventRepository.InsertAsync(eventoDto);

            return Ok("Criado com sucesso");
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Evento eventoDto)
        {
            if (eventoDto == null)
            {
                return BadRequest("Evento não pode ser nulo");
            }

            var evento = await _eventRepository.Get(e => e.Id == id).SingleOrDefaultAsync();

            if (evento == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }

            _eventRepository.UpdateAsync(evento);

            return CustomResponse<Evento>(null);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           

            return NoContent();
        }
    }
}
