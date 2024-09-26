using Identity_Application.Contracts.ApplicationRoles.Responses;
using MediatR;

namespace Identity_Application.Contracts.ApplicationRoles.Queries;

public class GetRoleBySlugQuery : IRequest<RoleNameResponse>
{
    public string Slug { get; set; }
}
