using Application.Interfaces.Services;
using Application.Services.Base;
using Data.Intefaces;
using Data.Repository;
using Domain.Dtos;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, EventoDto, IEventoRepository>, IEventoService
    {
        public EventoService(IServiceProvider service) : base(service)
        {
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosAsync(includePalestrantes);

            if (eventos.IsNullOrEmpty())
                NotificarInformacao("Nenhum evento encontrado");

            var eventoDto = _mapper.Map<IEnumerable<EventoDto>>(eventos);

            return eventoDto;
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                NotificarInformacao($"Eventos com tema {tema} não encontrados");

            var eventoDto = _mapper.Map<IEnumerable<EventoDto>>(eventos);

            return eventoDto;
        }

        public async Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                NotificarInformacao($"Evento com id {id} não encontrado");

            var eventoDto = _mapper.Map<EventoDto>(evento);

            return eventoDto;
        }

        public async Task<EventoDto> UpdateAsync(int id, EventoDto eventoDto)
        {
            if (eventoDto == null)
            {
                NotificarInformacao("Evento não pode ser nulo.");
                return null;
            }

            var evento = await _repository.GetEventosByIdAsync(id, false);

            if (evento == null)
            {
                NotificarInformacao("Evento não encontrado.");
                return null;
            }

            _mapper.Map(eventoDto, evento);

            _repository.UpdateAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao("Ocorreu um erro ao atualizar evento.");
                return null;
            }

            return _mapper.Map<EventoDto>(evento);

        }
    }
}
