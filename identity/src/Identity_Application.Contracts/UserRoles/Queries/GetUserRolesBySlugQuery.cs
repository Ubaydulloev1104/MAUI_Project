using Identity_Application.Contracts.UserRoles.Response;
using MediatR;

namespace Identity_Application.Contracts.UserRoles.Queries;

public class GetUserRolesBySlugQuery : IRequest<UserRolesResponse>
{
	public string Slug { get; set; }
}
