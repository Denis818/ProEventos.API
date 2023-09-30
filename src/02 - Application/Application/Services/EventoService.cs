using Application.Interfaces.Services;
using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Services.Base;
using Application.Utilities;
using Application.Constants;

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
                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFound);

            return MapToListDto(eventos);
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                Notificar(EnumTipoNotificacao.Erro, $"Nenhum evento com tema={tema} encontrado.");

            return MapToListDto(eventos);
        }

        public async Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFoundById + id);

            return MapToDto(evento);
        }

        public async Task<EventoDto> InsertAsync(EventoDto eventoDto)
        {
            if (Validator(eventoDto)) return null;

            var evento = MapToModel(eventoDto);

            await _repository.InsertAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, ErrorMessages.InsertError);
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
                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFoundOrDifferentId);
                return null;
            }

            if (Validator(eventoDto)) return null;

            MapDtoToModel(eventoDto, evento);

            _repository.UpdateAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, ErrorMessages.UpdateError);
                return null;
            }

            return MapToDto(evento);
        }

        public async Task DeleteAsync(int id)
        {
            var evento = await _repository.GetByIdAsync(id);

            if (evento == null)
            {
                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFoundById + id);
                return;
            }

            _repository.DeleteAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, ErrorMessages.DeleteError);
                return;
            }

            Notificar(EnumTipoNotificacao.Informacao, "Registro Deletado");
        }

        public async Task DeleteRangerAsync(int[] ids)
        {
            var eventos = _repository.Get(evento => ids.Contains(evento.Id)).ToArray();

            if (eventos.IsNullOrEmpty())
            {
                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFoundByIds + string.Join(", ", ids));
                return;
            }

            var idsNaoEncontrados = ids.Except(eventos.Select(evento => evento.Id));

            if (idsNaoEncontrados.Any())
            {
                string idsNotFound = $"{string.Join(", ", idsNaoEncontrados)}. Encontrados foram deletados";

                Notificar(EnumTipoNotificacao.Erro, ErrorMessages.NotFoundByIds + idsNotFound);
            }

            _repository.DeleteRangeAsync(eventos);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, ErrorMessages.DeleteError);
                return;
            }

            Notificar( EnumTipoNotificacao.Informacao, "Registros Deletados");
        }
    }
}
