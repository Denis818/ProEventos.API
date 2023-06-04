﻿using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventoRepository _eventRepository;

        public EventsController(IEventoRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetAll()
        {
            var listEvents = await _eventRepository.Get().ToListAsync();

            if(listEvents == null || !listEvents.Any())
            {
                return NotFound("Nenhum evento encontrado");
            }

            return Ok(listEvents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById(int id)
        {
            var evento = await _eventRepository.Get(e => e.Id == id).SingleOrDefaultAsync();

            if(evento == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto eventoDto)
        {
            if (eventoDto == null)
            {
                return BadRequest("Evento não pode ser nulo");
            }

            await _eventRepository.InsertAsync(eventoDto);

            return Ok("Criado com sucesso");
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, EventoDto eventoDto)
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

            await _eventRepository.UpdateAsync(evento, eventoDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEvent = await _eventRepository.Get(e => e.Id == id).SingleOrDefaultAsync();

            if (existingEvent == null)
            {
                return NotFound($"Evento com id {id} não encontrado");
            }
          
            await _eventRepository.DeleteAsync(existingEvent);

            return NoContent(); 
        }
    }
}
 