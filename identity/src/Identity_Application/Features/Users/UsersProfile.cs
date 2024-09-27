using AutoMapper;
using Identity_Application.Contracts.User.Responses;
using Identity_Domain.Entities;

namespace Identity_Application.Features.Users;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(dest => dest.FullName, opt => opt
            .MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}
