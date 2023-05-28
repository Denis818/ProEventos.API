using Application.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Configurations.PerfisAutoMapper.EntitysProfiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<EventDto, Event>();
        }
    }
}
