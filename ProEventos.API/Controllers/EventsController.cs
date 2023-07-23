using Application.Interfaces.Services;
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
        public async Task<IActionResult> GetAll()
        {
            return CustomResponse(await _eventoService.GetAllEventosAsync(true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CustomResponse(await _eventoService.GetEventosByIdAsync(id, true));
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            return CustomResponse(await _eventoService.GetAllEventosByTemaAsync(tema, true));
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto eventodto)
        {
            return CustomResponse(await _eventoService.InsertAsync(eventodto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto eventoDto)
        {
            return CustomResponse(await _eventoService.UpdateAsync(id, eventoDto));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _eventoService.DeleteAsync(id))
                return CustomResponse("Ocorreu um erro ao deletar");

            return CustomResponse("Deletado");
        }

        [HttpDelete("DeleteRange")]
        public async Task<IActionResult> DeleteRanger(int[] ids)
        {
            if (!await _eventoService.DeleteRangerAsync(ids))
                return CustomResponse("Ocorreu um erro ao deletar");

            return CustomResponse("Deletados");
        }
    }
}
