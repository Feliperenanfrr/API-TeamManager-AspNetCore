using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Mapping;

public class CoachProfile : Profile
{
    public CoachProfile()
    {
        CreateMap<CoachCreateDto, Coach>();

        CreateMap<CoachUpdateDto, Coach>();

        CreateMap<Coach, CoachResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
