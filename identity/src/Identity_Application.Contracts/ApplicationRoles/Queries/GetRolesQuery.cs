using Identity_Application.Contracts.ApplicationRoles.Responses;
using MediatR;

namespace Identity_Application.Contracts.ApplicationRoles.Queries;

public class GetRolesQuery : IRequest<List<RoleNameResponse>>
{
}
