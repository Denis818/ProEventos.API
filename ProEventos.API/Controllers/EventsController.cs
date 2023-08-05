using Application.Interfaces.Services;
using Application.Utilities;
using DadosInCached.CustomAttribute;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Controllers.Base;

namespace ProEventos.API.Controllers
{
    [Cached(15)]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : BaseApiController
    {
        private readonly IEventoService _eventoService;

        public EventsController(IServiceProvider service, IEventoService eventoService) : base(service)
        {
            _eventoService = eventoService;
        }

        [HttpGet()]
        public async Task<IEnumerable<EventoDto>> GetAll()
        {
            return await _eventoService.GetAllEventosAsync(true);
        }

        [HttpGet("{id}")]
        public async Task<EventoDto> GetById(int id)
        {
            return await _eventoService.GetEventosByIdAsync(id, true);
        }

        [HttpGet("tema/{tema}")]
        public async Task<IEnumerable<EventoDto>> GetByTema(string tema)
        {
            return await _eventoService.GetAllEventosByTemaAsync(tema, true);
        }

        [HttpPost]
        public async Task<EventoDto> Post(EventoDto eventodto)
        {
            return await _eventoService.InsertAsync(eventodto);
        }

        [HttpPut("{id}")]
        public async Task<EventoDto> Put(int id, EventoDto eventoDto)
        {
            return await _eventoService.UpdateAsync(id, eventoDto);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _eventoService.DeleteAsync(id);
        }

        [HttpDelete("DeleteRange")]
        public async Task DeleteRanger(int[] ids)
        {
            await _eventoService.DeleteRangerAsync(ids);
        }
    }
}
