using AutoMapper;
using Identity_Application.Contracts.ApplicationRoles.Queries;
using Identity_Application.Contracts.ApplicationRoles.Responses;
using Identity_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Role.Queries;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleNameResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<List<RoleNameResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync();

        var roleResponses = _mapper.Map<List<RoleNameResponse>>(roles);

        return roleResponses;

    }

}
