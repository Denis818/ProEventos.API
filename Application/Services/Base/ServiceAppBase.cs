﻿using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Data.Interfaces.Base;
using Domain.Dtos;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ServiceAppBase<TEntity, TEntityDto, TIRepository>
        where TEntity : class, new()
        where TIRepository : class, IRepositoryBase<TEntity>
    {
        protected readonly TIRepository _repository;
        protected readonly INotificador _notificador;
        protected readonly IValidator<TEntityDto> _validator;
        protected readonly IMapper _mapper;

        protected ServiceAppBase(IServiceProvider service)
        {
            _mapper = service.GetRequiredService<IMapper>();
            _repository = service.GetRequiredService<TIRepository>();
            _notificador = service.GetRequiredService<INotificador>();
            _validator = service.GetRequiredService<IValidator<TEntityDto>>();

        }

        public TEntityDto MapToDto(TEntity entity) 
            => _mapper.Map<TEntityDto>(entity);    

        public TEntity MapToModel(TEntityDto entityDto) 
            => _mapper.Map<TEntity>(entityDto);

        public IEnumerable<TEntityDto> MapToListDto(IEnumerable<TEntity> entityDto) 
            => _mapper.Map<IEnumerable<TEntityDto>>(entityDto);
        
        public void NotificarInformacao(string message) 
            => _notificador.Add(new Notificacao(message));

        public bool Validator(TEntityDto entityDto)
        {
            ValidationResult results = _validator.Validate(entityDto);

            if (!results.IsValid)
            {
                results.Errors.ForEach(failure => NotificarInformacao(failure.ErrorMessage));
                return true;
            }

            return false;
        }
    }
}
