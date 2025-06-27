using AutoMapper;
using backFGRB.Domain.Models;

namespace backFGRB.Application.Services.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

        CreateMap<User, UserViewModel>();
        CreateMap<User, UserAuditDto>();     
    }
}