using Application.Interfaces.Services;
using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Services.Base;
using Application.Utilities;

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
                Notificar(ErrorMessages.NotFound);

            return MapToListDto(eventos);
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                Notificar($"Nenhum evento com tema={tema} encontrado.");

            return MapToListDto(eventos);
        }

        public async Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                Notificar($"{ErrorMessages.NotFound} Id {id}");

            return MapToDto(evento);
        }

        public async Task<EventoDto> InsertAsync(EventoDto eventoDto)
        {
            if (Validator(eventoDto)) return null;

            var evento = MapToModel(eventoDto);

            await _repository.InsertAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(ErrorMessages.InsertError, EnumTipoNotificacao.ErroInterno);
                return null;
            }

            var eventCriado = await _repository.GetEventosByIdAsync(evento.Id, false);

            return MapToDto(eventCriado);
        }

        public async Task<EventoDto> UpdateAsync(int id, EventoDto eventoDto)
        {
            var evento = await _repository.GetEventosByIdAsync(id, false);

            if (evento == null || evento.Id != eventoDto.Id)
            {
                Notificar(ErrorMessages.NotFoundOrDifferentId);
                return null;
            }

            if (Validator(eventoDto)) return null;

            _mapper.Map(eventoDto, evento);

            _repository.UpdateAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(ErrorMessages.UpdateError, EnumTipoNotificacao.ErroInterno);
                return null;
            }

            return MapToDto(evento);
        }

        public async Task DeleteAsync(int id)
        {
            var evento = await _repository.GetByIdAsync(id);

            if (evento == null)
            {
                Notificar($"{ErrorMessages.NotFound} Id {id}");
                return;
            }

            _repository.DeleteAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(ErrorMessages.DeleteError, EnumTipoNotificacao.ErroInterno);
                return;
            }

            Notificar("Registro Deletado");
        }

        public async Task DeleteRangerAsync(int[] ids)
        {
            var eventos = _repository.Get(evento => ids.Contains(evento.Id)).ToArray();

            if (eventos.IsNullOrEmpty())
            {
                Notificar($"{ErrorMessages.NotFound} Ids: {string.Join(", ", ids)}");
                return;
            }

            var idsNaoEncontrados = ids.Except(eventos.Select(evento => evento.Id).ToArray());

            if (idsNaoEncontrados.Any())
            {
                Notificar($"{ErrorMessages.NotFound} Ids: {string.Join(", ", idsNaoEncontrados)}; Deletando os encontrados");
            }

            _repository.DeleteRangeAsync(eventos);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(ErrorMessages.DeleteError, EnumTipoNotificacao.ErroInterno);
                return;
            }

            Notificar("Registros Deletados");
        }
    }
}
