using AutoMapper;
using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Responses;
using Identity_Domain.Entities;

namespace Identity_Application.Features.UserProfiles;

public class UserProfilesProfile : Profile
{
    public UserProfilesProfile()
    {
        CreateMap<UpdateProfileCommand, ApplicationUser>();
        CreateMap<ApplicationUser, UserProfileResponse>();
    }
}
