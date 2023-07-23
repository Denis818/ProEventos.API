﻿using Application.Interfaces.Services;
using Data.Intefaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Application.Services.Base;
using Application.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class EventoService : ServiceAppBase<Evento, EventoDto, IEventoRepository>, IEventoService
    {
        private readonly IValidator<EventoDto> _validator;

        public EventoService(IServiceProvider service) : base(service)
        {
            _validator = service.GetRequiredService<IValidator<EventoDto>>();
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosAsync(includePalestrantes);

            if (eventos.IsNullOrEmpty())
                NotificarInformacao(ErrorMessages.NotFound);

            var eventoDto = _mapper.Map<IEnumerable<EventoDto>>(eventos);

            return eventoDto;
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _repository.GetAllEventosByTemaAsync(tema);

            if (eventos.IsNullOrEmpty())
                NotificarInformacao($"Nenhum evento com tema={tema} encontrado.");

            var eventoDto = MapToListDto(eventos);

            return eventoDto;
        }

        public async Task<EventoDto> GetEventosByIdAsync(int id, bool includePalestrantes = false)
        {
            var evento = await _repository.GetEventosByIdAsync(id, includePalestrantes);

            if (evento == null)
                NotificarInformacao($"{ErrorMessages.IdNotFound} {id}.");

            var eventoDto = MapToDto(evento);

            return eventoDto;
        }

        public async Task<EventoDto> InsertAsync(EventoDto eventoDto)
        {
            ValidationResult results = _validator.Validate(eventoDto);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    NotificarInformacao(failure.ErrorMessage);
                }
                return null;
            }

            var evento = MapToModel(eventoDto);

            await _repository.InsertAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao(ErrorMessages.InsertError);
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
                NotificarInformacao($"{ErrorMessages.IdNotFoundOrDifferent}");
                return null;
            }

            _mapper.Map(eventoDto, evento);

            _repository.UpdateAsync(evento);

            if (!await _repository.SaveChangesAsync())
            {
                NotificarInformacao(ErrorMessages.UpdateError);
                return null;
            }

            return MapToDto(evento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _repository.GetByIdAsync(id);

            if (evento == null)
            {
                NotificarInformacao($"{ErrorMessages.IdNotFound} {id}");
                return false;
            }

            _repository.DeleteAsync(evento);

            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteRangerAsync(int[] ids)
        {
            var eventos = _repository.Get(evento => ids.Contains(evento.Id)).ToArray();

            if (eventos.IsNullOrEmpty())
            {
                NotificarInformacao($"{ErrorMessages.IdNotFound} {string.Join(", ", ids)}");
                return false;
            }

            _repository.DeleteRangeAsync(eventos);

            return await _repository.SaveChangesAsync();
        }
    }
}
