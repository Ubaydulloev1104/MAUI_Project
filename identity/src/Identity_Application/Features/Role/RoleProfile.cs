using AutoMapper;
using Identity_Application.Contracts.ApplicationRoles.Responses;
using Identity_Domain.Entities;

namespace Identity_Application.Features.Role;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<ApplicationRole, RoleNameResponse>();

    }
}
