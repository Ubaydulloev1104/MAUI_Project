using AutoMapper;
using Identity_Application.Contracts.ApplicationRoles.Queries;
using Identity_Application.Contracts.ApplicationRoles.Responses;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Role.Queries;

public class GetRoleGetBySlugQueryHandler : IRequestHandler<GetRoleBySlugQuery, RoleNameResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleGetBySlugQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<RoleNameResponse> Handle(GetRoleBySlugQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
        var roleResponses = _mapper.Map<RoleNameResponse>(role);
        return roleResponses;
    }
}
