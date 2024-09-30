using Identity_Application.Contracts.UserRoles.Response;
using MediatR;

namespace Identity_Application.Contracts.UserRoles.Queries;

public class GetUserRolesQuery : IRequest<List<UserRolesResponse>>
{
	public string Role { get; set; }
	public string UserName { get; set; }
}
