using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Mapping;

public class AthleteProfile : Profile
{
    public AthleteProfile()
    {
        CreateMap<AthleteCreateDto, Athlete>();

        CreateMap<AthleteUpdateDto, Athlete>();

        CreateMap<Athlete, AthleteResponseDto>()
            .ForMember(
                dest => dest.PositionName,
                opt => opt.MapFrom(src => src.Position.ToString())
            );
    }
}
