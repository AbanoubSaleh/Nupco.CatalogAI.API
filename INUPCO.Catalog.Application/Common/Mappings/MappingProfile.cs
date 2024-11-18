using AutoMapper;
using INUPCO.Catalog.Application.DTOs;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;

namespace INUPCO.Catalog.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GenericItemPharma, GenericItemPharmaDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        CreateMap<Comment, CommentDto>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));
    }
} 