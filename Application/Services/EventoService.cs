using Application.Interfaces.Services;
using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Services.Base;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, IEventoRepository>, IEventoService
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

        public async Task<EventoDto> InsertAsync(EventoDto eventoDto)
        {
            var evento = _mapper.Map<Evento>(eventoDto);

            await _repository.InsertAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao("Ocorreu um erro ao adicionar");
                return null;
            }

            var eventCriado = await _repository.GetEventosByIdAsync(evento.Id, false);

            return _mapper.Map<EventoDto>(eventCriado);
        }

        public async Task<EventoDto> UpdateAsync(int id, EventoDto eventoDto)
        {
            var evento = await _repository.GetEventosByIdAsync(id, false);

            if (evento == null)
            {
                NotificarInformacao($"Evento com Id={id} não foi encontrado.");
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

        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _repository.GetByIdAsync(id);

            if (evento == null)
            {
                NotificarInformacao($"Evento com Id={id} não foi encontrado.");
                return false;
            }

            _repository.DeleteAsync(evento);

            return await _repository.SaveChangesAsync();
        }
    }
}
