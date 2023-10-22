using AutoMapper;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Models;

namespace FitNoteIT.Modules.Users.Core.Mapper;
internal class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Role, RoleDto>();
    }
}
