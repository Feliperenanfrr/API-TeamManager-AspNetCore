using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
