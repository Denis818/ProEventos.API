using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Configurations.PerfisAutoMapper.EntitysProfiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<EventoDto, Evento>();
            CreateMap<PalestranteDto, Palestrante>();
            CreateMap<LoteDto, Lote>();
            CreateMap<RedeSocialDto, RedeSocial>();
        }
    }
}
