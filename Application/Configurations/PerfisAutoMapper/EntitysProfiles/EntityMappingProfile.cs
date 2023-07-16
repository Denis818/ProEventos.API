using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Configurations.PerfisAutoMapper.EntitysProfiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
        }
    }
}
