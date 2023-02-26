using AutoMapper;
using FitNoteIT.Modules.Users.Core.Common.DTO;
using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Common.Mapper;
internal class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
